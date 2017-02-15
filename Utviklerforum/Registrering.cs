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

		public void Explicit_Registration_Forwarded()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<ISomeClass, ISomeClass2>().ImplementedBy<SomeClass>());

			var someClass = container.Resolve<ISomeClass>();
			var someClass2 = container.Resolve<ISomeClass2>();

			Assert.That(someClass, Is.SameAs(someClass2));
		}

		public void Explicit_Registration_Default()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<ISomeClass>().ImplementedBy<SomeClass>());
			container.Register(Component.For<ISomeClass>().ImplementedBy<SomeClass2>().IsDefault());

			var someClass = container.Resolve<ISomeClass>();

			Assert.That(someClass, Is.InstanceOf<SomeClass2>());
		}

	}

	public interface ISomeClass
	{
	}

	public interface ISomeClass2
	{
	}

	public class SomeClass : ISomeClass, ISomeClass2
	{
	}

	public class SomeClass2 : ISomeClass { }

}