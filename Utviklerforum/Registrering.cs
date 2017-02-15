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

		public void Explicit_Registration_ByInterface()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<ISomeClass>().ImplementedBy<SomeClass>());

			var someClass = container.Resolve<ISomeClass>();

			Assert.That(someClass, Is.Not.Null);
		}

	}

	public interface ISomeClass
	{
	}

	public class SomeClass : ISomeClass
	{
	}

}