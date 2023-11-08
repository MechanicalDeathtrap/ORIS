using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyTemplates
{
    public class Mapper
    {

        public string Example1(string name)
        {
            string result = "Здравствуйте, @{name} , вы отчислены".Replace("@{name}", name);
            return result;
        }

        public string Example2(object obj)
        {
            var name = obj.GetType().GetProperty("Name").GetValue(obj).ToString();
            var address = obj.GetType().GetProperty ("Address").GetValue(obj).ToString();

            var message = "Здравствуйте ,@{name}, вы прописаны по адресу: @{address}".Replace("@{name}", name).Replace("@{address}", address);
            return message;
        }

        public string Example3(object obj)
        {
            var message = "Здравствуйте ,@{name},  @if(temperature > 37) @then { Выздоравливайте} @else { Прогульщица}";

            var name = obj.GetType().GetProperty("Name").GetValue(obj).ToString();
            var temperature = Convert.ToDouble(obj.GetType().GetProperty("Temperature").GetValue(obj).ToString());

            var ifState = new Regex(@"\((.*?)\)").Match(message).Value.Trim(new char[] { '{', '}' });
            var states = new Regex(@"{(.*?)}").Matches(message);
            var thenState = states[1].Value.Trim(new char[] { '{', '}' });
            var elseState = states[2].Value.Trim(new char[] { '{', '}' });

            message = new Regex(@"(.*?) ,@{name},").Match(message).Value.Replace("@{name}", name);

            var sign = ifState.Trim(new char[] {'(', ')'}).Split(' ')[1];
            var stateTempa = Convert.ToDouble(ifState.Trim(new char[] { '(', ')' }).Split(' ')[2].ToString());

            switch (sign)
            {
                case ">":
                    message += temperature > stateTempa ? thenState : elseState;
                    break;
            }

            return message;

/*            var length = message.Length;
            var subLenght = length - 1 - message.IndexOf('i') - 1;
            var ifElseStatement = message.Substring(message.IndexOf('i') - 1);

            var ifStatement = ifElseStatement.Substring(ifElseStatement.IndexOf('(') + 1, ifElseStatement.IndexOf(')') - ifElseStatement.IndexOf('(') - 1);//.Replace("temperature", temperature); 
            var thenStatement = ifElseStatement.Substring(ifElseStatement.IndexOf("@then"), ifElseStatement.IndexOf('}') - ifElseStatement.IndexOf("@then")+1).Replace("@then ", "")
                .Trim(new char[] { '{', '}' });
            var elseStatement = ifElseStatement.Substring(ifElseStatement.IndexOf("@else"), ifElseStatement.LastIndexOf('}') - ifElseStatement.IndexOf("@else")+1).Replace("@else ", "")
                .Trim(new char[] { '{', '}' });

            ifStatement.Replace("tempera")*/
        }

        public string Example4(object obj)
        {
            var message = "Здравствуйте, студенты группы @{group}. Ваши баллы по ОРИС:  @for(var item in Students){ @{item.FIO} баллы @{item.Points} }";

            var group = obj.GetType().GetProperty("Group").GetValue(obj).ToString();

            var forState = new Regex(@"@for\((.*?)\)").Match(message).Value.Replace("@for", "").Trim(new char[] { '(', ')' });
            var list = forState.Split(' ')[3];
            var students = obj.GetType().GetProperty(list).GetValue(obj) as List<Student> ;
            var statement = new Regex(@"{ @{(.*?)\} }").Match(message).Value.Trim(new char[] {'{', '}'}).Trim();

            message = message.Substring(0,message.IndexOf(":")+1).Replace("@{group}", group);


            foreach(var item in students)
            {
                message += "\n" + statement.Replace("@{item.FIO}", item.FIO).Replace("@{item.Points}", item.Points.ToString()) ;
            }
            return message;
        }
    }
}
