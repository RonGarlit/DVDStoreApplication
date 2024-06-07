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
    [Area("Security")]
    [Authorize(Roles = "Administrator")]
    [Route("Security/[controller]/[action]")]
    public class UserController : Controller
    {
        #region Private Fields

        private readonly ISecurityUnitOfWork _securityDbWork;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<UserController> _logger;

        #endregion Private Fields

        #region Public Constructors

        public UserController(ISecurityUnitOfWork securityDbWork, SignInManager<ApplicationUser> signInManager, ILogger<UserController> logger)
        {
            _securityDbWork = securityDbWork;
            _signInManager = signInManager;
            _logger = logger;
        }

        #endregion Public Constructors

        #region Private Properties

        private static string? BTLSearchQuery { get; set; }

        #endregion Private Properties

        #region Public Methods

        [HttpGet("{id?}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(string? id)
        {
            try
            {
                _logger.LogInformation("User Delete Page Entered for ID: {UserIdToDelete}", id);
                var user = _securityDbWork.User.GetUser(id!);
                if (user == null)
                {
                    _logger.LogWarning("User Delete Page Entered for ID: {UserIdToDelete} - User Not Found", id);
                    return NotFound("User Not Found");
                }
                // Pass Search Query parameter forward for "Back To List" tag helper
                ViewBag.IndexSearchQuery = BTLSearchQuery;
                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error accessing User Delete page for ID: {UserIdToDelete}", id);
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        [HttpPost("{id?}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteConfirmed(string? id)
        {
            _logger.LogInformation("User Delete Confirmed for ID: {UserIdToDelete}", id);
            var user = _securityDbWork.User.GetUser(id!);
            if (user != null)
            {
                _logger.LogInformation("User Delete Confirmed for ID: {UserIdToDelete} - User Deleted", id);
                _securityDbWork.User.DeleteUser(user);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id?}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                _logger.LogInformation("User Details Page Entered for ID: {UserIdToDetail}", id);
                var user = _securityDbWork.User.GetUser(id);
                if (user == null)
                {
                    _logger.LogWarning("User Details Page for ID: {UserIdToDetail} - User Not Found", id);
                    return NotFound("User Not Found");
                }
                var roles = _securityDbWork.Role.GetRoles();
                var userRoles = await _signInManager.UserManager.GetRolesAsync(user);
                var roleItems = roles.Select(role =>
                    new SelectListItem(
                        role.Name,
                        role.Id,
                        userRoles.Any(ur => ur.Contains(role.Name!)))).ToList();

                var vm = new DetailsUserModel
                {
                    User = user,
                    Roles = roleItems
                };
                // Pass Search Query parameter forward for "Back To List" tag helper
                ViewBag.IndexSearchQuery = BTLSearchQuery;
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading User Details page for ID: {UserIdToDetail}", id);
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        [HttpGet("{id?}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                _logger.LogInformation("User Edit Page Entered for ID: {UserIdToEdit}", id);
                var user = _securityDbWork.User.GetUser(id);
                if (user == null)
                {
                    _logger.LogWarning("User Edit Page for ID: {UserIdToEdit} - User Not Found", id);
                    return NotFound("User Not Found");
                }
                var roles = _securityDbWork.Role.GetRoles();
                var userRoles = await _signInManager.UserManager.GetRolesAsync(user);
                var roleItems = roles.Select(role =>
                    new SelectListItem(
                        role.Name,
                        role.Id,
                        userRoles.Any(ur => ur.Contains(role.Name!)))).ToList();

                var vm = new EditUserViewModel
                {
                    User = user,
                    Roles = roleItems
                };
                // Pass Search Query parameter forward for "Back To List" tag helper
                ViewBag.IndexSearchQuery = BTLSearchQuery;
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error accessing User Edit page for ID: {UserIdToEdit}", id);
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Index([FromQuery] int pageNo = 1,
                                    [FromQuery] int pageSize = 10,
                                    [FromQuery] string? SearchQuery = null)
        {
            try
            {
                _logger.LogInformation("User Administration Page Entered");
                _logger.LogDebug("Page Number: {PageNumber}, Page Size: {PageSize}, Search Query: {SearchQuery}", pageNo, pageSize, SearchQuery);
                var UsersResourceParameters = new UsersResourceParameters
                {
                    PageNumber = pageNo,
                    PageSize = pageSize,
                    SearchQuery = SearchQuery,
                    OrderBy = "LastName"
                };

                UsersPagedModel<ApplicationUser> data = _securityDbWork.User.GetUsersPagedList(UsersResourceParameters);
                // Get data for filter Select boxes in UI

                // Prep some needed ViewBag Variables
                ViewBag.SearchQuery = UsersResourceParameters.SearchQuery ?? "";

                // Save the search query parameter forward so that it can be added to the "Back To List"
                // tag helpers where required through ViewBag.
                ViewBag.BTLSearchQuery = ViewBag.SearchQuery;

                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading User Administration page - {ErrorMessage}", ex.Message);
                // Return a custom error view or a standard error response as needed
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> OnPostAsync(EditUserViewModel data)
        {
            _logger.LogInformation("User Edit Page Post Entered for ID: {UserIdToEdit}", data.User!.Id);
            var user = _securityDbWork.User.GetUser(data.User!.Id);
            if (user == null)
            {
                _logger.LogWarning("User Edit Page Post Entered for ID: {UserIdToEdit} - User Not Found", data.User!.Id);
                return NotFound();
            }

            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);

            // Loop through the roles in ViewModel
            // Check if the Role is Assigned In DB
            // If Assigned -> Do Nothing
            // If Not Assigned -> Add Role

            var rolesToAdd = new List<string>();
            var rolesToDelete = new List<string>();

            foreach (var role in data.Roles!)
            {
                var assignedInDb = userRolesInDb.FirstOrDefault(ur => ur == role.Text);
                if (role.Selected)
                {
                    if (assignedInDb == null)
                    {
                        rolesToAdd.Add(role.Text);
                    }
                }
                else
                {
                    if (assignedInDb != null)
                    {
                        rolesToDelete.Add(role.Text);
                    }
                }
            }

            if (rolesToAdd.Count != 0)
            {
                await _signInManager.UserManager.AddToRolesAsync(user, rolesToAdd);
            }

            if (rolesToDelete.Count != 0)
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToDelete);
            }

            user.FirstName = data.User.FirstName;
            user.LastName = data.User.LastName;

            _securityDbWork.User.UpdateUser(user);

            return RedirectToAction("index");
        }

        #endregion Public Methods
    }
}