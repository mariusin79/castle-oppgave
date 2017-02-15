using System;
using Castle.Facilities.TypedFactory;
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

		public void Optional_Dependencies()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<SomeDependency>());
			container.Register(Component.For<AnotherDependency>());
			container.Register(Component.For<SomeClass>());

			var instance = container.Resolve<SomeClass>();

			Assert.That(instance.OptionalDependency, Is.Not.Null);
		}

		public void Delegate_Based_Factory()
		{
			var container = new WindsorContainer();

			container.AddFacility<TypedFactoryFacility>();

			container.Register(Component.For<SomeDependency>());

			var factory =  container.Resolve<Func<SomeDependency>>();

			Assert.That(factory, Is.Not.Null);
		}

		public class SomeClass
		{
			public SomeDependency Dependency { get; }

			public AnotherDependency OptionalDependency { get; set; }

			public SomeClass(SomeDependency dependency)
			{
				Dependency = dependency;
			}
		}

		public class SomeDependency
		{

		}

		public class AnotherDependency
		{
			
		}
	}
}