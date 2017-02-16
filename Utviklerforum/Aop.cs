using System.Collections.Generic;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace Utviklerforum
{
	public class Aop
	{

		public void Log_All_Method_Invocations()
		{
			var container = new WindsorContainer();

			container.Register(Component.For<Logger>());
			container.Register(Component.For<Log>());

			container.Register(Component.For<AClass>().Interceptors<Logger>());
			container.Register(Component.For<IBClass>().ImplementedBy<BClass>().Interceptors<Logger>());

			container.Resolve<AClass>().Hello();
			container.Resolve<IBClass>().World();

			Assert.That(container.Resolve<Log>().Messages, Is.EquivalentTo(new [] { "Hello", "World" }));
		}

		public class AClass
		{
			public virtual void Hello()
			{
				
			}
		}

		public class BClass : IBClass
		{
			public void World()
			{

			}
		}

		public class Logger : IInterceptor
		{
			private readonly Log _log;

			public Logger(Log log)
			{
				_log = log;
			}

			public void Intercept(IInvocation invocation)
			{
				_log.Add(invocation.Method.Name);
				invocation.Proceed();
			}
		}

		public class Log
		{
			private readonly List<string> _log = new List<string>();

			public void Add(string msg)
			{
				_log.Add(msg);
			}

			public IEnumerable<string> Messages => _log;
		}

	}

	public interface IBClass
	{
		void World();
	}
}