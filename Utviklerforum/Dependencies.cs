using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace Utviklerforum
{
	public class Dependencies
	{

		public void Mandatory_Dependencies()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<SomeDependency>());
			container.Register(Component.For<SomeClass>());

			var instance = container.Resolve<SomeClass>();

			Assert.That(instance.Dependency, Is.Not.Null);
		}


		public class SomeClass
		{
			public SomeDependency Dependency { get; }

			public SomeClass(SomeDependency dependency)
			{
				Dependency = dependency;
			}
		}

		public class SomeDependency
		{

		}
	}
}