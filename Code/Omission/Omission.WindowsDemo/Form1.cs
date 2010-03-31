using System;
using System.Windows.Forms;
using Omission.WindowsDemo.Exceptions;

namespace Omission.WindowsDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                int selection = GetSelection();

                switch(selection)
                {
                    case 0: ThrowQuietException(); break;
                    case 1: ThrowUglyException(); break;
                    default: throw new Exception("base exception");
                }
            }
            catch (Exception exception)
            {
                OmissionFacade.Handle(exception);
            }
        }

        int GetSelection()
        {
            return lbxOptions.SelectedIndex;
        }

        private void ThrowQuietException()
        {
            throw new UglyException("Ooooweee!");
        }

        private void ThrowUglyException()
        {
            throw new QuietException("Shh! No talking!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadExceptions();
        }

        void LoadExceptions()
        {
            
        }
    }
}
