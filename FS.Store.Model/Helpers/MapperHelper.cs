namespace FS.Store.Model.Helpers
{
    public static class MapperHelper
    {
        public static TDestination Map<TDestination>(object source) where TDestination : new()
        {
            ArgumentNullException.ThrowIfNull(source);
            var destination = new TDestination();

            var sourceProperties = source.GetType().GetProperties().Where(p => p.CanRead);

            var destinationProperties = typeof(TDestination).GetProperties().Where(p => p.CanWrite).ToDictionary(p => p.Name);

            foreach (var sourceProperty in sourceProperties)
            {
                if (!destinationProperties.TryGetValue(sourceProperty.Name, out var destinationProperty))
                {
                    continue;
                }

                if (!destinationProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                {
                    continue;
                }

                var value = sourceProperty.GetValue(source);

                destinationProperty.SetValue(destination, value);
            }

            return destination;
        }
    }
}