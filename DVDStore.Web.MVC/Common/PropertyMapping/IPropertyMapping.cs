namespace DVDStore.Web.MVC.Common.PropertyMapping.BaseMappingCode
{
    public interface IPropertyMapping
    {
        Dictionary<string, PropertyMappingValue> MappingDictionary { get; }
    }
}