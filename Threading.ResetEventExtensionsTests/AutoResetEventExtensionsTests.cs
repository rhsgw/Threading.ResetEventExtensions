using Microsoft.VisualStudio.TestTools.UnitTesting;
using Threading.ResetEventExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.ResetEventExtensions.Tests
{
	[TestClass()]
	public class AutoResetEventExtensionsTests
	{
		[TestMethod()]
		public async Task WaitOneAsyncTest()
		{
			using(var are = new AutoResetEvent(false))
			using(new Timer(a => ((AutoResetEvent)a).Set(), are, 50, Timeout.Infinite))
			{
				var begin = DateTimeOffset.Now;
				using(var setter1 = await are.WaitOneAsync(100))
				{
					var elapsed = DateTimeOffset.Now - begin;
					Assert.IsTrue(elapsed.TotalMilliseconds < 99);
					Assert.IsTrue(setter1.Signal);
					Assert.IsFalse(are.WaitOne(0));

					using(var setter2 = await are.WaitOneAsync(10))
						Assert.IsFalse(setter2.Signal);
					Assert.IsFalse(are.WaitOne(0));
				}

				Assert.IsTrue(are.WaitOne(0));
			}
		}
		[TestMethod]
		public async Task DisposedResetEventTest()
		{
			var are = new AutoResetEvent(false);
			are.Dispose();
			await are.WaitOneAsync(150);

			are = new AutoResetEvent(true);
			using(var waited = await are.WaitOneAsync(0))
				are.Dispose();
		}
	}
}