//#define LeanTouch
#if LeanTouch
using UnityEngine;

public class ProjectionRotatieScale : MonoBehaviour {
	
	public new Camera camera = null;

	protected virtual void OnEnable () {
		Lean.LeanTouch.OnTwistDegrees += OnTwistDegrees;
	}

	protected virtual void OnDisable () {
		Lean.LeanTouch.OnTwistDegrees -= OnTwistDegrees;
	}

	public void OnTwistDegrees (float degrees) {
		bool over = false;
		foreach (var f in Lean.LeanTouch.Fingers) {
			if (f.IsOverGui) {
				over = true;
				break;
			}
		}
		if (over)
			return;
		Lean.LeanTouch.RotateObject (transform, Lean.LeanTouch.TwistDegrees, camera);
		Lean.LeanTouch.ScaleObject (transform, Lean.LeanTouch.PinchScale);
	}

}
#endif