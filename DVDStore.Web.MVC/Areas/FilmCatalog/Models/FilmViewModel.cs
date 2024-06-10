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
**  FileName: FilmViewModel.cs (DVDStore Application)
**  Version: 1.0
**  Author: Ronald Garlit
**  
**  Description: 
**  This file contains the FilmViewModel class which represents the view model
**  for film details in the DVDStore application. It includes properties such as
**  Film ID, Title, Description, Release Year, Language ID, Rental Duration,
**  Rental Rate, Length, Replacement Cost, Rating, Special Features, and Last Update.
**
**  Change History
**
**  WHEN            WHO          WHAT
**---------------------------------------------------------------------------------
**  2024-05-31      RGARLIT      STARTED DEVELOPMENT
***********************************************************************************/

using System.ComponentModel.DataAnnotations;

namespace DVDStore.Web.MVC.Areas.FilmCatalog.Models
{
    public class FilmViewModel
    {
        [Key]
        [Required]
        [Display(Name = "Film ID")]
        public int Filmid { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [MaxLength(2147483647)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [MaxLength(4)]
        [Display(Name = "Release Year")]
        [RegularExpression(@"\d{4}", ErrorMessage = "Please enter a valid four-digit year.")]
        public string Releaseyear { get; set; }

        [Required]
        [Display(Name = "Language ID")]
        public byte Languageid { get; set; }

        [Display(Name = "Original Language ID")]
        public byte? Originallanguageid { get; set; }

        [Required]
        [Display(Name = "Rental Duration")]
        public byte Rentalduration { get; set; }

        [Required]
        [Display(Name = "Rental Rate")]
        public decimal Rentalrate { get; set; }

        [Display(Name = "Length")]
        public short? Length { get; set; }

        [Required]
        [Display(Name = "Replacement Cost")]
        public decimal Replacementcost { get; set; }

        [MaxLength(10)]
        [Display(Name = "Rating")]
        public string Rating { get; set; }

        [MaxLength(255)]
        [Display(Name = "Special Features")]
        public string Specialfeatures { get; set; }

        [Required]
        [Display(Name = "Last Update")]
        public DateTime Lastupdate { get; set; }
    }
}
/*
 
           .            .                     .
                  _        .                          .            (
                 (_)        .       .                                     .
  .        ____.--^.
   .      /:  /    |                               +           .         .
         /:  `--=--'   .                                                .
  LS    /: __[\==`-.___          *           .
       /__|\ _~~~~~~   ~~--..__            .             .
       \   \|::::|-----.....___|~--.                                 .
        \ _\_~~~~~-----:|:::______//---...___
    .   [\  \  __  --     \       ~  \_      ~~~===------==-...____
        [============================================================-
        /         __/__   --  /__    --       /____....----''''~~~~      .
  *    /  /   ==           ____....=---='''~~~~ .
      /____....--=-''':~~~~                      .                .
      .       ~--~         Kuat Drive Yard's Imperial-class Star Destroyer
                     .                                   .           .
                          .                      .             +
        .     +              .                                       <=>
                                               .                .      .
   .                 *                 .                *                ` -


 */