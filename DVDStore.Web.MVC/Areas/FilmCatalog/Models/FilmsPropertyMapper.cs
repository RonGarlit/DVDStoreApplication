using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace DVDStore.Web.MVC.Areas.FilmCatalog.Common
{
    public static class FilmsPropertyMapper
    {
        private static Dictionary<string, string> _propertyMapping = new Dictionary<string, string>
        {
            { "Title", "Title" },
            { "Genre", "Genre" },
            { "Rating", "Rating" },
            { "RentalRate", "RentalRate" },
            { "Length", "Length" }
        };

        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderByQueryString)
        {
            if (!source.Any())
                return source;

            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source;

            var orderParams = orderByQueryString.Trim().Split(',');
            var orderQuery = string.Join(", ", orderParams.Select(p => {
                var propertyFromQueryName = p.Split(" ")[0];
                var sortingOrder = p.EndsWith(" desc") ? "descending" : "ascending";
                var mappedProperty = _propertyMapping.ContainsKey(propertyFromQueryName) ? _propertyMapping[propertyFromQueryName] : propertyFromQueryName;
                return $"{mappedProperty} {sortingOrder}";
            }));

            return source.OrderBy(orderQuery);
        }
    }
}
