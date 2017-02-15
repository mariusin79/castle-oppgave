using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace Utviklerforum
{
	public class Registrering
	{
		public void Explicit_Registration()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<SomeClass>());

			var someClass = container.Resolve<SomeClass>();

			Assert.That(someClass, Is.Not.Null);
		}

	}

	public class SomeClass
	{
	}
}