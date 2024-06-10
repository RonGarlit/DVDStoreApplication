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
**  FileName: UserController.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the UserController class for the DVDStore web application.
**
**  The UserController class handles user-related actions, such as creating, updating, and deleting users.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-05-08		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

using DVDStore.Web.MVC.Areas.Identity.Data;
using DVDStore.Web.MVC.Areas.Security.Models;
using DVDStore.Web.MVC.Areas.Security.Repositories;
using DVDStore.Web.MVC.Common;
using DVDStore.Web.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Diagnostics;

namespace DVDStore.Web.MVC.Areas.Security.Controllers
{
    /// <summary>
    /// User Controller for User Administration
    /// </summary>
    [Area("Security")]
    [Authorize(Roles = "Administrator")]
    [Route("Security/[controller]/[action]")]
    public class UserController : Controller
    {
        #region Private Fields

        // Logger for Logging
        private readonly ILogger<UserController> _logger;

        // Security Database Unit of Work using the UserRepository and RoleRepository
        private readonly ISecurityUnitOfWork _securityDbWork;

        // SignInManager for User Authentication
        private readonly SignInManager<ApplicationUser> _signInManager;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructor for User Controller
        /// </summary>
        /// <param name="securityDbWork"></param>
        /// <param name="signInManager"></param>
        /// <param name="logger"></param>
        public UserController(ISecurityUnitOfWork securityDbWork, SignInManager<ApplicationUser> signInManager, ILogger<UserController> logger)
        {
            // Intialize the Security Database Unit of Work, SignInManager and Logger
            _securityDbWork = securityDbWork;
            _signInManager = signInManager;
            _logger = logger;
        }

        #endregion Public Constructors

        #region Private Properties

        /// <summary>
        /// BLTSearchQuery - Back To List Search Query
        /// </summary>
        private static string? BTLSearchQuery { get; set; }

        #endregion Private Properties

        #region Public Methods

