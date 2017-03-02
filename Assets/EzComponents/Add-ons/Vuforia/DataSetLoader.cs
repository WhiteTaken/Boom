//#define Vuforia
#if Vuforia
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Vuforia;

namespace EzComponents.Vuforia {
	public class DataSetLoader : MonoBehaviour {

		[SerializeField]
		protected RootPath rootPath = RootPath.Custom;
		[SerializeField]
		protected string path = ".xml";

		[SerializeField]
		protected UnityEvent onLoaded = new UnityEvent ();
		[SerializeField]
		protected UnityEvent onActive = new UnityEvent ();
		[SerializeField]
		protected UnityEvent onDeactivate = new UnityEvent ();
		[SerializeField]
		protected UnityEvent onUnloaded = new UnityEvent ();

		public string Path {
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
				case RootPath.Custom:
					temp = path;
					break;
				}
				return temp;
			}
			set {
				rootPath = RootPath.Custom;
				path = value;
			}
		}

		DataSet dataSet = null;

		[ContextMenu ("Load")]
		public void LoadDataSet () {
			if (dataSet != null)
				return;
			ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
			dataSet = objectTracker.CreateDataSet ();
			if (dataSet.Load (Path, VuforiaUnity.StorageType.STORAGE_ABSOLUTE)) {
				foreach (TrackableBehaviour tb in TrackerManager.Instance.GetStateManager ().GetTrackableBehaviours ()) {
					if (tb.name == "New Game Object") {
						tb.name = "DynamicTarget-" + tb.TrackableName;
					}
				}
				onLoaded.Invoke ();
			} else {
				dataSet = null;
			}
		}

		[ContextMenu ("Active")]
		public void ActiveDataSet () {
			if (dataSet == null)
				return;
			ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
			objectTracker.Stop ();
			if (!objectTracker.ActivateDataSet (dataSet)) {
				
			}
			if (!objectTracker.Start ()) {
				
			}
			onActive.Invoke ();
		}

		[ContextMenu ("Deactivate")]
		public void DeactivateDataSet () {
			if (dataSet == null)
				return;
			ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
			objectTracker.Stop ();
			if (!objectTracker.DeactivateDataSet (dataSet)) {

			}
			if (!objectTracker.Start ()) {
				
			}
			onDeactivate.Invoke ();
		}

		[ContextMenu ("Destroy")]
		public void DestroyDataSet () {
			if (dataSet != null)
				return;
			ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
			objectTracker.Stop ();
			if (!objectTracker.DeactivateDataSet (dataSet)) {

			}
			if (!objectTracker.DestroyDataSet (dataSet, false)) {

			}
			if (!objectTracker.Start ()) {
				
			}
			onUnloaded.Invoke ();
		}

		protected enum RootPath {
			DataPath,
			StreamingAssetsPath,
			PersistentDataPath,
			TemporaryCachePath,
			Custom
		}

	}
}
#endif