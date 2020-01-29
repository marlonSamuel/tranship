using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using TransShip.LogBook;

namespace TransShip.EMailHandler
{
    public class EMailHandler
    {
        private readonly SmtpClient _clienteCorreoElectronico = new SmtpClient();
        private MailMessage _mensajeCorreoElectronico;        
        private readonly MailAddress _sender;
        public bool MailSent;

        public EMailHandler()
        {
            var target = (MailTarget)LogManager.Configuration.FindTargetByName("EMailHandler");

            string from = target.From.ToString().Replace("'","");
            string subject = target.Subject.ToString().Replace("'", "");
            string host = target.SmtpServer.ToString().Replace("'","") ;
            string password = target.SmtpPassword.ToString().Replace("'","");
            string username = target.SmtpUserName.ToString().Replace("'",""); 
            int port = target.SmtpPort;
            bool enableSsl = target.EnableSsl;

            _sender = new MailAddress(from, subject, Encoding.UTF8);
            _clienteCorreoElectronico.Host = host;
            _clienteCorreoElectronico.EnableSsl = enableSsl;
            _clienteCorreoElectronico.Credentials = new NetworkCredential
            {
                Password = password,
                UserName = username
            };
            _clienteCorreoElectronico.Port = port;
            MailSent = false;
        }

        public void SendConfirmationEmail(string message, string link, string to)
        {
            _mensajeCorreoElectronico = new MailMessage
            {
                Body = string.Format("{0}",message + "<br/>" + link),
                BodyEncoding = Encoding.UTF8,
                Subject = "Confirm EMail",
                SubjectEncoding = Encoding.UTF8,
                From = _sender,
                IsBodyHtml = true
            };
            _mensajeCorreoElectronico.To.Add(new MailAddress(to));
            _clienteCorreoElectronico.Send(_mensajeCorreoElectronico);            
        }

        public void SendMembershipEmail(string message, string to) {
            _mensajeCorreoElectronico = new MailMessage {
                Body = string.Format("{0}", message + "<br/>"),
                BodyEncoding = Encoding.UTF8,
                Subject = "Membership Alert",
                SubjectEncoding = Encoding.UTF8,
                From = _sender,
                IsBodyHtml = true
            };
            _mensajeCorreoElectronico.To.Add(new MailAddress(to));
            _clienteCorreoElectronico.Send(_mensajeCorreoElectronico);
        }

        private static string RngCharacterMask(int longitud)
        {
            var maxSize = longitud;

            const string a = "1234567890";
            var chars = a.ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            var size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (var b in data)
                result.Append(chars[b % (chars.Length - 1)]);
            return result.ToString();
        }


        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {            
            var token = (string)e.UserState;
            if (e.Cancelled)
                LogBook.LogBook.TextLog.Info("Cancelled {0}",token);
            if (e.Error != null)
                LogBook.LogBook.TextLog.Info("Error {0} {1}", token,e.Error);
            else
                LogBook.LogBook.TextLog.Info("Message sent {0}", token);
            MailSent = true;
            _mensajeCorreoElectronico.Dispose();
        }
    }
}
