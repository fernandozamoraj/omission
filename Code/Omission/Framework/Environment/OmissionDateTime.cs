using System;

namespace Omission.Framework.Environment
{
    public class OmissionDateTime : IDateTime
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}