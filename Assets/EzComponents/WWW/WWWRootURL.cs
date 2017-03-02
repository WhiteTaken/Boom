using UnityEngine;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/WWW/WWWRootURL")]
	public sealed class WWWRootURL : MonoBehaviour {
		static private WWWRootURL instance = null;

		static public WWWRootURL Instance {
			get {
				if (instance == null) {
					GameObject go = new GameObject ("WWW RootURL");
					go.transform.localPosition = Vector3.zero;
					go.transform.localRotation = Quaternion.identity;
					go.transform.localScale = Vector3.one;
					go.AddComponent<WWWRootURL> ();
				}
				return instance;
			}
		}

		private WWWRootURL lastRootURL = null;

		[SerializeField]
		private RootURL userRootURL = RootURL.FILE;
		[SerializeField]
		private string url = "";

		public string UserRootURL {
			get {
				string temp = "";
				switch (userRootURL) {
				case RootURL.HTTP:
					temp = "http://" + url + "/";
					break;
				case RootURL.HTTPS:
					temp = "https://" + url + "/";
					break;
				case RootURL.FILE:
					temp = "file:///" + url + "/";
					break;
				case RootURL.FILE_DataPath:
					temp = "file:///" + Application.dataPath + "/" + url + "/";
					break;
				case RootURL.FILE_StreamingAssetsPath:
					temp = "file:///" + Application.streamingAssetsPath + "/" + url + "/";
					if (Application.platform == RuntimePlatform.Android) {
						temp = Application.streamingAssetsPath + "/" + url + "/";
					}
					break;
				case RootURL.FILE_PersistentDataPath:
					temp = "file:///" + Application.persistentDataPath + "/" + url + "/";
					break;
				case RootURL.FILE_TemporaryCachePath:
					temp = "file:///" + Application.temporaryCachePath + "/" + url + "/";
					break;
				case RootURL.Custom:
					temp = url + "/";
					break;
				}
				return temp;
			}
			set {
				userRootURL = RootURL.Custom;
				url = value;
			}
		}

		void Awake () {
			lastRootURL = instance;
			instance = this;
		}

		void OnDestroy () {
			instance = lastRootURL;
		}

		private enum RootURL {
			HTTP,
			HTTPS,
			FILE,
			FILE_DataPath,
			FILE_StreamingAssetsPath,
			FILE_PersistentDataPath,
			FILE_TemporaryCachePath,
			Custom
		}
	}
}