using System;
using Omission.WindowsDemo.Environment;

namespace Omission.WindowsDemo
{
    public class OmissionDateTime : IDateTime
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}