using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using NUnit.Framework;

namespace Utviklerforum
{
	public class Installers
	{

		public void Installer_Installs_Components()
		{
			var container = new WindsorContainer();

			container.Install(FromAssembly.This());

			var someInterceptor = container.Resolve<SomeInterceptor>();

			Assert.That(someInterceptor, Is.Not.Null);
		}


		public class MyInstaller: IWindsorInstaller
		{
			public void Install(IWindsorContainer container, IConfigurationStore store)
			{
				container.Register(Classes.FromThisAssembly().BasedOn<IInterceptor>().WithServiceSelf());
			}
		}

		public class SomeInterceptor: IInterceptor
		{
			public void Intercept(IInvocation invocation)
			{
				throw new System.NotImplementedException();
			}
		}

		public class AnotherInterceptor : IInterceptor
		{
			public void Intercept(IInvocation invocation)
			{
				throw new System.NotImplementedException();
			}
		}
	}
}