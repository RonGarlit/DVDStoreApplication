// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

// New global suppression for the entire project
[assembly: SuppressMessage("Naming", "VSSpell001:Spell Check", Justification = "Ignore spelling issues project-wide", Scope = "module")]
[assembly: SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = "Global suppression for primary constructor suggestion", Scope = "module")]
[assembly: SuppressMessage("Style","IDE0305:Simplify collection initialization", Justification = "<Pending>", Scope = "module")]
[assembly: SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "<Pending>")]
[assembly: SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "<Pending>", Scope = "module")]
[assembly: SuppressMessage("Major Code Smell", "S3928:Parameter names used into ArgumentException constructors should match an existing one ", Justification = "<Pending>", Scope = "member", Target = "~M:DVDStore.Web.MVC.Common.Extensions.IQueryableExtensions.ApplySort``1(System.Linq.IQueryable{``0},System.String,System.Collections.Generic.Dictionary{System.String,DVDStore.Web.MVC.Common.PropertyMapping.BaseMappingCode.PropertyMappingValue})~System.Linq.IQueryable{``0}")]
[assembly: SuppressMessage("Usage", "CA2208:Instantiate argument exceptions correctly", Justification = "<Pending>", Scope = "member", Target = "~M:DVDStore.Web.MVC.Common.Extensions.IQueryableExtensions.ApplySort``1(System.Linq.IQueryable{``0},System.String,System.Collections.Generic.Dictionary{System.String,DVDStore.Web.MVC.Common.PropertyMapping.BaseMappingCode.PropertyMappingValue})~System.Linq.IQueryable{``0}")]
