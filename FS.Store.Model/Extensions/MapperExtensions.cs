using FS.Store.Model.Helpers;
using System.Collections;

namespace FS.Store.Model.Extensions
{
    public static class MapperExtensions
    {
        public static TDestination ToDto<TDestination>(this object source) where TDestination : new()
        {
            ArgumentNullException.ThrowIfNull(source);

            if (source is IEnumerable collection && source is not string)
            {
                var destinationType = typeof(TDestination);

                if (destinationType.IsGenericType && destinationType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var itemType = destinationType.GetGenericArguments()[0];

                    return (TDestination)typeof(MapperHelper)
                        .GetMethod(nameof(MapperHelper.MapList))!
                        .MakeGenericMethod(itemType)
                        .Invoke(null, [collection])!;
                }
            }

            return MapperHelper.Map<TDestination>(source);
        }
    }
}
