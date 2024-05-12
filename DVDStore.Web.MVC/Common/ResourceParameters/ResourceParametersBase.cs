//*****************************************************************************
//  SpHoa v1.0
//
//  Copyright 2022
//  Developed by:
//     Ronald Garlit.
//
//
//  Use is subject to license terms.
//*****************************************************************************
//
//  FileName: ResourceParametersBase.cs (Common Code)
//  Version: 0.1
//  Author: Ronald Garlit
//
//  Description:
//
//  Abstract Base class used for resources and pages.  Override and extend as needed.
//
//  Change History
//
//  WHEN			WHO        WHAT
//-----------------------------------------------------------------------------
//  2022-07-06		RGARLIT     STARTED DEVELOPMENT
//****************************************************************************/

namespace DVDStore.Web.MVC.Common.ResourceParameters
{
    public abstract class ResourceParametersBase
    {
        #region Protected Fields
        //=====================================================================
        // Setup default values here
        //=====================================================================
        // Restricting Max Page Size to this value
        protected const int maxPageSize = 100;
        // Set basic page size to this default value
        protected int _pageSize = 10;

        #endregion Protected Fields

        #region Public Properties

        public abstract string? Fields { get; set; }
        public abstract int MaxPageSize { get;}
        public abstract string OrderBy { get; set; }
        public abstract int PageNumber { get; set; }
        public abstract int PageSize { get; set; }
        public abstract string? SearchQuery { get; set; }


        #endregion Public Properties
    }
}