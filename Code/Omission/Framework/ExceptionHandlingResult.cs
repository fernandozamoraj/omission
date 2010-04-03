namespace Omission.Framework
{
    public class ExceptionHandlingResult
    {
        public bool WasHandled { get; set; }
        public bool ReThrow { get; set; }

        public ExceptionHandlingResult()
        {
            WasHandled = false;
            ReThrow = true;
        }
    }
}