        /// <summary>
        /// Delete User Get Method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id?}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(string? id)
        {
            // Try to Delete the User
            try
            {
                // Log the User Delete Page Entry
                _logger.LogInformation("User Delete Page Entered for ID: {UserIdToDelete}", id);
                // Get the User from the Database using the ID through the Security Database Unit of Work
                var user = _securityDbWork.User.GetUser(id!);
                // Check if the User is Null
                if (user == null)
                {
                    // Log the User Not Found
                    _logger.LogWarning("User Delete Page Entered for ID: {UserIdToDelete} - User Not Found", id);
                    // Return a Not Found Response
                    return NotFound("User Not Found");
                }
                // Pass Search Query parameter forward for "Back To List" tag helper
                ViewBag.IndexSearchQuery = BTLSearchQuery;
                // Return the User View
                return View(user);
            }
            // Catch any Exceptions
            catch (Exception ex)
            {
                // Log the Error for the User Delete Page
                _logger.LogError(ex, "Error accessing User Delete page for ID: {UserIdToDelete}", id);
                // Return an Error View
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        /// <summary>
        /// Confirm the User Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id?}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteConfirmed(string? id)
        {
            // log the user being deleted
            _logger.LogInformation("User Delete Confirmed for ID: {UserIdToDelete}", id);
            // Get the User from the Database using the ID through the Security Database Unit of Work
            var user = _securityDbWork.User.GetUser(id!);
            // Check if the User is Null
            if (user != null)
            {
                // Log the User Deleted information
                _logger.LogInformation("User Delete Confirmed for ID: {UserIdToDelete} - User Deleted", id);
                // Delete the User using the Security Database Unit of Work
                _securityDbWork.User.DeleteUser(user);
            }
            // Redirect to the User Index Page
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Details of the User selected
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id?}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(string id)
        {
            // Try to get the User Details
            try
            {
                // Log the User Details Page Entry
                _logger.LogInformation("User Details Page Entered for ID: {UserIdToDetail}", id);
                // Get the User from the Database using the ID through the Security Database Unit of Work
                var user = _securityDbWork.User.GetUser(id);
                // Check if the User is Null
                if (user == null)
                {
                    // Log the User Not Found
                    _logger.LogWarning("User Details Page for ID: {UserIdToDetail} - User Not Found", id);
                    // Return a Not Found Response
                    return NotFound("User Not Found");
                }
                // Get the Roles from the Database using the Security Database Unit of Work
                var roles = _securityDbWork.Role.GetRoles();
                // Get the User Roles from the Database using the SignInManager
                var userRoles = await _signInManager.UserManager.GetRolesAsync(user);
                // Create a List of SelectListItem for the Roles
                var roleItems = roles.Select(role =>
                    new SelectListItem(
                        role.Name,
                        role.Id,
                        userRoles.Any(ur => ur.Contains(role.Name!)))).ToList();
                // Create a Details User Model
                var vm = new DetailsUserModel
                {
                    User = user,
                    Roles = roleItems
                };
                // Pass Search Query parameter forward for "Back To List" tag helper
                ViewBag.IndexSearchQuery = BTLSearchQuery;
                // Return the View Model
                return View(vm);
            }
            // Catch any Exceptions
            catch (Exception ex)
            {
                // Log the Error for the User Details Page
                _logger.LogError(ex, "Error loading User Details page for ID: {UserIdToDetail}", id);
                // Return an Error View
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        /// <summary>
        /// Get Edit User Page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id?}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            // Try to get the User Edit Page
            try
            {
                // Log the User Edit Page Entry for the User ID
                _logger.LogInformation("User Edit Page Entered for ID: {UserIdToEdit}", id);
                // Get the User from the Database using the ID through the Security Database Unit of Work
                var user = _securityDbWork.User.GetUser(id);
                // Check if the User is Null
                if (user == null)
                {
                    // Log the specific User Not Found
                    _logger.LogWarning("User Edit Page for ID: {UserIdToEdit} - User Not Found", id);
                    // Return a Not Found Response
                    return NotFound("User Not Found");
                }
                // Get the Roles from the Database using the Security Database Unit of Work
                var roles = _securityDbWork.Role.GetRoles();
                // Get the User Roles from the Database using the SignInManager
                var userRoles = await _signInManager.UserManager.GetRolesAsync(user);
                // Create a List of SelectListItem for the Roles
                var roleItems = roles.Select(role =>
                    new SelectListItem(
                        role.Name,
                        role.Id,
                        userRoles.Any(ur => ur.Contains(role.Name!)))).ToList();
                // Create a Edit User View Model
                var vm = new EditUserViewModel
                {
                    User = user,
                    Roles = roleItems
                };
                // Pass Search Query parameter forward for "Back To List" tag helper
                ViewBag.IndexSearchQuery = BTLSearchQuery;
                // Return the View Model
                return View(vm);
            }
            // Catch any Exceptions
            catch (Exception ex)
            {
                // Log the Error for the User Edit Page
                _logger.LogError(ex, "Error accessing User Edit page for ID: {UserIdToEdit}", id);
                // Return an Error View
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        /// <summary>
        /// Post Method for User Edit Page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditPostAsync(EditUserViewModel data)
        {
            // Log the User Edit Page Post Entry
            _logger.LogInformation("User Edit Page Post Entered for ID: {UserIdToEdit}", data.User!.Id);
            // Get the User from the Database using the ID through the Security Database Unit of Work
            var user = _securityDbWork.User.GetUser(data.User!.Id);
            // Check if the User is Null
            if (user == null)
            {
                // Log the User Not Found
                _logger.LogWarning("User Edit Page Post Entered for ID: {UserIdToEdit} - User Not Found", data.User!.Id);
                // Return a Not Found Response
                return NotFound();
            }
            // Get the Roles from the Database using the Security Database Unit of Work
            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);

            // Loop through the roles in ViewModel
            // Check if the Role is Assigned In DB
            // If Assigned -> Do Nothing
            // If Not Assigned -> Add Role

            // create lists to hold roles to add and delete
            var rolesToAdd = new List<string>();
            var rolesToDelete = new List<string>();
            // Loop through the Roles in the ViewModel
            foreach (var role in data.Roles!)
            {
                // Check if the Role is Assigned in the Database
                var assignedInDb = userRolesInDb.FirstOrDefault(ur => ur == role.Text);
                if (role.Selected)
                {
                    // Check if the Role is Assigned in the Database
                    if (assignedInDb == null)
                    {
                        // Add the Role to the Roles to Add List
                        rolesToAdd.Add(role.Text);
                    }
                }
                else
                {
                    // Check if the Role is Assigned in the Database
                    if (assignedInDb != null)
                    {
                        // Add the Role to the Roles to Delete List
                        rolesToDelete.Add(role.Text);
                    }
                }
            }
            // Check roles to add
            if (rolesToAdd.Count != 0)
            {
                // Add the Roles to the User
                await _signInManager.UserManager.AddToRolesAsync(user, rolesToAdd);
            }
            // Check roles to delete
            if (rolesToDelete.Count != 0)
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToDelete);
            }
            // Update the User Information
            user.FirstName = data.User.FirstName;
            user.LastName = data.User.LastName;
            // Update the User using the Security Database Unit of Work
            _securityDbWork.User.UpdateUser(user);
            //  Redirect to the User Index Page
            return RedirectToAction("index");
        }

        /// <summary>
        /// Get the User Administration Page with Pagination
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="SearchQuery"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Index([FromQuery] int pageNo = 1,
                                    [FromQuery] int pageSize = 10,
                                    [FromQuery] string? SearchQuery = null)
        {
            // Try to get the User Administration Page
            try
            {
                // Log the User Administration Page Entry
                _logger.LogInformation("User Administration Page Entered");
                // Debug Log the Page Number, Page Size and Search Query
                _logger.LogDebug("Page Number: {PageNumber}, Page Size: {PageSize}, Search Query: {SearchQuery}", pageNo, pageSize, SearchQuery);
                // Get the Users Page Resource Parameters
                var UsersResourceParameters = new UsersResourceParameters
                {
                    PageNumber = pageNo,
                    PageSize = pageSize,
                    SearchQuery = SearchQuery,
                    OrderBy = "LastName"
                };
                // Get the Users Paged Model from the Security Database Unit of Work
                UsersPagedModel<ApplicationUser> data = _securityDbWork.User.GetUsersPagedList(UsersResourceParameters);
                // Get data for filter Select boxes in UI
                // Prep some needed ViewBag Variables
                ViewBag.SearchQuery = UsersResourceParameters.SearchQuery ?? "";

                // Save the search query parameter forward so that it can be added to the "Back To List"
                // tag helpers where required through ViewBag.
                ViewBag.BTLSearchQuery = ViewBag.SearchQuery;
                // Return the User Administration Page
                return View(data);
            }
            // Catch any Exceptions
            catch (Exception ex)
            {
                // Log the Error for the User Administration Page
                _logger.LogError(ex, "Error loading User Administration page - {ErrorMessage}", ex.Message);
                // Return a custom error view or a standard error response as needed
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
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
JRO .   _________     | ROSWELL|   __________   __________  '  |This was |
 .'     |50 years|  ' |HAPPENED|  |Secret UFO| | MAKE THE |  . |the crash|
        |of cover|    |________|  |Bodies!!! | |GOVERNMENT|    |__SITE!__|
        |__up!!__|        ||  " . |__________| |_REVEAL!!_|        ||
  '   _____ ||       @@@@@||          ||           ||              ||
     //ovo\\||   .. @@*.*@@|     )))) |m .    \\\\ |m       //"\\  ||
      \_-_/ m|       @\'/@/|    (~OO~)//      /^v^\|\\  .  //ovo\\ |m
     //\_/\//| .  '  /( )/       \--///       \ o /_//      \ ~ /  //
    //|   |/        //) (  . '  /|  |         /             //  \\//

 * */