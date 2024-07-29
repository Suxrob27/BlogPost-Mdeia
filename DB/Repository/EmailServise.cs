using DB.IRepository;
using DB.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DB.Repository
{
    public class EmailServise : IEmailServise

    {
        private readonly IOptions<SMTP> smtpSetting;

        public EmailServise(IOptions<SMTP> smtpSetting)
        {
            this.smtpSetting = smtpSetting;
        }

        public async Task Send (
            string from,
            string to,
            string subject,
            string body)
        {

            var message = new MailMessage(from, to, subject, body);


            using (var emailClient = new SmtpClient(smtpSetting.Value.Server, smtpSetting.Value.Port))
            {
                emailClient.Credentials = new NetworkCredential(
                    smtpSetting.Value.Login,
                    smtpSetting.Value.Password);

                await emailClient.SendMailAsync(message);   
            }
        }
    }
}
