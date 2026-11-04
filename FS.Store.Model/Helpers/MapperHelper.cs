
using FS.Store.Model.Attributes;
using System.Collections;
using System.Reflection;

namespace FS.Store.Model.Helpers
{
    public static class MapperHelper
    {
        public static TDestination Map<TDestination>(object source) where TDestination : new()
        {
            ArgumentNullException.ThrowIfNull(source);
            var destination = new TDestination();
            var sourceProperties = source.GetType().GetProperties().Where(p => p.CanRead).ToDictionary(p => p.Name);
            var destinationProperties = typeof(TDestination).GetProperties().Where(p => p.CanWrite);

            foreach (var destinationProperty in destinationProperties)
            {
                var mapFrom = destinationProperty.GetCustomAttribute<MapFromAttribute>();

                var value = mapFrom != null
                    ? GetNestedValue(source, mapFrom.Source)
                    : sourceProperties.TryGetValue(
                        destinationProperty.Name, out var sourceProperty)
                            ? sourceProperty.GetValue(source)
                            : null;

                if (value == null)
                    continue;

                var destinationType = destinationProperty.PropertyType;

                // Propriedades compatíveis
                if (destinationType.IsAssignableFrom(value.GetType()))
                {
                    destinationProperty.SetValue(destination, value);
                    continue;
                }

                // Tratamento de listas
                if (value is IEnumerable sourceList && destinationType.IsGenericType && destinationType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var destinationItemType = destinationType.GetGenericArguments()[0];
                    var destinationList = (IList)Activator.CreateInstance(destinationType)!;
                    foreach (var item in sourceList)
                    {
                        if (item == null)
                            continue;

                        var mappedItem = typeof(MapperHelper).GetMethod(nameof(Map))!.MakeGenericMethod(destinationItemType).Invoke(null, [item]);
                        destinationList.Add(mappedItem);
                    }
                    destinationProperty.SetValue(destination, destinationList);
                }
            }

            return destination;
        }

        public static List<TDestination> MapList<TDestination>(IEnumerable source) where TDestination : new()
        {
            ArgumentNullException.ThrowIfNull(source);
            var result = new List<TDestination>();
            foreach (var item in source)
            {
                if (item == null)
                    continue;

                result.Add(Map<TDestination>(item));
            }

            return result;
        }

        private static object? GetNestedValue(object source, string path)
        {
            object? current = source;
            foreach (var propertyName in path.Split('.'))
            {
                if (current == null)
                    return null;

                var property = current.GetType().GetProperty(propertyName);

                if (property == null || !property.CanRead)
                    return null;

                current = property.GetValue(current);
            }
            return current;
        }
    }
}
