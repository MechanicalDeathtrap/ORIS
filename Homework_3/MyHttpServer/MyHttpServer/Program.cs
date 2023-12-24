using System.Net;

HttpListener server = new HttpListener();

server.Prefixes.Add("http://127.0.0.1:2323/");
server.Start();
Console.WriteLine("Серевер запущен");

var context = await server.GetContextAsync();

var response = context.Response;

var filePath = Directory.GetCurrentDirectory() + @"\index.html";
var responseData = File.ReadAllBytes(filePath);
byte[] buffer = responseData;
// получаем поток ответа и пишем в него ответ
response.ContentLength64 = buffer.Length;
using Stream output = response.OutputStream;
// отправляем данные
await output.WriteAsync(buffer);
await output.FlushAsync();

Console.WriteLine("Запрос обработан");

server.Stop();
Console.WriteLine("Сервер прекратил работу");