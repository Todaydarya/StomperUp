using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StomperUp.Class
{
    internal class MessageClass
    {
        public void Message(string text)
        {
            var notify = new ToastContentBuilder();
            notify.AddText($"{text}");
            notify.Show();
        }
    }
}
