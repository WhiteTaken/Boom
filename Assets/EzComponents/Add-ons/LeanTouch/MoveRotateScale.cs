//#define LeanTouch
#if LeanTouch
using UnityEngine;

public class MoveRotateScale : MonoBehaviour {
	
	public new Camera cameraX = null;
    public new Camera cameraY = null;
    public float minScale = 0.5f;
    public float maxScale = 1.5f;

    public float speed = 0.1f;

	protected virtual void OnEnable () {
		Lean.LeanTouch.OnTwistDegrees += OnTwistDegrees;
        Lean.LeanTouch.OnFingerDrag += OnFingerTap;
        Lean.LeanTouch.OnMultiDrag += OnMultiDrag;

	}

	protected virtual void OnDisable () {
		Lean.LeanTouch.OnTwistDegrees -= OnTwistDegrees;
        Lean.LeanTouch.OnFingerDrag -= OnFingerTap;
        Lean.LeanTouch.OnMultiDrag -= OnMultiDrag;

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
		//Lean.LeanTouch.RotateObject (transform, Lean.LeanTouch.TwistDegrees, camera);
		Lean.LeanTouch.ScaleObject (transform, Lean.LeanTouch.PinchScale);
        if (this.transform.localScale.x <= minScale)
        {
            this.transform.localScale = new Vector3(minScale, minScale, minScale);
        }
        if (this.transform.localScale.x >= maxScale)
        {
            this.transform.localScale = new Vector3(maxScale, maxScale, maxScale);
        }
	}
    public void OnFingerTap(Lean.LeanFinger finger)
    {
        bool over = false;
        foreach (var f in Lean.LeanTouch.Fingers)
        {
            if (f.IsOverGui)
            {
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
        Lean.LeanTouch.RotateObject(transform, Lean.LeanTouch.DragDelta.x*speed, cameraX);
        Lean.LeanTouch.RotateObject(transform, Lean.LeanTouch.DragDelta.y*speed, cameraY);
        
    }


    public void OnMultiDrag(Vector2 drag)
    {
        bool over = false;
        foreach (var f in Lean.LeanTouch.Fingers)
        {
            if (f.IsOverGui)
            {
                over = true;
                break;
            }
        }
        if (over)
            return;
        Lean.LeanTouch.MoveObject(transform, Lean.LeanTouch.DragDelta);
    }
}
#endif