using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace DDenry
{
    public class Email
    {
        private String[] attachments = new String[0];
        public SendCompletedEventHandler SendCompleted;
        //发送Email
        public Boolean SendEmailToAdmin(String subject, String body, List<String> attachments = null)
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
            //添加附件
            if (attachments != null)
                for (int i = 0; i < attachments.Count; i++)
                {
                    mailMessage.Attachments.Add(new System.Net.Mail.Attachment(attachments[i]));
                }
            //邮件内容
            mailMessage.Body = "[" + DateTime.Now + "]<br>  " + System.Environment.UserName + ":<br>&emsp;&emsp;&emsp;&emsp;" + body;
            //
            mailMessage.From = mailAddress;
            //
            mailMessage.To.Add("dengrui.520@163.com");
            //
            SmtpClient client = new SmtpClient();
            //发送完毕后回调
            client.SendCompleted += SendCompleted;
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