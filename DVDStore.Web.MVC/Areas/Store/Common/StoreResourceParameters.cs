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
**  FileName: StoreResourceParameters.cs (DVDStore Application)
**  Version: 1.0
**  Author: Ronald Garlit
**  
**  Description: 
**  This file contains the StoreResourceParameters class which is used to
**  encapsulate the parameters for resource requests in the store area of the
**  DVDStore application. It includes properties such as Page Number, Page Size,
**  Search Query, Category, Rating, and Sort Order.
**
**  Change History
**
**  WHEN            WHO          WHAT
**---------------------------------------------------------------------------------
**  2024-05-31      RGARLIT      STARTED DEVELOPMENT
***********************************************************************************/

namespace DVDStore.Web.MVC.Areas.Store.Common
{
    /// <summary>
    /// Store Resource Parameters class
    /// </summary>
    public class StoreResourceParameters
    {
        #region Private Fields

        // max page size field
        private const int maxPageSize = 50;
        // page size field
        private int _pageSize = 10;

        #endregion Private Fields

        #region Public Properties
        /// <summary>
        /// Category property
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Page number property
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Page size property
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
        /// <summary>
        /// Rating property
        /// </summary>
        public string? Rating { get; set; }
        /// <summary>
        /// Search query property
        /// </summary>
        public string? SearchQuery { get; set; }
        /// <summary>
        /// Sort order property
        /// </summary>
        public string? SortOrder { get; set; }

        #endregion Public Properties
    }
}

/*
                    (`'`'`'`'`)
                     |       |
                     |       |
                     |       |
    -----..        (()----   |
   |        ||     (_        |
   |        ||       |       |
   |        ||       |       |
   |        ||       /\   ..--
   '--------''   /\  ||-''    \
      /   \      \ \//   ,,   \---.
   .---------.    \./ |~| /__\  \  |
___|_________|__|""-.___ / ||   |  |
|               | .-----'  ||   |  |
|               |CC.-----.      |  |
|               |  '-----'      |  |-ABG
                                |  |
*/