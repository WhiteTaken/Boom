//#define LeanTouch
#if LeanTouch
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TapEvents : MonoBehaviour {
	
	public UnityEvent onTap = new UnityEvent ();

	protected virtual void OnEnable () {
		Lean.LeanTouch.OnFingerTap += OnFingerTap;
	}

	protected virtual void OnDisable () {
		Lean.LeanTouch.OnFingerTap -= OnFingerTap;
	}

	public void OnFingerTap (Lean.LeanFinger finger) {
		if (finger.IsOverGui == false) {
			onTap.Invoke ();
		}
	}

	public class TapEvent : UnityEvent<Vector3> {
		
	}

}
#endif