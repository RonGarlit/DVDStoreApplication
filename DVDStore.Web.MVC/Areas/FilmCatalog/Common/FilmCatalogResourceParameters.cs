/**********************************************************************************
**
**  DVDStore Application v1.0
**
**  Copyright 2024
**  Developed by:
**     Ronald Garlit.
**
**  This software was created for educational purposes.
**
**  Use is subject to license terms.
***********************************************************************************
**
**  FileName: FilmCatalogResourceParameters.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the FilmCatalogResourceParameters class for the 
**  DVDStore web application.
**
**  The FilmCatalogResourceParameters class defines query parameters for film catalog resources.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-05-28		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

namespace DVDStore.Web.MVC.Areas.FilmCatalog.Common
{
    public class FilmCatalogResourceParameters
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        // This class includes properties for pagination (PageNumber and PageSize),
        // a search query (SearchQuery), filtering by genre (Genre) and rating (Rating),
        // and sorting (SortOrder).

        public string SearchQuery { get; set; }
        public string Rating { get; set; }
        public string SortOrder { get; set; } = "Title"; // Default sort order
    }
}