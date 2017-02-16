using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace Utviklerforum
{
	public class KonvensjonsbasertRegistrering
	{
		public void Register_By_Naming_Convention()
		{
			var container = new WindsorContainer();

			container.Register(Classes.FromThisAssembly().Where(type => type.Name.EndsWith("Repo")));

			var someRepo = container.Resolve<SomeRepo>();

			Assert.That(someRepo, Is.Not.Null);
		}




		public class SomeRepo
		{
			
		}

		public class AnotherRepo
		{
		
		}

	}
}