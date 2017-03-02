//#define LeanTouch
#if LeanTouch
using UnityEngine;

public class DragRotate : MonoBehaviour {
	
	public Transform x;
	public Transform y;

	public Camera cx;
	public Camera cy;
    public float speed = 0.1f;

	protected virtual void OnEnable () {
		Lean.LeanTouch.OnFingerDrag += OnFingerTap;
	}

	protected virtual void OnDisable () {
		Lean.LeanTouch.OnFingerDrag -= OnFingerTap;
	}

	public void OnFingerTap (Lean.LeanFinger finger) {
		bool over = false;
		foreach (var f in Lean.LeanTouch.Fingers) {
			if (f.IsOverGui) {
				over = true;
				break;
			}
		}
		if (over)
			return;
        if (Lean.LeanTouch.Fingers.Count > 1)
        {
            return;
        }
        Lean.LeanTouch.RotateObject(x, Lean.LeanTouch.DragDelta.x * speed, cx);
        Lean.LeanTouch.RotateObject(y, Lean.LeanTouch.DragDelta.y * speed, cy);
	}

}
#endif