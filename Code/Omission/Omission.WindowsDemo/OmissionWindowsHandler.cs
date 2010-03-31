using System;
using System.Text;
using System.Windows.Forms;
using Omission.Framework;

namespace Omission.WindowsDemo
{
    public class OmissionWindowsHandler : IExceptionHandler
    {
        IAppConfig _applicationConfig;

        public OmissionWindowsHandler(IAppConfig appConfig)
        {
            _applicationConfig = appConfig;
        }

        public ExceptionHandlingResult Handle(Exception exception)
        {

            if(exception is OmissionDemoBaseException)
            {
                HandleRecognizedException(exception);
            }
            else
            {
                HandleUnknownException(exception);
            }
            
            return new ExceptionHandlingResult{ReThrow = false, WasHandled = true};
        }

        void HandleUnknownException(Exception exception)
        {
            StringBuilder fullMessage = new StringBuilder();
            Exception tempException = exception;

            while (tempException != null)
            {
                fullMessage.Append("\r\n");
                fullMessage.Append(exception.GetType().ToString());
                fullMessage.Append("\r\n");
                fullMessage.Append(tempException.Message);
                tempException = tempException.InnerException;
            }

            MessageBox.Show(fullMessage.ToString(),
                            _applicationConfig.GetApplicationName(), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);  

        }

        void HandleRecognizedException(Exception exception)
        {
            MessageBox.Show(exception.Message, _applicationConfig.GetApplicationName() + " error", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
        }
    }
}