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
**  FileName: ResourceParametersBase.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the ResourceParametersBase class for the DVDStore web application.
**
**  The ResourceParametersBase class is an abstract base class used for resources and pages.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-04-06		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

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
        public abstract int MaxPageSize { get; }
        public abstract string OrderBy { get; set; }
        public abstract int PageNumber { get; set; }
        public abstract int PageSize { get; set; }
        public abstract string? SearchQuery { get; set; }

        #endregion Public Properties
    }
}