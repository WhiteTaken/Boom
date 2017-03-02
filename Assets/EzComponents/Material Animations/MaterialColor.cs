using UnityEngine;
using System.Collections;

namespace EzComponents {
	public class MaterialColor : MonoBehaviour {
		
		public Material material;
		public string colorName = "_Color";
		public Color fromColor = Color.white;
		public Color toColor = Color.white;
		public float speed = 1;

		enum State {
			Forward,
			Backward,
			Finished
		}

		State state = State.Finished;
		Color nowColor = Color.white;
		float now = 0f;

		void Awake () {
			nowColor = fromColor;
		}

		[ContextMenu ("SetFrom")]
		public void SetFrom () {
			state = State.Finished;
			now = 0f;
			nowColor = Color.Lerp (fromColor, toColor, now);
			material.SetColor (colorName, nowColor);
		}

		[ContextMenu ("SetTo")]
		public void SetTo () {
			state = State.Finished;
			now = 1f;
			nowColor = Color.Lerp (fromColor, toColor, now);
			material.SetColor (colorName, nowColor);
		}

		[ContextMenu ("ReplayForward")]
		public void ReplayForward () {
			state = State.Forward;
			now = 0f;
		}

		[ContextMenu ("ReplayBackward")]
		public void ReplayBackward () {
			state = State.Backward;
			now = 1f;
		}

		[ContextMenu ("PlayForward")]
		public void PlayForward () {
			state = State.Forward;
		}

		[ContextMenu ("PlayBackward")]
		public void PlayBackward () {
			state = State.Backward;
		}

		void Update () {
			if (state == State.Finished)
				return;
			if (state == State.Forward) {
				now += Time.deltaTime * speed;
			}
			if (state == State.Backward) {
				now -= Time.deltaTime * speed;
			}
			if (now >= 1) {
				now = 1;
				state = State.Finished;
			}
			if (now <= 0) {
				now = 0;
				state = State.Finished;
			}
			nowColor = Color.Lerp (fromColor, toColor, now);
			material.SetColor (colorName, nowColor);
		}
	}
}