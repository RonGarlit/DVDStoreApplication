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
**  FileName: UsersPagedModel.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the UsersPagedModel class for the DVDStore web application.
**
**  The UsersPagedModel class is used for pagination of user data.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-05-08		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

using DVDStore.Web.MVC.Common;

namespace DVDStore.Web.MVC.Areas.Security.Models
{
    public class UsersPagedModel<T> : List<T>, IPagedList
    {
        #region Public Constructors

        public UsersPagedModel(List<T> items,
                                  int totalCount,
                                  int pageNumber,
                                  int pageSize)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            AddRange(items);
        }

        #endregion Public Constructors

        #region Public Properties

        public int CurrentPage { get; private set; }
        public bool HasNext => CurrentPage < TotalPages;
        public bool HasPrevious => CurrentPage > 1;
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public static UsersPagedModel<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new UsersPagedModel<T>(items, count, pageNumber, pageSize);
        }

        #endregion Public Methods
    }
}