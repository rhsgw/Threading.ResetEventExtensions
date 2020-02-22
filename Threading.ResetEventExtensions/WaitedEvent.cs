using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncHelper
{
	public readonly struct WaitedEvent:IDisposable
	{
		public static WaitedEvent Empty { get; } = new WaitedEvent(default);
		readonly AutoResetEvent resetEvent;
		public bool Signal => resetEvent != default;
		internal WaitedEvent(AutoResetEvent autoResetEvent) =>
			resetEvent = autoResetEvent;

		public void Dispose() => resetEvent?.Set();
	}
}
