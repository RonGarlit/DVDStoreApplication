using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DVDStore.Web.MVC.Areas.Identity.Data;
using DVDStore.Web.MVC.Areas.Security.Models;
using DVDStore.Web.MVC.Common;
using System.Data;
using DVDStore.Web.MVC.Areas.Security.Repositories;

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

        #endregion Private Fields

        #region Public Constructors

        public UserController(ISecurityUnitOfWork securityDbWork, SignInManager<ApplicationUser> signInManager)
        {
            _securityDbWork = securityDbWork;
            _signInManager = signInManager;
        }

        #endregion Public Constructors

        #region Private Properties

        private static string? BTLSearchQuery { get; set; }

        #endregion Private Properties

        #region Public Methods

        [HttpGet("{id?}")]
        [Authorize(Roles = "Administrator")]
        public Task<IActionResult> Delete(string? id)
        {
            var user = _securityDbWork.User.GetUser(id!);
            if (user == null)
            {
                return Task.FromResult<IActionResult>(NotFound());
            }
            // Pass Search Query parameter forward for "Back To List" tag helper
            ViewBag.IndexSearchQuery = BTLSearchQuery;
            return Task.FromResult<IActionResult>(View(user));
        }

        [HttpPost("{id?}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteConfirmed(string? id)
        {
            var user = _securityDbWork.User.GetUser(id!);
            if (user != null)
            {
                _securityDbWork.User.DeleteUser(user);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id?}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(string id)
        {
            var user = _securityDbWork.User.GetUser(id);
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

        [HttpGet("{id?}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = _securityDbWork.User.GetUser(id);
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

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Index([FromQuery] int pageNo = 1,
                                    [FromQuery] int pageSize = 10,
                                    [FromQuery] string? SearchQuery = null)
        {
            var UsersResourceParameters = new UsersResourceParameters
            {
                PageNumber = pageNo,
                PageSize = pageSize,
                SearchQuery = SearchQuery,
                OrderBy = "LastName"
            };

            UsersPagedModel<ApplicationUser> data = _securityDbWork.User.GetUsersPagedList(UsersResourceParameters);
            //  Get data for filter Select boxes in UI

            // Prep some needed ViewBag Variables
            ViewBag.SearchQuery = "";

            // Load up the ViewBag variables with data from the filters and search boxes
            if (UsersResourceParameters.SearchQuery != null)
            {
                ViewBag.SearchQuery = UsersResourceParameters.SearchQuery;
            }

            // Save the search query parameter forward so that is can be added to the "Back To List"
            // tag helpers where required through ViewBag. 
            BTLSearchQuery = ViewBag.SearchQuery;


            return View(data);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> OnPostAsync(EditUserViewModel data)
        {
            var user = _securityDbWork.User.GetUser(data.User!.Id);
            if (user == null)
            {
                return NotFound();
            }

            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);

            //Loop through the roles in ViewModel
            //Check if the Role is Assigned In DB
            //If Assigned -> Do Nothing
            //If Not Assigned -> Add Role

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