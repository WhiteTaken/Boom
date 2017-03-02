using UnityEngine;
using System.Collections;

namespace EzComponents.UnityUI {
	[AddComponentMenu ("EzComponents/UnityUI/TransformPointTrigger")]
	public class TransformPointTrigger : MonoBehaviour {
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

		[SerializeField]
		protected UnityEvent onEnter;

		public UnityEvent OnEnter {
			get {
				return onEnter;
			}
		}

		[SerializeField]
		protected UnityEvent onExit;

		public UnityEvent OnExit {
			get {
				return onExit;
			}
		}

		protected bool current = false;

		void Update () {
			if (!targetCamera)
				return;
			if (!targetTransform)
				return;
			if (!rect)
				return;
			if (RectTransformUtility.RectangleContainsScreenPoint (rect, targetCamera.WorldToScreenPoint (targetTransform.position))) {
				if (!current) {
					current = true;
					onEnter.Invoke ();
				}
			} else {
				if (current) {
					current = false;
					onExit.Invoke ();
				}
			}
		}
	}
}