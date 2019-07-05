namespace NendUnityPlugin.AD.Native
{
	using System;
	using System.Linq;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.EventSystems;

	using NendUnityPlugin.AD.Native.Handlers;
	using NendUnityPlugin.AD.Native.Utils;

	using Log = NendUnityPlugin.Common.NendAdLogger;

	internal abstract class NativeAd : INativeAd, IDisposable
	{
		public event EventHandler AdClicked;

		private bool m_IsImpression = false;
		private HashSet<MonoBehaviour> m_Handlers = new HashSet<MonoBehaviour> ();

		~NativeAd ()
		{
			Dispose ();
		}

		public virtual void Dispose ()
		{
			Log.D ("Dispose NativeAd.");
			DestroyAllHandlers ();
		}

		public virtual string ShortText {
			get {
				throw new NotImplementedException ();
			}
		}

		public virtual string LongText {
			get {
				throw new NotImplementedException ();
			}
		}

		public virtual string PromotionUrl {
			get {
				throw new NotImplementedException ();
			}
		}

		public virtual string PromotionName {
			get {
				throw new NotImplementedException ();
			}
		}

		public virtual string ActionButtonText {
			get {
				throw new NotImplementedException ();
			}
		}

		public virtual string AdImageUrl {
			get {
				throw new NotImplementedException ();
			}
		}

		public virtual string LogoImageUrl {
			get {
				throw new NotImplementedException ();
			}
		}

		public virtual string GetAdvertisingExplicitlyText (AdvertisingExplicitly advertisingExplicitly)
		{
			throw new NotImplementedException ();
		}

		public IEnumerator LoadAdImage (Action<Texture2D> callback)
		{
			if (!string.IsNullOrEmpty (AdImageUrl)) {
				return TextureLoader.LoadTexture (AdImageUrl, callback);
			} else {
				callback (null);
				return null;
			}
		}

		public IEnumerator LoadLogoImage (Action<Texture2D> callback)
		{
			if (!string.IsNullOrEmpty (LogoImageUrl)) {
				return TextureLoader.LoadTexture (LogoImageUrl, callback);
			} else {
				callback (null);
				return null;
			}
		}

		public void Activate (GameObject adGameObject, GameObject prGameObject)
		{
			if (null == adGameObject || null == prGameObject) {
				throw new ArgumentNullException ("`GameObject` must not be null.");
			}
				
			DestroyHandler (adGameObject.GetComponent <ClickHandler> ());
			DestroyHandler (prGameObject.GetComponent <ClickHandler> ());
			DestroyHandler (adGameObject.GetComponent <ImpressionHandler> ());

			AddClickHandler (adGameObject, () => {
				PerformAdClick ();
				var callback = AdClicked;
				if (null != callback) {
					callback (this, EventArgs.Empty);
				}
			});

			AddClickHandler (prGameObject, () => {
				PerformInformationClick ();
			});
				
			if (!m_IsImpression) {
				AddImpressionHandler (adGameObject);
			}
		}

		public void Into (NendAdNativeView adView)
		{
			if (null == adView) {
				throw new ArgumentNullException ("`NendAdNativeView` must not be null.");
			}
			adView.RenderAd (this);
		}

		internal abstract void SendImpression ();

		internal abstract void PerformAdClick ();

		internal abstract void PerformInformationClick ();

		private void AddClickHandler (GameObject target, Action callback)
		{
			ClickHandler handler = null;
			var canvas = target.GetComponentInParent <Canvas> ();
			if (null != canvas) {
				handler = target.AddComponent <ClickHandlerUI> ();
			} else {
				var collider = target.GetComponent <Collider> ();
				if (null != collider) {
					handler = target.AddComponent <ClickHandler3D> ();
				} else {
					var collider2D = target.GetComponent <Collider2D> ();
					if (null != collider2D) {
						handler = target.AddComponent <ClickHandler2D> ();
					}
				}
			}
			if (null != handler) {
				handler.OnClick = callback;
				m_Handlers.Add (handler);
			} else {
				Log.W ("Couldn't be to clickable. {0}", target.name);
			}
		}

		private void AddImpressionHandler (GameObject target)
		{
			ImpressionHandler handler = null;
			var transform = target.GetComponent <RectTransform> ();
			if (null != transform) {
				handler = target.AddComponent <ImpressionHandlerUI> ();
			} else {
				handler = target.AddComponent <ImpressionHandlerGO> ();
			}
			if (null != handler) {
				handler.OnImpression = () => {
					m_IsImpression = true;
					SendImpression ();
					DestroyImpressionHandlers ();
				};
				m_Handlers.Add (handler);
			} else {
				Log.W ("Couldn't be possible to detect an impression. {0}", target.name);
			}
		}

		private void DestroyHandler (MonoBehaviour handler)
		{
			if (null != handler) {
				GameObject.Destroy (handler);
				m_Handlers.Remove (handler);
			}
		}

		private void DestroyHandlers (MonoBehaviour[] handlers)
		{
			if (null != handlers && 0 < handlers.Length) {
				foreach (var handler in handlers) {
					DestroyHandler (handler);
					m_Handlers.Remove (handler);
				}
			}
		}
			
		private void DestroyImpressionHandlers ()
		{
			DestroyHandlers (m_Handlers.Where (h => h is ImpressionHandler).ToArray ());
		}

		private void DestroyAllHandlers ()
		{
			DestroyHandlers (m_Handlers.ToArray ());
		}
	}
}