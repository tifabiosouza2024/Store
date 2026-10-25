using FS.Store.Model.Helpers;

namespace FS.Store.Model.Extensions
{
    public static class MapperExtensions
    {
        public static TDestination ToDto<TDestination>(this object source) where TDestination : new()
        {
            return MapperHelper.Map<TDestination>(source);
        }
    }
}
