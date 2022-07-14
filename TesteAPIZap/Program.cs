// See https://aka.ms/new-console-template for more information

using System.Net.Http.Headers;
using System.Text;
using Whatsapp.Domain;

HttpClient client = new();

while (true)
{
    var msg = Console.ReadLine();

    var message = new TextMessage("5549999014654", "alo");
}

