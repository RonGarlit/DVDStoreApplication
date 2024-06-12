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
**  FileName: UsersResourceParameters.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the UsersResourceParameters class for the DVDStore web application.
**
**  The UsersResourceParameters class is used for defining query parameters for user resources.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-05-08		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

using DVDStore.Web.MVC.Common.ResourceParameters;

namespace DVDStore.Web.MVC.Common
{
    public class UsersResourceParameters : ResourceParametersBase
    {
        /// <summary>
        /// Data Shaping feature!!!
        /// Used to return only specific columns from table minimizing the
        /// data exposed and sent over the wire. - I normally use this in API
        /// development
        /// </summary>
        public override string? Fields { get; set; }

        /// <summary>
        /// Max Page Size is set in the base class.  Default is 100.
        /// </summary>
        public override int MaxPageSize
        {
            get
            {
                return _pageSize;
            }
        }

        /// <summary>
        /// Put Id as the default column to OrderBy here.  It can be over-ridden
        /// if appropriate changes are made in the TextbookPropertyMapper class
        /// property mapping section in the private fields region.
        /// </summary>
        public override string OrderBy { get; set; } = "Id";

        /// <summary>
        /// This provides the current or needed page number.
        /// Default value is one (1)
        /// </summary>
        public override int PageNumber { get; set; } = 1;

        /// <summary>
        /// Used to override the Page Size that is set in the base class.  The
        /// base class sets the default page size is 10 and default max page
        /// size is 100.  To go larger the base class will need changing.
        /// </summary>
        public override int PageSize
        {
            get
            {
                return _pageSize;
            }

            set => _pageSize = value > maxPageSize ? maxPageSize : value;
        }

        /// <summary>
        /// This is where the search query parameters are passed in.  Note that
        /// you can only search the hard coded columns setup in the
        /// TextbookRepository method using the PageList class.
        /// </summary>
        public override string? SearchQuery { get; set; }
    }
}

/*
           |~
           |.---.
          .'_____`. /\
          |~xxxxx~| ||
          |_  #  _| ||
     .------`-#-'-----.
    (___|\_________/|_.`.
     /  | _________ | | |
    /   |/   _|_   \| | |
   /   /X|  __|__  |/ `.|
  (  --< \\/    _\//|_ |`.
  `.    ~----.-~=====,:=======
    ~-._____/___:__(``/| |
      |    |      XX|~ | |
       \__/======| /|  `.|
       |_\|\    /|/_|    )
       |_   \__/   _| .-'
       | \ .'||`. / |(_|
    LS |  ||.'`.||  |   )
       |  `'|  |`'  |  /
       |    |  |    |\/   Boba Fett

 */