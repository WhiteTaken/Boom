using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/EzSceneLoader/EzSceneLoader")]
	public class EzSceneLoader : MonoBehaviour {
		public string loaderScene;
		public string targetScene;

		public AsyncOperation loader = null;

		State state = State.Ready;

		enum State {
			Ready,
			Load,
			Loading
		}

		[ContextMenu ("Load")]
		public void Load () {
			transform.parent = null;
			if (loaderScene.Equals ("")) {
				SceneManager.LoadScene (targetScene);
				state = State.Loading;
				return;
			}
			SceneManager.LoadScene (loaderScene);
			state = State.Load;
		}

		void Awake () {
			GameObject.DontDestroyOnLoad (gameObject);
		}

		void Update () {
			if (state == State.Load) {
				if (SceneManager.GetActiveScene ().name.Equals (loaderScene)) {
					loader = SceneManager.LoadSceneAsync (targetScene);
					state = State.Loading;
				}
			}
			if (state == State.Loading) {
				if (loader.isDone) {
					GameObject.Destroy (gameObject);
				}
			}
		}
	}
}