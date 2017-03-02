using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/WWW/WWWTextureLoader")]
	public class WWWTextureLoader : MonoBehaviour {
		[SerializeField]
		protected RootURL rootURL = RootURL.FILE_StreamingAssetsPath;
		[SerializeField]
		protected string url = ".jpg";

		[SerializeField]
		protected bool autoLoad = true;

		public bool AutoLoad {
			get {
				return autoLoad;
			}
			set {
				autoLoad = value;
			}
		}

		[SerializeField]
		protected bool autoUnload = true;

		public bool AutoUnload {
			get {
				return autoUnload;
			}
			set {
				autoUnload = value;
			}
		}

		[SerializeField]
		protected TextureEvent onLoaded = new TextureEvent ();

		public TextureEvent OnLoaded {
			get {
				return onLoaded;
			}
		}

		[SerializeField]
		protected UnityEvent onUnload = new UnityEvent ();

		public UnityEvent OnUnload {
			get {
				return onUnload;
			}
		}

		protected Texture2D texture;

		public Texture2D Texture {
			get {
				return texture;
			}
		}


		bool isStarted = false;

		public string URL {
			get {
				string temp = "";
				switch (rootURL) {
				case RootURL.HTTP:
					temp = "http://" + url;
					break;
				case RootURL.HTTPS:
					temp = "https://" + url;
					break;
				case RootURL.FILE:
					temp = "file://" + url;
					break;
				case RootURL.FILE_DataPath:
					temp = "file://" + Application.dataPath + "/" + url;
					break;
				case RootURL.FILE_StreamingAssetsPath:
					temp = "file://" + Application.streamingAssetsPath + "/" + url;
					if (Application.platform == RuntimePlatform.Android) {
						temp = Application.streamingAssetsPath + "/" + url;
					}
					break;
				case RootURL.FILE_PersistentDataPath:
					temp = "file://" + Application.persistentDataPath + "/" + url;
					break;
				case RootURL.FILE_TemporaryCachePath:
					temp = "file://" + Application.temporaryCachePath + "/" + url;
					break;
				case RootURL.UserURL:
					temp = WWWRootURL.Instance.UserRootURL + "/" + url;
					break;
				case RootURL.Custom:
					temp = url;
					break;
				}
				return temp;
			}
			set {
				rootURL = RootURL.Custom;
				url = value;
			}
		}


		void Start () {
			if (autoLoad) {
				Load ();
			}
			isStarted = true;
		}

		void OnEnable () {
			if (autoLoad && isStarted && texture == null) {
				Load ();
			}
		}

		void OnDisable () {
			if (autoUnload) {
				Unload ();
			}
		}

		[ContextMenu ("Load")]
		public void Load () {
			StopAllCoroutines ();
			StartCoroutine (Do ());
		}

		public void Load (string url) {
			URL = url;
			Load ();
		}

		[ContextMenu ("Unload")]
		public void Unload () {
			StopAllCoroutines ();
			if (texture != null) {
				onUnload.Invoke ();
				Destroy (texture);
				Resources.UnloadUnusedAssets ();
			}
		}

		IEnumerator Do () {
			WWW www = new WWW (URL);
			yield return www;
			texture = www.texture;
			if (texture) {
				onLoaded.Invoke (texture);
			}
		}

		protected enum RootURL {
			HTTP,
			HTTPS,
			FILE,
			FILE_DataPath,
			FILE_StreamingAssetsPath,
			FILE_PersistentDataPath,
			FILE_TemporaryCachePath,
			UserURL,
			Custom
		}
	}
}
