using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading.ResetEventExtensions
{
	public static class AutoResetEventExtensions
	{
		public static ValueTask<WaitedEvent> WaitOneAsync(this AutoResetEvent autoResetEvent, int timeoutMilliseconds) =>
			autoResetEvent.WaitOneAsync(TimeSpan.FromMilliseconds(timeoutMilliseconds));

		public static async ValueTask<WaitedEvent> WaitOneAsync(this AutoResetEvent autoResetEvent, TimeSpan timeout) =>
			autoResetEvent == null ? throw new ArgumentNullException(nameof(autoResetEvent)) :
			autoResetEvent.WaitOne(0) ? new WaitedEvent(autoResetEvent) :
			await Task.Run(() => autoResetEvent.WaitOne(timeout)) ? new WaitedEvent(autoResetEvent) :
			WaitedEvent.Empty;
	}
}
