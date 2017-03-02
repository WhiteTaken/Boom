using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/UnityUI/RectChangedEvent")]
	[ExecuteInEditMode]
	[RequireComponent (typeof(RectTransform))]
	public class RectChangedEvent : MonoBehaviour {
		Rect lastRect = new Rect ();

		[SerializeField]
		protected RectTransform rect = null;

		public RectTransform Rect {
			get {
				return rect;
			}
			set {
				rect = value;
			}
		}

		[SerializeField]
		protected UnityEvent onRectChanged = new UnityEvent ();

		public UnityEvent OnRectChanged {
			get {
				return onRectChanged;
			}
			set {
				onRectChanged = value;
			}
		}


		void Update () {
			if (rect) {
				if (lastRect != rect.rect) {
					onRectChanged.Invoke ();
				}
				lastRect = rect.rect;
			} else {
				rect = GetComponent<RectTransform> ();
				if (!rect)
					enabled = false;
			}
		}
	}
}
