using UnityEngine;
using System.Collections;

namespace EzComponents {
	[RequireComponent (typeof(Camera))]
	public class DepthOutput : MonoBehaviour {

		[SerializeField]
		protected RenderTexture depth = null;

		public RenderTexture Depth {
			get {
				return depth;
			}
		}

		[SerializeField]
		protected Shader shader = null;
		protected Material material = null;

		void Start () {
			GetComponent<Camera> ().depthTextureMode = DepthTextureMode.Depth;
			if (depth == null) {
				depth = new RenderTexture (512, 512, 0, RenderTextureFormat.ARGB32);
				depth.Create ();
			}
			if (shader == null) {
				material = new Material (Shader.Find ("Hidden/EzComponents/DepthOutput"));
			} else {
				material = new Material (shader);
			}

		}

		void OnRenderImage (RenderTexture src, RenderTexture dest) {
			Graphics.Blit (src, depth, material);
			Graphics.Blit (src, dest);
		}
	}
}