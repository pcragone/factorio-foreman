﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foreman
{
	public class Mod
	{
		public String name = "";
		public String title = "";
		public String version = "";
		public Version parsedVersion;
		public String dir = "";
		public String description = "";
		public String author = "";
		public List<String> dependencies = new List<String>();
		public List<ModDependency> parsedDependencies = new List<ModDependency>();
		public bool enabled = true;

		public bool SatisfiesDependency(ModDependency dep)
		{
			if (name != dep.ModName)
			{
				return false;
			}
			if (dep.Version != null)
			{
				if (dep.VersionType == DependencyType.EqualTo
					&& parsedVersion != dep.Version)
				{
					return false;
				}
				if (dep.VersionType == DependencyType.GreaterThan
					&& parsedVersion <= dep.Version)
				{
					return false;
				}
				if (dep.VersionType == DependencyType.GreaterThanOrEqual
					&& parsedVersion < dep.Version)
				{
					return false;
				}
			}
			return true;
		}

		public bool DependsOn(Mod mod)
		{
			foreach (ModDependency dep in parsedDependencies)
			{
				if (mod.SatisfiesDependency(dep))
				{
					return true;
				}
			}
			return false;
		}

		public override string ToString()
		{
			return name;
		}
	}

	public class ModDependency
	{
		public String ModName = "";
		public Version Version;
		public bool Optional = false;
		public DependencyType VersionType = DependencyType.EqualTo;
	}

	public enum DependencyType
	{
		EqualTo,
		GreaterThan,
		GreaterThanOrEqual
	}
}
