using System;
using System.Windows.Forms;
using Omission.Framework;

namespace Omission.WindowsDemo.Logging
{
    public class LogEmailer : IExceptionLogger
    {
        NotifyIcon _notifyIcon = new NotifyIcon();

        string[] _emailRecipients;

        public LogEmailer(string[] emailRecipients)
        {
            _emailRecipients = emailRecipients;

            BuildupNotifiyIcon();
        }

        void BuildupNotifiyIcon()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));

            _notifyIcon.Visible = true;
            _notifyIcon.Text = "Ballon Tip";
            _notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        }

        public void Log(Exception exception)
        {
            string formattedMessage = string.Format("\r\n{0}\r\nMessage has been sent to the following email addresses: ", exception.Message);

            foreach (string emailRecipient in _emailRecipients)
            {
                formattedMessage += string.Format("\r\n{0}", emailRecipient);
            }

            _notifyIcon.BalloonTipText = formattedMessage;
            _notifyIcon.ShowBalloonTip(2000);

        }
    }
}
