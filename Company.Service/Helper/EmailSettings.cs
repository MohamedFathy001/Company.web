using System.Net;
using System.Net.Mail;

namespace Company.Service.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Email input)
        {
            var client = new SmtpClient("smtp.gmail.com" , 587);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential("arsanynashat0@gmail.com", "gthrpemzmuzferyv");

            client.Send("arsanynashat0@gmail.com", input.To, input.subject, input.body);
        }
    }
}
