using System;
using System.Windows.Forms;
using Omission.Framework;

namespace Omission.WindowsDemo.Logging
{
    public class LogEmailer : IExceptionLogger
    {
        string[] _emailRecipients;

        public LogEmailer(string[] emailRecipients)
        {
            _emailRecipients = emailRecipients;
        }

        public void Log(Exception exception)
        {
            string formattedMessage = string.Format("\r\n{0}\r\nLet's supposed that we sent the message to the following email addresses: ", exception.Message);

            foreach (string emailRecipient in _emailRecipients)
            {
                formattedMessage += string.Format("\r\n{0}", emailRecipient);
            }

            //TODO fill in the details of how you would actually send the message
            MessageBox.Show(formattedMessage, "LogEmailer", MessageBoxButtons.OK);
        }
    }
}
