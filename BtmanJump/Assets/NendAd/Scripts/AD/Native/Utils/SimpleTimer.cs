namespace NendUnityPlugin.AD.Native.Utils
{
	using System;
	using System.Timers;

	using Worker = NendUnityPlugin.Common.NendAdMainThreadWorker;
	using Log = NendUnityPlugin.Common.NendAdLogger;

	internal class SimpleTimer : IDisposable
	{
		internal Action OnFireEvent;

		private Timer m_Timer;

		internal SimpleTimer ()
		{
			Worker.Prepare ();
		}

		~SimpleTimer ()
		{
			Log.D ("~SimpleTimer()");
			Dispose ();
		}

		public void Dispose ()
		{
			Stop ();
		}

		internal void Start (double interval)
		{
			Stop ();
			m_Timer = new Timer (interval);
			m_Timer.Elapsed += OnTimedEvent;
			m_Timer.Start ();
			Log.I ("Start the timer with {0} milliseconds.", interval);
		}

		internal void Stop ()
		{
			if (null != m_Timer) {
				Log.I ("Stop the timer.");
				m_Timer.Elapsed -= OnTimedEvent;
				m_Timer.Stop ();
				m_Timer.Close ();
				m_Timer = null;
			}
		}

		private void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			Worker.Instance.Post (OnFireEvent);
		}
	}
}

