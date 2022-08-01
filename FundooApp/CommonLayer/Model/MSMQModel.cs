using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace CommonLayer.Model
{
    public class MSMQModel
    {
        MessageQueue messageQ = new MessageQueue();

        public void sendData2Queue(string Token)
        {
            messageQ.Path = @".\private$\Token";
            if(!MessageQueue.Exists(messageQ.Path))
            {
                //Exists
                MessageQueue.Create(messageQ.Path);
            }
            
            messageQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQ.ReceiveCompleted += MessageQ_ReceiveCompleted;
            messageQ.Send(Token);
            messageQ.BeginReceive();
            messageQ.Close();
        }

        private void MessageQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = messageQ.EndReceive(e.AsyncResult);
            string Token = msg.Body.ToString();
            string subject = "FundooNotes Reset Link";
            string body = Token;
            var SMTP = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("hoffmannicolas6@gmail.com", "tuvxvzdkucheykzw"),
                EnableSsl = true,
            };

            SMTP.Send("hoffmannicolas6@gmail.com", "hoffmannicolas6@gmail.com", subject, body);

            messageQ.BeginReceive();
        }
    }
}
