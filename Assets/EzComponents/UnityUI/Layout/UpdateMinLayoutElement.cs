using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/UnityUI/UpdateMinLayoutElement")]
	[ExecuteInEditMode]
	[RequireComponent (typeof(LayoutElement))]
	public class UpdateMinLayoutElement : MonoBehaviour {
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
		protected LayoutElement layout = null;

		public LayoutElement Layout {
			get {
				return layout;
			}
			set {
				layout = value;
			}
		}


		void Update () {
			if (!rect) {
				rect = GetComponent<RectTransform> ();
				if (!rect) {
					enabled = false;
					return;
				}
			} 
			
			if (!layout) {		
				layout = GetComponent<LayoutElement> ();
				if (!layout) {
					enabled = false;
					return;
				}
			}

			if (layout && rect) {
				if (lastRect != rect.rect) {
					layout.minWidth = rect.rect.width;
					layout.minHeight = rect.rect.height;
				}
				lastRect = rect.rect;
			} 
		}
	}
}