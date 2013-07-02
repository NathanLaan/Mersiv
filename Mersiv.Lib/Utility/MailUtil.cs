using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Mersiv.Lib.Utility
{
    public sealed class MailUtil
    {

        public static void SendMail(string from, string to, string subject, string message, string smtpServer = "localhost")
        {
            try
            {
                MailMessage mailMessage = new MailMessage(from, to, subject, message);
                SmtpClient smtpClient = new SmtpClient(smtpServer);
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Send(mailMessage);
            }
            catch
            {
                //
                // TODO: Logging
                //
            }
        }

        public static void SendMail(string host, int port, string username, string password, string fromEmail, string fromName,
            string toEmail, string toName, string subject, string body, bool useSSL)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.Sender = new MailAddress(fromEmail, fromName, System.Text.Encoding.UTF8);
            mailMessage.From = new MailAddress(fromEmail, fromName);
            mailMessage.To.Add(new MailAddress(toEmail, toName));
            mailMessage.Body = body;
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.Priority = MailPriority.Normal;

            SmtpClient smtpClient = new SmtpClient(host, port);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
            smtpClient.EnableSsl = useSSL;
            smtpClient.Send(mailMessage);
        }
    }
}
