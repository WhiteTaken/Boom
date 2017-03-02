//#define Vuforia
#if Vuforia
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

namespace EzComponents.Vuforia {
	public class UnityTrackableEventHandler : MonoBehaviour, ITrackableEventHandler {

		private TrackableBehaviour mTrackableBehaviour;
		public UnityEvent onFound = new UnityEvent ();
		public UnityEvent onLost = new UnityEvent ();

		void Start () {
			mTrackableBehaviour = GetComponent<TrackableBehaviour> ();
			if (mTrackableBehaviour) {
				mTrackableBehaviour.RegisterTrackableEventHandler (this);
			}
		}

		public void OnTrackableStateChanged (TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus) {
			if (newStatus == TrackableBehaviour.Status.DETECTED ||
			    newStatus == TrackableBehaviour.Status.TRACKED ||
			    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
				OnTrackingFound ();
			} else {
				OnTrackingLost ();
			}
		}

		private void OnTrackingFound () {
			onFound.Invoke ();
		}

		private void OnTrackingLost () {
			onLost.Invoke ();
		}

	}
}
#endif