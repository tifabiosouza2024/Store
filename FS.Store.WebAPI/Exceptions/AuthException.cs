namespace FS.Store.WebAPI.Exceptions
{
    [Serializable]
    public class AuthException : Exception
    {
        public AuthException() : base() { }

        public AuthException(string message) : base(message) { }
    }
}
