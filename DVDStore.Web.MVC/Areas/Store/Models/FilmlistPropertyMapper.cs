using DVDStore.DAL;
using DVDStore.Web.MVC.Common.Exceptions;
using DVDStore.Web.MVC.Common.PropertyMapping;
using DVDStore.Web.MVC.Common.PropertyMapping.BaseMappingCode;

namespace DVDStore.Web.MVC.Areas.Store.Models
{
    public class FilmlistPropertyMapper
    {
        private readonly Dictionary<string, PropertyMappingValue> _filmlistPropertyMapping =
            new(StringComparer.OrdinalIgnoreCase)
            {
                { "Fid", new PropertyMappingValue(new List<string> { "Fid" }) },
                { "Title", new PropertyMappingValue(new List<string> { "Title" }) },
                { "Description", new PropertyMappingValue(new List<string> { "Description" }) },
                { "Category", new PropertyMappingValue(new List<string> { "Category" }) },
                { "Price", new PropertyMappingValue(new List<string> { "Price" }) },
                { "Length", new PropertyMappingValue(new List<string> { "Length" }) },
                { "Rating", new PropertyMappingValue(new List<string> { "Rating" }) },
                { "Actors", new PropertyMappingValue(new List<string> { "Actors" }) }
            };

        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public FilmlistPropertyMapper()
        {
            _propertyMappings.Add(new PropertyMapping<Filmlist, Filmlist>(_filmlistPropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First().MappingDictionary;
            }

            throw new PropertyMappingException($"Cannot find exact property mapping instance for <{typeof(TSource)}, {typeof(TDestination)}>");
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(',');

            foreach (var field in fieldsAfterSplit)
            {
                var trimmedField = field.Trim();
                int indexOfFirstSpace = trimmedField.IndexOf(' ');
                var propertyName = indexOfFirstSpace == -1 ? trimmedField : trimmedField.Remove(indexOfFirstSpace);

                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
