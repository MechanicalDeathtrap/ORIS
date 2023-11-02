using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using Azure;
using System.Reflection;
using System.Reflection.Metadata;

namespace MyORM
{
    public class Database
    {
        public IDbConnection connection { get; set; }
        public IDbCommand command { get; set; }

        public Database(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            command = connection.CreateCommand();
        }

        public bool Insert<T>(T entity)
        {
            using (connection)
            {
                try
                {
                    PropertyInfo[] modelfields = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    List<string> parametrs = modelfields.Select(x =>$"@{x.Name}" ).ToList();

                    string sqlExpression = $"INSERT INTO {typeof(T).Name} VALUES ({String.Join(", ", parametrs)})";

                    foreach(PropertyInfo parametr in modelfields)
                    {
                        command.Parameters.Add(new SqlParameter($"@{parametr.Name}", parametr.GetValue(entity)));
                    }

                    command.CommandText = sqlExpression;
                    connection.Open();
                    command.ExecuteNonQuery();

                    return true;
                }
                catch 
                {
                    Console.WriteLine("Возникла ошибка при выполнении команды Insert");
                    return false;
                    throw;
                }

            }
        }

        public void Delete<T>(int id)
        {
            using(connection)
            {
                var sqlExpression = $"DELETE FROM {typeof(T).Name} WHERE id=@ID";
                command.Parameters.Add(new SqlParameter("@ID", id));
                command.CommandText = sqlExpression;
                command.ExecuteNonQuery();
            }
        }

        public void Update<T>(T entity)
        {
            using (connection)
            {
                PropertyInfo[] modelFields = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                string tableName = typeof(T).Name;
                List<string> columnsToUpdate = new List<string>();
                List<SqlParameter> parameters = new List<SqlParameter>();

                string sqlExpression = $"SELECT * FROM {typeof(T).Name}";
                command.CommandText = sqlExpression;

                connection.Open();
                var reader = command.ExecuteReader();
                var columnName = reader.GetName(0);
                reader.Close();

                foreach (PropertyInfo parameter in modelFields)
                {
                    if (parameter.Name == columnName)
                    {
                        command.Parameters.Add(new SqlParameter($"@{parameter.Name}", parameter.GetValue(entity)));
                    }
                    else
                    {
                        columnsToUpdate.Add($"{parameter.Name} = @{parameter.Name}");
                        command.Parameters.Add(new SqlParameter($"@{parameter.Name}", parameter.GetValue(entity)));
                    }

                }
                sqlExpression = $"UPDATE {tableName} SET {string.Join(", ", columnsToUpdate)} WHERE {columnName} = @{columnName}";
                command.CommandText = sqlExpression;

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<T> Select<T>()
        {
            IList<T> list = new List<T>();
            Type t = typeof(T);

            using (connection)
            {
                string sqlExpression =  $"SELECT * FROM {t.Name}";
                command.CommandText = sqlExpression;

                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    T obj = (T)Activator.CreateInstance(t);
                    t.GetProperties().ToList().ForEach(x => x.SetValue(obj, reader[x.Name]));

                    list.Add(obj);
                }

                return list;
            }
        }

        public T SelectById<T>(int id)
        {
            Type t = typeof(T);

            using (connection)
            {
                string sqlExpression = $"SELECT * FROM {t.Name} WHERE id = @Id";
                command.CommandText = sqlExpression;
                command.Parameters.Add(new SqlParameter("@Id", id));

                connection.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    T obj = (T)Activator.CreateInstance(t);
                    t.GetProperties().ToList().ForEach(x => x.SetValue(obj, reader[x.Name]));

                    return obj;
                }

                return default(T);  
            }

        }

    }
}
