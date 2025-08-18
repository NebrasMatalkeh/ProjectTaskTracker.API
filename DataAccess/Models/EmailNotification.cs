using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{

    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FromEmail { get; set; }
    }

    public class EmailNotification
    {
        public string To { get; set; } 
        public string Subject { get; set; }
        public string Body { get; set; }

    }

}
