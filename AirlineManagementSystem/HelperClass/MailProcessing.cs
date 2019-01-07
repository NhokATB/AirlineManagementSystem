using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AirportManagerSystem.HelperClass
{
    class MailProcessing
    {
        public static string GenneratePass()
        {
            char[] chars = new char[]
            {
                '0', '1', '2', '3', '4','5', '6', '7', '8', '9',
                'a','b','c','d','e','f','g','h','i','j','k', 'l',
                'm','n','o','u','p','q','r','s','t','u','x','w','v','z'
            };
            StringBuilder newPass = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                int index = random.Next(0, chars.Length);
                newPass.Append(chars[index]);
            }
            return newPass.ToString();
        }
        public static void SendMail(string to, string title, string text)
        {
            MailMessage mess = new MailMessage("nhokvuongnguyen1996@gmail.com", to, title, text);
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("nhokvuongnguyen1996@gmail.com", "nhokvuong1703_nguyen1996*atb");
            client.Send(mess);
        }
    }
}
