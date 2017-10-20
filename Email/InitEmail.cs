using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
    public class InitEmail
    {
        //发送Email
        public Boolean SendEmailToAdmin(String subject, String body)
        {
            MailAddress mailAddress = new MailAddress("nosafe4u@163.com", "I'm Tool", System.Text.Encoding.UTF8);
            MailMessage mailMessage = new MailMessage();
            //设置标题编码
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            //设置内容编码
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            //设置邮件优先级
            mailMessage.Priority = MailPriority.Normal;
            //邮件标题
            mailMessage.Subject = subject;
            //邮件内容
            mailMessage.Body = "[" + DateTime.Now + "]<br>  " + System.Environment.UserName + ":<br>&emsp;&emsp;&emsp;&emsp;" + body;
            //
            mailMessage.From = mailAddress;
            //
            mailMessage.To.Add("dengrui.520@163.com");
            //
            SmtpClient client = new SmtpClient();
            //
            client.Host = "smtp.163.com";
            client.UseDefaultCredentials = true;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            //
            client.Credentials = new NetworkCredential("nosafe4u@163.com", "password4u");
            //启用ssl,也就是安全发送
            client.EnableSsl = true;
            //
            return ReturnMessage(mailMessage, client);
        }

        private Boolean ReturnMessage(MailMessage mailMessage, SmtpClient client)
        {
            try
            {
                //发送邮件
                client.SendAsync(mailMessage, "OK");
                return true;
                //
            }
            catch (Exception exception)
            {
                return false;
                throw exception;
            }
        }
    }
}
