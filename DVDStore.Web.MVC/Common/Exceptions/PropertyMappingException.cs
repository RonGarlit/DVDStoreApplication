namespace DVDStore.Web.MVC.Common.Exceptions
{
    public class PropertyMappingException : Exception
    {
        public PropertyMappingException()
        {
        }

        public PropertyMappingException(string message)
            : base(message)
        {
        }

        public PropertyMappingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}