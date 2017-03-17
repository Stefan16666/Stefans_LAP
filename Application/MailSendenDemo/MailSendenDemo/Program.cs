using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailSendenDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // erstelle die Mail-Nachricht
                string from = "franz.pilgerstorfer@qualifizierung.or.at";
                string to = "franz.pilgerstorfer@gmx.at";
                MailMessage mail = new MailMessage(from, to);
                mail.Subject = "this is a test email.";
                mail.Body = "this is my test email body";

                // erstelle den SMTP Client
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Host = "srv08.itccn.loc";
                // username üblicherweise VORNAME.NACHNAME@qualifizierung.or.at
                string username = "franz.pilgerstorfer@qualifizierung.or.at";
                // bitte IHR PASSWORT eintragen
                string password = "123user!";
                client.Credentials = new NetworkCredential(username, password);

                // sende alles ab
                client.Send(mail);

                Console.WriteLine("Mail wurde versendet!");
            }
            catch (SmtpException  ex)
            {
                Console.WriteLine("Fehler beim Mail-Versand");
                Debug.WriteLine(ex.StatusCode);
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }
        }
    }
}
