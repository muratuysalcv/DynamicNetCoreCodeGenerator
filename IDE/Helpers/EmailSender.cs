using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Scriptingo.Admin
{
    public class EmailSender
    {
        public static string SendEmail(
            string toMailAddress,
            string subject,
            string htmlString,
             List<string> FileAttachmentPaths = null)
        {
            try
            {
                var config = AdminConfig.Get();
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(config.MailSettings.Email);

                if (toMailAddress.Contains(";"))
                {
                    var mails = toMailAddress.Split(';').ToList();
                    foreach (var addr in mails)
                    {
                        message.To.Add(addr);
                    }
                }
                else
                {
                    message.To.Add(toMailAddress);
                }
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = config.MailSettings.Port;
                smtp.Host = config.MailSettings.Host; //for gmail host  
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(config.MailSettings.Email, config.MailSettings.Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                if (FileAttachmentPaths != null)
                {
                    foreach (var attachmentpath in FileAttachmentPaths)
                    {
                        System.Net.Mail.Attachment attachment;
                        attachment = new System.Net.Mail.Attachment(attachmentpath);
                        message.Attachments.Add(attachment);
                    }
                }
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                return "ERROR|" + ex.Message;
            }

            return "1";
        }
    }
}
