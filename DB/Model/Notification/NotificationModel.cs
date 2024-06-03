using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Model.Notification
{
    public class NotificationModel
    {
        public string Message { get; set; } = string.Empty; 
        public NotificationType Type { get; set; }
    }
}
