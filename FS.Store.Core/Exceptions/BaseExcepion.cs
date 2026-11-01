namespace FS.Store.Core.Exceptions
{

    public abstract class BaseException : Exception
    {
        protected BaseException() : base()
        {
        }
        protected BaseException(string message) : base(message)
        {
        }

        protected BaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public static void ThrowIfNull(object? value, string message)
        {
            if (value is null)
                throw new InvalidOperationException(message);
        }

        public static void ThrowIf(bool condition, string message)
        {
            if (condition)
                throw new InvalidOperationException(message);
        }
    }
}
