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
**  FileName: FilmlistViewModel.cs (DVDStore Application)
**  Version: 1.0
**  Author: Ronald Garlit
**  
**  Description: 
**  This file contains the FilmlistViewModel class which represents the view model
**  for the film list in the Store area. It includes properties such as Film ID, 
**  Title, Description, Category, Price, Length, Rating, and Actors with appropriate 
**  data annotations for display names and validation.
**
**  Change History
**
**  WHEN            WHO          WHAT
**---------------------------------------------------------------------------------
**  2024-05-31      RGARLIT      STARTED DEVELOPMENT
***********************************************************************************/
using System.ComponentModel.DataAnnotations;

namespace DVDStore.Web.MVC.Areas.Store.Models
{
    /// <summary>
    /// FilmlistViewModel class represents the view model for the film list in the Store area.
    /// </summary>
    public class FilmlistViewModel
    {
        [Display(Name = "Film ID")]
        public int? Fid { get; set; }

        [MaxLength(255)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [MaxLength(25)]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        [Display(Name = "Length")]
        public short? Length { get; set; }

        [MaxLength(10)]
        [Display(Name = "Rating")]
        public string Rating { get; set; }

        [MaxLength(91)]
        [Display(Name = "Actors")]
        public string Actors { get; set; }
    }
}

/*
         (@@@)  (@@)
       (@@@@@@@@@@@)
      (@@@@@@@@@@@@@@)
     (@@@@@@@@@@@@@@@@)
     (@@         (@@@@@@)
    (@@@ ~~  ~~  (@@@@@@)
    (@@@   (     (@@@@@@@)
   (@@@@    _    (@@@@@@@@)
  (@@@@@    _)   (@@@@@@@)
 (@@@@@@_________(@@@@@@@@)
        /        \
       /         |
       |      |  |              Alice
       |      |__|
       |       | |
       |       | |
       |       | |
       |       | |
       |_______| |
        |[[[[[(__)
        |[[[[[[[[       teb

*/