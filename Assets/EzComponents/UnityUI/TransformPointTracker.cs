using UnityEngine;
using System.Collections;

namespace EzComponents.UnityUI {
	[AddComponentMenu ("EzComponents/UnityUI/TransformPointTracker")]
	public class TransformPointTracker : MonoBehaviour {
		[SerializeField]
		protected Camera targetCamera = null;

		public Camera TargetCamera {
			get {
				return targetCamera;
			}
			set {
				targetCamera = value;
			}
		}

		[SerializeField]
		protected Transform targetTransform = null;

		public Transform TargetTransform {
			get {
				return targetTransform;
			}
			set {
				targetTransform = value;
			}
		}


		[SerializeField]
		protected RectTransform rect;

		public RectTransform Rect {
			get {
				return rect;
			}
			set {
				rect = value;
			}
		}

		protected Canvas canvas;

		void Start () {
			Canvas[] cs = rect.GetComponentsInParent<Canvas> ();
			Canvas topmost = cs [cs.Length - 1];
			canvas = topmost;
		}

		void Update () {
			if (!targetCamera)
				return;
			if (!targetTransform)
				return;
			if (!canvas)
				return;
			if (!rect)
				return;
			if (canvas.renderMode == RenderMode.ScreenSpaceOverlay) {
				Vector2 temp;
				RectTransformUtility.ScreenPointToLocalPointInRectangle (rect.parent as RectTransform, targetCamera.WorldToScreenPoint (targetTransform.position), null, out temp);
				rect.localPosition = temp;
			}
			if (canvas.renderMode == RenderMode.ScreenSpaceCamera) {
				Vector2 temp;
				RectTransformUtility.ScreenPointToLocalPointInRectangle (rect.parent as RectTransform, targetCamera.WorldToScreenPoint (targetTransform.position), targetCamera, out temp);
				rect.localPosition = temp;
			}

		}
	}
}