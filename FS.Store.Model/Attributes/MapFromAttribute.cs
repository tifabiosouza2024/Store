namespace FS.Store.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MapFromAttribute(string source) : Attribute
    {
        public string Source { get; } = source;
    }
}
