namespace DVDStore.Web.MVC.Common.Exceptions
{
    public class IntentionalException : Exception
    {
        public IntentionalException()
        {
        }

        public IntentionalException(string message)
            : base(message)
        {
        }

        public IntentionalException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
