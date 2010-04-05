using System;
using System.Windows.Forms;
using Omission.Framework;
using Omission.Framework.Environment;
using Omission.Framework.Logging;
using Omission.WindowsDemo.Exceptions;
using Omission.WindowsDemo.Logging;
using Omission.WindowsDemo.Model;

namespace Omission.WindowsDemo
{
    public partial class Form1 : Form, IExceptionLogger
    {
        readonly ExceptionLauncher _exceptionLauncher = new ExceptionLauncher();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                WrappedException selection = GetSelection();

                _exceptionLauncher.Throw(selection.Exception);
            }
            catch (Exception exception)
            {
                OmissionFacade.Handle(exception);
            }
        }

        WrappedException GetSelection()
        {
            return lbxOptions.SelectedItem as WrappedException;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            LoadExceptions();
            Configure();
        }

        void Configure()
        {
            var configuration =
                OmissionFacade.GetExceptionHandler().GetExceptionConfiguration();

            IExceptionLogger fileLogger = new SimpleFileLogger(new FileNameCreator().GetFilePath("SilentLogger"),
                                                           new OmissionDateTime(),
                                                           new AppConfig(),
                                                           new FileSystem(),
                                                           new OStreamWriter());

            OmissionFacade.GetExceptionHandler().DefaultLogger = this;

            configuration
                .MapExceptionToHandler<UglyException>(new MyUglyErrorMessageHandler())
                .With(this)
                .AndAlso(new LogEmailer(new []{"fz@hotmail.com", "bib@yahoo.com"}))
                .AndAlso(fileLogger)

                //.IgnoreExceptionForHandling<QuietException>()
                .IgnoreExceptionForHandling<QuietException>()
                .With(this)
                .AndAlso(new LogEmailer(new []{"fz@gmail.com", "jz@gmail.com"}))
                .AndAlso(new Log4NetLogger())
                .MapExceptionToLogger<Exception>(this);
                
        }

        void LoadExceptions()
        {
            lbxOptions.DataSource = _exceptionLauncher.GetExceptions();
        }

        public void Log(Exception exception)
        {
            string formattedMessage = string.Format("{0} {1} {2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), exception.Message);
            lbxLogEntries.Items.Add(formattedMessage);
        }
    }
}
