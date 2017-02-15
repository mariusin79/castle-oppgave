using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace Utviklerforum
{
	public class Lifestyles
	{
		public void Default_Lifestyle()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<SomeClass>());

			var someClass = container.Resolve<SomeClass>();
			var someClass2 = container.Resolve<SomeClass>();

			Assert.That(someClass, Is.SameAs(someClass2));
		}

		public class SomeClass
		{
		}

	}

}