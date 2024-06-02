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
**  FileName: PagedList.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the PagedList class for the DVDStore web application.
**
**  The PagedList class is used for pagination work for UI, repository, and database calls.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-04-06		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

namespace DVDStore.Web.MVC.Common
{
    public class PagedList<T> : List<T>, IPagedList
    {
        #region Public Constructors

        public PagedList(List<T> items, int totalCount, int pageNumber, int pageSize)
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

        public static PagedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        #endregion Public Methods
    }
}

/*

                 _                ( Hey down there! Don't )
                /\\               ( you blockheads know )
                \ \\  \__/ \__/  / ( that these things )
                 \ \\ (oo) (oo) /    ( CAN'T crash!! )
                  \_\\/~~\_/~~\_
                 _.-~===========~-._
                (___/_______________)
                   /  \_______/
       ( What a bunch of nuts! )

            _                                      _
      __   |.|       ___________                  | \
     / .|  | |      /  .  > .   \                /   \
    |.' |  |'|     / '  ..  "    \____          |.' ~|
____|  .|__| |____/ .  _________ '    \____..---| .  |__..------_________
RAG .   _________     | ROSWELL|   __________   __________  '  |This was |
 .'     |50 years|  ' |HAPPENED|  |Secret UFO| | MAKE THE |  . |the crash|
        |of cover|    |________|  |Bodies!!! | |GOVERNMENT|    |__SITE!__|
        |__up!!__|        ||  " . |__________| |_REVEAL!!_|        ||
  '   _____ ||       @@@@@||          ||           ||              ||
     //ovo\\||   .. @@*.*@@|     )))) |m .    \\\\ |m       //"\\  ||
      \_-_/ m|       @\'/@/|    (~OO~)//      /^v^\|\\  .  //ovo\\ |m
     //\_/\//| .  '  /( )/       \--///       \ o /_//      \ ~ /  //
    //|   |/        //) (  . '  /|  |         /             //  \\//
 */