using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncHelper
{
	public readonly struct SignalSetter:IDisposable
	{
		public static SignalSetter Empty { get; } = new SignalSetter(default);
		readonly AutoResetEvent resetEvent;
		public bool Signal => resetEvent != default;
		internal SignalSetter(AutoResetEvent autoResetEvent) =>
			resetEvent = autoResetEvent;

		public void Dispose() => resetEvent?.Set();
	}
}
