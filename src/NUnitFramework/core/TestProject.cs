using System;
using System.IO;

namespace NUnit.Core
{
	/// <summary>
	/// Simple representation of a test project used
	/// in the NUnit core.
	/// </summary>
	[Serializable]
	public struct TestProject
	{
		private string projectPath;
		private string[] assemblies;

		public TestProject( string projectPath, string[] assemblies )
		{
			this.projectPath = projectPath;
			this.assemblies = assemblies;
		}

		public string Name
		{
			get { return Path.GetFileNameWithoutExtension( projectPath ); }
		}

		public string ProjectPath
		{
			get { return projectPath; }
		}

		public string[] Assemblies
		{
			get { return assemblies; }
		}
	}
}
