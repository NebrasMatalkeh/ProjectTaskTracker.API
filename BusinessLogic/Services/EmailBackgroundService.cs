using BusinessLogic.Interfaces;
using DataAccess.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly SmtpSettings _settings;

        public EmailBackgroundService(IOptions<SmtpSettings> options)
        {

            _settings = options.Value;
            if (string.IsNullOrEmpty(_settings.FromEmail))
                _settings.FromEmail = "admain@23.com";

            if (string.IsNullOrWhiteSpace(_settings.Host))
                _settings.Host = "sandbox.smtp.mailtrap.io";

            if (_settings.Port == 0)
                _settings.Port = 2525;

            if (string.IsNullOrWhiteSpace(_settings.Username))
                _settings.Username = "6c8a810dd93f79";

            if (string.IsNullOrWhiteSpace(_settings.Password))
                _settings.Password = "c18db6d2e2b6e1";

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                if (NotificationQueue.emails.TryDequeue(out var email))
                {
                    var message = new MimeMessage();
                    message.From.Add(MailboxAddress.Parse(_settings.FromEmail));
                    message.To.Add(MailboxAddress.Parse(email.To));
                    message.Subject = email.Subject;
                    message.Body = new TextPart("Plain") { Text = email.Body };

                    using var client = new SmtpClient();
                    await client.ConnectAsync(_settings.Host, _settings.Port, false, stoppingToken);
                    await client.AuthenticateAsync(_settings.Username, _settings.Password, stoppingToken);
                    await client.SendAsync(message, stoppingToken);
                    await client.DisconnectAsync(true, stoppingToken);
                }
                await Task.Delay(1000);


               
               
            }
        }
    }
}