
using MyTemplates;
///2)
///  string method(object) - возвращает строку с данными(Здравствуйте ,@{name}, вы прописаны по адресу @{address} ) заполненными
///1)
///   метод string Method(string, name) - возвращает строку с заполненным данными(Здравствуйте, @{name} вы отчислены )
///   
///3) Здравствуйте ,@{name},  @if(temperature > 37) @then {Выздоравливайте} @else {Прогульщица}  string Method(object)
///
/// object - class Student поля F- Нонская ,  I-Лейсан , O-? , Age- , temperature-37.1
/// 
/// 
///4)   Здравствуйте, студенты группы @{group}. Ваши баллы по ОРИС:  @for(item in students){ @{item.FIO} баллы @{item.balls} }     string Method(object)
///
/// object  -  table fields: list< student>, string group,   student fields: FIO, balls
/// 

public class Student
{
    public string FIO { get; set; }
    public int Points { get; set; }

    /*    public Student( string fio, int points) { 
            FIO = fio;
            Points = points;
        }*/
}

public class Table
{
    public string Group { get; set; }
    public List<Student> Students { get; set; }

}

public class Program
{
    public static void Main(string[] args)
    {
        var ex = (new Mapper()).Example4(
                    new Table()
                    {
                        Group = "11-208",
                        Students = new List<Student>()
                        {
                            new Student(){
                                FIO = "Nonskaya Leisan",
                                Points = 35
                            },
                            new Student()
                            {
                                FIO = "Subuhankulov Bulat",
                                Points = 45
                            },
                            new Student()
                            {
                                FIO = "Karimov Irek",
                                Points = 44
                            }
                        }
                    }
            );



/*        var ex = (new Mapper()).Example3(new
        {
            Name = "leisan",
            Address = "innopolis",
            Temperature = 37.1
        });*/

        //var ex = (new Mapper()).Example1("leisan");

        Console.WriteLine(ex);
    }
}


