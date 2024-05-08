// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

// New global suppression for the entire project
[assembly: SuppressMessage("Naming", "VSSpell001:Spell Check", Justification = "Ignore spelling issues project-wide", Scope = "module")]
[assembly: SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = "Global suppression for primary constructor suggestion", Scope = "module")]