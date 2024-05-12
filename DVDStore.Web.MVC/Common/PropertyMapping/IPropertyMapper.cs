namespace DVDStore.Web.MVC.Common.PropertyMapping.BaseMappingCode
{
    public interface IPropertyMapper
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
        bool ValidMappingExistsFor<TSource, TDestination>(string fields);
    }
}