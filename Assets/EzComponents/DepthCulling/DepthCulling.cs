using UnityEngine;
using System.Collections;

namespace EzComponents {
	public class DepthCulling : MonoBehaviour {

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
		protected DepthOutput depthOutput = null;

		public DepthOutput DepthOutput {
			get {
				return depthOutput;
			}
			set {
				visible = false;
				onInvisible.Invoke ();
				depthOutput = value;
				targetCamera = depthOutput.GetComponent<Camera> ();
			}
		}

		[SerializeField]
		protected float showOffset = -0.05f;

		public float ShowOffset {
			get {
				return showOffset;
			}
			set {
				showOffset = value;
			}
		}

		[SerializeField]
		protected float hideOffset = -0.1f;

		public float HideOffset {
			get {
				return hideOffset;
			}
			set {
				hideOffset = value;
			}
		}

		[SerializeField]
		protected UnityEvent onVisible = new UnityEvent ();

		public UnityEvent OnVisible {
			get {
				return onVisible;
			}
		}

		[SerializeField]
		protected UnityEvent onInvisible = new UnityEvent ();

		public UnityEvent OnInvisible {
			get {
				return onInvisible;
			}
		}

		protected Camera targetCamera = null;
		protected Texture2D sampler = null;
		protected bool visible = false;

		void Start () {
			visible = false;
			sampler = new Texture2D (2, 2, TextureFormat.ARGB32, false);
			if (depthOutput != null) {
				targetCamera = depthOutput.GetComponent<Camera> ();
			}
		}

		void OnEnable () {
			
		}

		void OnDisable () {
			
		}

		void Update () {
			if (depthOutput == null) {
				return;
			}
			Transform temp = targetTransform ? targetTransform : gameObject.transform;
			var v = targetCamera.WorldToScreenPoint (temp.position);
			var d = Vector3.Distance (temp.position, targetCamera.transform.position);

			v.x /= Screen.width;
			v.y /= Screen.height;

			RenderTexture.active = depthOutput.Depth;

			float t = 1;
			if (v.x >= 0 && v.x < 1 && v.y >= 0 && v.y < 1) {
				Rect r = new Rect (v.x * depthOutput.Depth.width, (1 - v.y) * depthOutput.Depth.height, 2, 2);
				if (r.x >= depthOutput.Depth.width - 1)
					r.x -= 1;
				if (r.y >= depthOutput.Depth.height - 1)
					r.y -= 1;
				sampler.ReadPixels (r, 0, 0);
				RenderTexture.active = null;
				t = sampler.GetPixel (0, 0).r;
			}
			t = t * (targetCamera.farClipPlane - targetCamera.nearClipPlane) + targetCamera.nearClipPlane;
			if (d + showOffset < t) {
				if (!visible) {
					visible = true;
					onVisible.Invoke ();
				}
			} 
			if (d + hideOffset > t) {
				if (visible) {
					visible = false;
					onInvisible.Invoke ();
				}
			}
		}
	}
}