//#define EasyMovieTexture
#if EasyMovieTexture
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class EasyMovieTextureCtrl : MonoBehaviour {

	[SerializeField]
	protected MediaPlayerCtrl ctrl = null;

	[SerializeField]
	protected RootPath rootPath = RootPath.Custom;
	[SerializeField]
	protected string path = ".mp4";

	[SerializeField]
	protected bool autoLoad = true;
	[SerializeField]
	protected bool autoUnload = true;

	[SerializeField]
	protected float updateInterval = 0.2f;

	[SerializeField]
	protected UnityEvent onLoaded = new UnityEvent ();
	[SerializeField]
	protected UnityEvent onUnload = new UnityEvent ();
	[SerializeField]
	protected IntEvent onPositionChanged = new IntEvent ();
	[SerializeField]
	protected FloatEvent onNormalizedPositionChanged = new FloatEvent ();


	[SerializeField]
	protected UnityEvent onVideoReady = new UnityEvent ();
	[SerializeField]
	protected UnityEvent onVideoFirstFrameReady = new UnityEvent ();
	[SerializeField]
	protected UnityEvent onVideoEnd = new UnityEvent ();
	[SerializeField]
	protected StringEvent onVideoError = new StringEvent ();

	public string FullPath {
		get {
			string temp = "";
			switch (rootPath) {
			case RootPath.DataPath:
				temp = Application.dataPath + "/" + path;
				break;
			case RootPath.StreamingAssetsPath:
				temp = Application.streamingAssetsPath + "/" + path;
				break;
			case RootPath.PersistentDataPath:
				temp = Application.persistentDataPath + "/" + path;
				break;
			case RootPath.TemporaryCachePath:
				temp = Application.temporaryCachePath + "/" + path;
				break;
			case RootPath.HTTP:
				temp = "http://" + path;
				break;
			case RootPath.HTTPS:
				temp = "https://" + path;
				break;
			case RootPath.FILE:
				temp = "file:///" + path;
				break;
			case RootPath.FILE_DataPath:
				temp = "file:///" + Application.dataPath + "/" + path;
				break;
			case RootPath.FILE_StreamingAssetsPath:
				temp = "file:///" + Application.streamingAssetsPath + "/" + path;
				break;
			case RootPath.FILE_PersistentDataPath:
				temp = "file:///" + Application.persistentDataPath + "/" + path;
				break;
			case RootPath.FILE_TemporaryCachePath:
				temp = "file:///" + Application.temporaryCachePath + "/" + path;
				break;
			case RootPath.UserPath:
				temp = EasyMovieTextureRootPath.Instance.UserRootPath + "/" + path;
				break;
			case RootPath.Custom:
				temp = path + "/";
				break;
			}
			return temp;
		}
		set {
			rootPath = RootPath.Custom;
			path = value;
		}
	}

	public string Path {
		get {
			return path;
		}
		set {
			path = value;
		}
	}

	bool isStart = false;

	void Start () {
		if (autoLoad) {
			Load ();
		}
		isStart = true;
	}

	void OnEnable () {

		if (autoLoad && isStart) {
			Load ();
		}
	}

	void OnDisable () {
		if (autoUnload) {
			Unload ();
		}
	}

	float lastInterval = 0;

	void Update () {
		if (ctrl && ctrl.GetCurrentState () == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING) {
			lastInterval += Time.deltaTime;
			if (lastInterval >= updateInterval) {
				lastInterval = 0;
				int d = ctrl.GetDuration ();
				if (d == 0)
					return;
				int p = ctrl.GetSeekPosition ();
				onPositionChanged.Invoke (p);
				onNormalizedPositionChanged.Invoke ((float)p / (float)d);
			}
		}
	}

	[ContextMenu ("Load")]
	public void Load () {
		if (ctrl) {
			ctrl.OnReady += OnVideoReady;
			ctrl.OnVideoFirstFrameReady += OnVideoFirstFrameReady;
			ctrl.OnEnd += OnVideoEnd;
			ctrl.OnVideoError += OnVideoError;
			ctrl.Load (FullPath);
			onLoaded.Invoke ();
		}
	}

	public void Load (string fullPath) {
		FullPath = fullPath;
		if (ctrl)
			Load ();
	}

	[ContextMenu ("Unload")]
	public void Unload () {
		onUnload.Invoke ();
		if (ctrl) {
			ctrl.UnLoad ();
			ctrl.OnReady -= OnVideoReady;
			ctrl.OnVideoFirstFrameReady -= OnVideoFirstFrameReady;
			ctrl.OnEnd -= OnVideoEnd;
			ctrl.OnVideoError -= OnVideoError;
		}
	}

	[ContextMenu ("Play")]
	public void Play () {
		lastInterval = updateInterval;
		if (ctrl) {
			ctrl.Play ();
		}
	}

	[ContextMenu ("Pause")]
	public void Pause () {
		lastInterval = updateInterval;
		if (ctrl) {
			ctrl.Pause ();
		}
	}

	[ContextMenu ("Stop")]
	public void Stop () {
		lastInterval = updateInterval;
		if (ctrl) {
			ctrl.Stop ();
		}
	}

	float currentVolume = 1f;

	public float GetVolume () {
		return currentVolume;
	}

	public void SetVolume (float volume) {
		if (ctrl) {
			currentVolume = volume;
			ctrl.SetVolume (volume);
		}
	}

	public int GetSeekPosition () {
		if (ctrl) {
			return ctrl.GetSeekPosition ();
		}
		return 0;
	}

	public void SeekTo (int seek) {
		if (ctrl) {
			if (seek == GetSeekPosition ())
				return;
			ctrl.SeekTo (seek);
		}
	}

	public float GetSeekPositionNormalize () {
		if (ctrl) {
			int d = ctrl.GetDuration ();
			if (d == 0)
				return 0;
			int p = ctrl.GetSeekPosition ();
			return (float)p / (float)d;
		}
		return 0;
	}

	public void SeekToNormalize (float seek) {
		if (ctrl) {
			if (seek == GetSeekPositionNormalize ())
				return;
			int d = ctrl.GetDuration ();
			if (d == 0)
				return;
			ctrl.SeekTo ((int)(seek * (float)d));
		}
	}

	public int GetDuration () {
		if (ctrl)
			return ctrl.GetDuration ();
		return 0;
	}

	void OnVideoReady () {
		onVideoReady.Invoke ();
	}

	void OnVideoFirstFrameReady () {
		onVideoFirstFrameReady.Invoke ();
	}

	void OnVideoEnd () {
		onVideoEnd.Invoke ();
	}

	void OnVideoError (MediaPlayerCtrl.MEDIAPLAYER_ERROR errorCode, MediaPlayerCtrl.MEDIAPLAYER_ERROR errorCodeExtra) {
		onVideoError.Invoke ("MediaPlayerCtrl Error: [" + errorCode.ToString () + "] And [" + errorCode.ToString () + "]");
	}

	protected enum RootPath {
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
		UserPath,
		Custom
	}

	[Serializable]
	protected class IntEvent : UnityEvent<int> {
	}

	[Serializable]
	protected class FloatEvent : UnityEvent<float> {
	}

	[Serializable]
	protected class StringEvent : UnityEvent<string> {
	}
}

#endif