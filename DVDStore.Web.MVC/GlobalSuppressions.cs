// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

// New global suppression for the entire project
[assembly: SuppressMessage("Naming", "VSSpell001:Spell Check", Justification = "Ignore spelling issues project-wide", Scope = "module")]
[assembly: SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = "Global suppression for primary constructor suggestion", Scope = "module")]
[assembly: SuppressMessage("Style", "IDE0305:Simplify collection initialization", Justification = "<Pending>", Scope = "module")]
[assembly: SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "<Pending>")]
[assembly: SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "<Pending>", Scope = "module")]
[assembly: SuppressMessage("Major Code Smell", "S3928:Parameter names used into ArgumentException constructors should match an existing one ", Justification = "<Pending>", Scope = "member", Target = "~M:DVDStore.Web.MVC.Common.Extensions.IQueryableExtensions.ApplySort``1(System.Linq.IQueryable{``0},System.String,System.Collections.Generic.Dictionary{System.String,DVDStore.Web.MVC.Common.PropertyMapping.BaseMappingCode.PropertyMappingValue})~System.Linq.IQueryable{``0}")]
[assembly: SuppressMessage("Usage", "CA2208:Instantiate argument exceptions correctly", Justification = "<Pending>", Scope = "member", Target = "~M:DVDStore.Web.MVC.Common.Extensions.IQueryableExtensions.ApplySort``1(System.Linq.IQueryable{``0},System.String,System.Collections.Generic.Dictionary{System.String,DVDStore.Web.MVC.Common.PropertyMapping.BaseMappingCode.PropertyMappingValue})~System.Linq.IQueryable{``0}")]
[assembly: SuppressMessage("Sonar Code Smell", "S4144:DuplicateMethodBody", Justification = "Intentional duplication for maintenance reasons or pending refactoring.")]
[assembly: SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded", Justification = "<Pending>", Scope = "member", Target = "~F:DVDStore.Web.MVC.Areas.Identity.Pages.Account.Manage.EnableAuthenticatorModel.AuthenticatorUriFormat")]
[assembly: SuppressMessage("Minor Code Smell", "S3459:Unassigned members should be removed", Justification = "<Pending>", Scope = "member", Target = "~P:DVDStore.Web.MVC.Areas.FilmCatalog.Controllers.FilmsCatalogController.BTLSearchQuery")]
[assembly: SuppressMessage("Minor Code Smell", "S3459:Unassigned members should be removed", Justification = "<Pending>", Scope = "member", Target = "~P:DVDStore.Web.MVC.Areas.Security.Controllers.UserController.BTLSearchQuery")]
[assembly: SuppressMessage("Major Code Smell", "S6967:ModelState.IsValid should be called in controller actions", Justification = "Reviewed: our controllers use ModelState.IsValid where necessary; other usages are considered and handled appropriately.", Scope = "namespaceanddescendants", Target = "~N:DVDStore.Web.MVC")]
[assembly: SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed", Justification = "Reviewed: our controllers use ModelState.IsValid where necessary; other usages are considered and handled appropriately.", Scope = "namespaceanddescendants", Target = "~N:DVDStore.Web.MVC")]