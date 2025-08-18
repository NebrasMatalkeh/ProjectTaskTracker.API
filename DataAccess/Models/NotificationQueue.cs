using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public static class NotificationQueue
    {
        public static ConcurrentQueue<EmailNotification> emails  = new ();

    }

}