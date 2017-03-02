//#define EasyMovieTexture
#if EasyMovieTexture
using UnityEngine;
using System;
using System.Collections.Generic;

public sealed class EasyMovieTextureRootPath : MonoBehaviour {
	static private EasyMovieTextureRootPath instance = null;

	static public EasyMovieTextureRootPath Instance {
		get {
			if (instance == null) {
				GameObject go = new GameObject ("EasyMovieTexture RootPath");
				go.transform.localPosition = Vector3.zero;
				go.transform.localRotation = Quaternion.identity;
				go.transform.localScale = Vector3.one;
				go.AddComponent<EasyMovieTextureRootPath> ();
			}
			return instance;
		}
	}

	private EasyMovieTextureRootPath lastRootPath = null;

	[SerializeField]
	private RootPath userRootPath = RootPath.FILE;
	[SerializeField]
	private string path = "";

	public string UserRootPath {
		get {
			string temp = "";
			switch (userRootPath) {
			case RootPath.DataPath:
				temp = Application.dataPath + "/" + path + "/";
				break;
			case RootPath.StreamingAssetsPath:
				temp = Application.streamingAssetsPath + "/" + path + "/";
				break;
			case RootPath.PersistentDataPath:
				temp = Application.persistentDataPath + "/" + path + "/";
				break;
			case RootPath.TemporaryCachePath:
				temp = Application.temporaryCachePath + "/" + path + "/";
				break;
			case RootPath.HTTP:
				temp = "http://" + path + "/";
				break;
			case RootPath.HTTPS:
				temp = "https://" + path + "/";
				break;
			case RootPath.FILE:
				temp = "file:///" + path + "/";
				break;
			case RootPath.FILE_DataPath:
				temp = "file:///" + Application.dataPath + "/" + path + "/";
				break;
			case RootPath.FILE_StreamingAssetsPath:
				temp = "file:///" + Application.streamingAssetsPath + "/" + path + "/";
				if (Application.platform == RuntimePlatform.Android) {
					temp = Application.streamingAssetsPath + "/" + path + "/";
				}
				break;
			case RootPath.FILE_PersistentDataPath:
				temp = "file:///" + Application.persistentDataPath + "/" + path + "/";
				break;
			case RootPath.FILE_TemporaryCachePath:
				temp = "file:///" + Application.temporaryCachePath + "/" + path + "/";
				break;
			case RootPath.Custom:
				temp = path + "/";
				break;
			}
			return temp;
		}
		set {
			userRootPath = RootPath.Custom;
			path = value;
		}
	}

	void Awake () {
		lastRootPath = instance;
		instance = this;
	}

	void OnDestroy () {
		instance = lastRootPath;
	}

	private enum RootPath {
		DataPath,
		StreamingAssetsPath,
		PersistentDataPath,
		TemporaryCachePath,
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
#endif
