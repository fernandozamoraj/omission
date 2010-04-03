using System;
using System.Text;
using System.Windows.Forms;

namespace Omission.Framework.Windows
{
    public class GenericWindowsHandler : IExceptionHandler
    {
        IAppConfig _appConfig;

        public GenericWindowsHandler(IAppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public ExceptionHandlingResult Handle(Exception exception)
        {
            ExceptionHandlingResult result = new ExceptionHandlingResult();

            HandleException(exception);

            result.ReThrow = false;
            result.WasHandled = true;

            return result;
        }

        protected void HandleException(Exception e)
        {
            if (System.Environment.UserInteractive)
            {
                StringBuilder message = new StringBuilder();

               //The concurrency violation error wasn't being handled correclty because of this. 
                if (e.GetBaseException().Message.IndexOf("Concurrency violation") > 0)
                {
                    message.Append("The current operation could not be completed due to a concurrency issue.");
                    message.Append("\r\nAnother user of process modified some or all of your data prior to this operation.");

                    //Display message
                    MessageBox.Show(null, message.ToString(), "Concurrency", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //create message string
                    message.Append(_appConfig.GetApplicationName() + " has encountered an exception.");

                    //If the uppermost exception is the same as the base then only show the base, else show both
                    if (e.Message != e.GetBaseException().Message)
                    {
                        message.Append(string.Format("\r\nUppermost Exception: {0} {1}", e.GetType().Name, e.Message));
                    }

                    message.Append(string.Format("\r\nBase Exception: {0} {1}", e.GetBaseException().GetType().Name,
                                                 e.GetBaseException().Message));
                    //Display message
                    MessageBox.Show(null, message.ToString(), e.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}