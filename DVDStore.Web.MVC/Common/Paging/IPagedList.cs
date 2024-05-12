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
//  FileName: PagedList.cs (Common Code)
//  Version: 0.1
//  Author: Ronald Garlit
//
//  Description:
//
//  My PagedList class used for pagination work for UI, Repository and Database
//  calls.
//
//  Change History
//
//  WHEN			WHO        WHAT
//-----------------------------------------------------------------------------
//  2022-06-30		RGARLIT     STARTED DEVELOPMENT
//****************************************************************************/

namespace DVDStore.Web.MVC.Common
{
    public interface IPagedList
    {
        int CurrentPage { get; }
        bool HasNext { get; }
        bool HasPrevious { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
    }
}