using System;
using System.Threading;
using System.Threading.Tasks;
using Castle.MicroKernel.Lifestyle;
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

		public void Transient_Lifestyle()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<SomeClass>().LifeStyle.Transient);

			var someClass = container.Resolve<SomeClass>();
			var someClass2 = container.Resolve<SomeClass>();

			Assert.That(someClass, Is.Not.SameAs(someClass2));
		}

		public void Transient_Lifestyle_DoesNotTrackInstance()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<SomeClass>().LifeStyle.Transient);

			var someClass = container.Resolve<SomeClass>();

			Assert.That(!container.Kernel.ReleasePolicy.HasTrack(someClass));
		}

		public void Transient_Lifestyle_TracksIDisposable()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<SomeDisposableClass>().LifeStyle.Transient);

			var someClass = container.Resolve<SomeDisposableClass>();

			Assert.That(container.Kernel.ReleasePolicy.HasTrack(someClass));
		}

		public void Transient_Lifestyle_ReleasesTrackOfIDisposable()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<SomeDisposableClass>().LifeStyle.Transient);

			var someClass = container.Resolve<SomeDisposableClass>();
			container.Release(someClass);

			Assert.That(!container.Kernel.ReleasePolicy.HasTrack(someClass));
		}

		public void Transient_Lifestyle_DoesNotTrackInstancePopQuiz()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<SomeDisposableClass>().LifeStyle.Transient);
			container.Register(Component.For<AClassWithDiposableDependency>().LifeStyle.Transient);

			var someClass = container.Resolve<AClassWithDiposableDependency>();

			Assert.That(!container.Kernel.ReleasePolicy.HasTrack(someClass));
		}

		public void Scoped_Lifestyle()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<SomeClass>().LifeStyle.Scoped());

			using (container.BeginScope())
			{
				var someClass = container.Resolve<SomeClass>();
				Assert.That(someClass, Is.Not.Null);
			}
		}

		public async Task Scoped_Lifestyle_SupportsAsyncAwait()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<SomeClass>().LifeStyle.Scoped());

			using (container.BeginScope())
			{
				var someClass = await SomeMethodInTheCallStackAsync(container);
				Assert.That(someClass, Is.Not.Null);
			}
		}

		private static async Task<SomeClass> SomeMethodInTheCallStackAsync(IWindsorContainer container)
		{
			Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
			await Task.Delay(500).ConfigureAwait(false);
			Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
			return container.Resolve<SomeClass>();
		}

		public class SomeClass
		{
		}

		public class AClassWithDiposableDependency
		{
			private SomeDisposableClass _dependency;

			public AClassWithDiposableDependency(SomeDisposableClass dependency)
			{
				_dependency = dependency;
			}
		}

		public class SomeDisposableClass : IDisposable
		{
			public void Dispose()
			{
			}
		}

	}

}