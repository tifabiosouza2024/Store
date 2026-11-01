namespace FS.Store.Model.Helpers
{
    public static class MapperHelper
    {
        public static TDestination Map<TDestination>(object source) where TDestination : new()
        {
            ArgumentNullException.ThrowIfNull(source);

            var destination = new TDestination();

            var sourceProperties = source.GetType()
                .GetProperties()
                .Where(p => p.CanRead);

            var destinationProperties = typeof(TDestination)
                .GetProperties()
                .Where(p => p.CanWrite)
                .ToDictionary(p => p.Name);

            foreach (var sourceProperty in sourceProperties)
            {
                if (!destinationProperties.TryGetValue(sourceProperty.Name, out var destinationProperty))
                {
                    continue;
                }

                var value = sourceProperty.GetValue(source);

                if (value == null)
                    continue;

                var destinationType = destinationProperty.PropertyType;
                var sourceType = sourceProperty.PropertyType;

                if (destinationType.IsAssignableFrom(sourceType))
                {
                    destinationProperty.SetValue(destination, value);
                    continue;
                }

                // Tratamento de listas
                if (value is System.Collections.IEnumerable sourceList && destinationType.IsGenericType && destinationType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var destinationItemType = destinationType.GetGenericArguments()[0];

                    var destinationList = (System.Collections.IList)Activator.CreateInstance(destinationType)!;

                    foreach (var item in sourceList)
                    {
                        if (item == null)
                            continue;

                        var mappedItem = typeof(MapperHelper)
                            .GetMethod(nameof(Map))!
                            .MakeGenericMethod(destinationItemType)
                            .Invoke(null, [item]);

                        destinationList.Add(mappedItem);
                    }

                    destinationProperty.SetValue(destination, destinationList);
                }
            }

            return destination;
        }
    }
}