namespace FS.Store.DAL.Exceptions
{
    [Serializable]
    public class RepositoryException : Exception
    {
        public RepositoryException() : base() { }

        public RepositoryException(string message) : base(message) { }
    }
}
