using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncHelper
{
	public readonly struct SignalSetter:IDisposable
	{
		readonly AutoResetEvent resetEvent;
		public bool Signal => resetEvent != default;
		internal SignalSetter(AutoResetEvent autoResetEvent) =>
			resetEvent = autoResetEvent;

		public void Dispose() => resetEvent?.Set();
	}
	public static class AutoResetEventExtensions
	{
		public static ValueTask<SignalSetter> WaitOneAsync(this AutoResetEvent autoResetEvent, int timeoutMilliseconds) =>
			autoResetEvent.WaitOneAsync(TimeSpan.FromMilliseconds(timeoutMilliseconds));

		public static async ValueTask<SignalSetter> WaitOneAsync(this AutoResetEvent autoResetEvent, TimeSpan timeout) =>
			autoResetEvent == null ? throw new ArgumentNullException(nameof(autoResetEvent)) :
			autoResetEvent.WaitOne(0) ? new SignalSetter(autoResetEvent) :
			await Task.Run(() => autoResetEvent.WaitOne(timeout)) ? new SignalSetter(autoResetEvent) :
			new SignalSetter(default);
	}
}
