using System;
using System.Windows.Forms;
using Omission.Framework;

namespace Omission.WindowsDemo.Exceptions
{
    public class MyUglyErrorMessageHandler : IExceptionHandler
    {
        public ExceptionHandlingResult Handle(Exception exception)
        {
            MessageBox.Show(
                string.Format("An exception occurred and this is the only way I can think of handling it {0}",
                              exception.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return new ExceptionHandlingResult{ReThrow = false, WasHandled = true};
        }
    }
}
