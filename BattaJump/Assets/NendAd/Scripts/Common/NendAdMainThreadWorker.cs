namespace NendUnityPlugin.Common
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;

	using Log = NendUnityPlugin.Common.NendAdLogger;

	internal class NendAdMainThreadWorker : MonoBehaviour
	{
		private readonly static Queue<Action> s_ActionQueue = new Queue<Action> ();
		private static NendAdMainThreadWorker s_Instance = null;

		internal static NendAdMainThreadWorker Instance {
			get {
				return s_Instance;
			}
		}

		void Awake ()
		{
			if (null == s_Instance) {
				s_Instance = this;
				DontDestroyOnLoad (this.gameObject);
			} else {
				Destroy (this.gameObject);
			}
		}

		// Use this for initialization
		void Start ()
		{
	
		}
		
		// Update is called once per frame
		void Update ()
		{
			lock (s_ActionQueue) {
				while (0 < s_ActionQueue.Count) {
					s_ActionQueue.Dequeue ().Invoke ();
				}
			}
		}

		void OnDestroy ()
		{
			s_Instance = null;
		}

		internal static void Prepare ()
		{
			if (null == s_Instance) {
				var go = new GameObject ("NendAdMainThreadWorker");
				go.AddComponent <NendAdMainThreadWorker> ();
			} else {
				Log.D ("NendAdMainThreadWorker is already prepared.");
			}
		}

		internal void Post (Action action)
		{
			lock (s_ActionQueue) {
				s_ActionQueue.Enqueue (action);
			}
		}
	}
}
