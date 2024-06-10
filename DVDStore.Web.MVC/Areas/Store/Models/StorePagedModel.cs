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
**  FileName: StorePagedModel.cs (DVDStore Application)
**  Version: 1.0
**  Author: Ronald Garlit
**
**  Description:
**  This file contains the StorePagedModel class which represents a paginated model
**  for handling the pagination of store items. It includes properties for the
**  current page, total pages, page size, and total count. The class also provides
**  an asynchronous method for creating a paginated model from an IQueryable source.
**
**  Change History
**
**  WHEN            WHO          WHAT
**---------------------------------------------------------------------------------
**  2024-05-31      RGARLIT      STARTED DEVELOPMENT
***********************************************************************************/

using Microsoft.EntityFrameworkCore;

namespace DVDStore.Web.MVC.Areas.Store.Models
{
    /// <summary>
    /// StorePagedModel class represents a paginated model for handling the pagination of store items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StorePagedModel<T> : List<T>
    {
        #region Public Constructors

        public StorePagedModel(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        #endregion Public Constructors

        #region Public Properties

        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        #endregion Public Properties

        #region Public Methods
        /// <summary>
        /// CreateAsync method creates a paginated model from an IQueryable source.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<StorePagedModel<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new StorePagedModel<T>(items, count, pageNumber, pageSize);
        }

        #endregion Public Methods
    }
}

/*

  THE VEX FILES  #1                    /-_-/-  \
      _                               ///--_-\\\\
     /-\.-.                           |// " \\\\|
    //=" \\\                          \/__ __\///
   /=/_  _\=                           \-/ /- |/
  //| ,  ,/=              .---.  ) )   // /   .)
 / /C   \ \ \       ( (  /_  _ \      |(__)\ /|
 |  |   "  | |           \\></ /      |_---'__/
 \\_\\_ ^ ///             \"  /      _/ \--'/ \__
  ___/ \-/\___         ___/\~/\___  / | // /\ |  \
 /    \/\\/   \           \    \   /  |/|| \_\|  |
 \  \_  \\   \ \           \_/_/\ /  / ||| _\  | |
  \ ==\-._..-| |           / |\  /  /|/ ||  \\ | |
   \  """_.--" /         _/ _| \   / || \/    \| |
   /`--""\"---"                 `-'  ||=======|/ |
  / /_/  /\ \\                       |\  .   /\_/
  \_____/  \__\                      |/| |_  \_/|
   \        /                        |:|  |   |\|
   /________\                        |:|  |   |:|
      \ ) )                          |:|  |   |:|
      / |/                           |:|  |   |:|
     .\ |                            |:|  |   |:|
    /\/_|                             "|__|___|"
   /_ |  \_                           _.' |' |
     \_\\__\                         (___(___)



 */