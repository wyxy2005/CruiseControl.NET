using System;

using Exortech.NetReflector;

using ThoughtWorks.CruiseControl.Remote;

namespace ThoughtWorks.CruiseControl.Core.Builder.Test
{
	[ReflectorType("mockbuildrunner")]
	public class MockBuilder : IBuilder
	{
		public const string BUILDER_OUTPUT = "success";
		public bool HasRun = false;

		public bool ShouldRun(IntegrationResult result)
		{
			return true;
		}

		public void Run(IntegrationResult result)
		{
			result.Status = IntegrationStatus.Success;
			result.Output = BUILDER_OUTPUT;
			HasRun = true;
		}
	}
}
