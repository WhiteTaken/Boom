using UnityEngine;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/Player/AnimationVirtualClipPlayer")]
	public class AnimationVirtualClipPlayer : MonoBehaviour {
		public Animation animationTarget;
		public float startTime = 0f;
		public float endTime = 2f;
		public string animationName = "";
		public float animationSpeed = 1;
		public int animationLayer = 0;
		public float animationWeight = 1f;
		public float animationFadeLength = 0.2f;
		public PlayMode animationPlayMode = PlayMode.StopSameLayer;
		public QueueMode animationQueueMode = QueueMode.CompleteOthers;
		public WrapMode animationWrapMode = WrapMode.ClampForever;

		void Start () {
			if (!animationTarget) {
				animationTarget = GetComponent<Animation> ();
			}
			if (animationTarget && animationName.Equals ("")) {
				animationName = animationTarget.clip.name;
			}
			if (animationTarget) {
				Rewind ();
			} else {
				enabled = false;
			}
		}

		void Update () {
			if (!animationTarget)
				return;
			if (!animationTarget.isPlaying)
				return;
			if (animationTarget [animationName].time >= endTime) {
				Stop ();
				animationTarget [animationName].time = endTime;
				animationTarget.Sample ();
				Debug.Log (animationTarget [animationName].time);
			}
		}

		[ContextMenu ("Sample")]
		public void Sample () {
			if (!animationTarget)
				return;
			bool lastState = animationTarget [animationName].enabled;
			animationTarget [animationName].enabled = true;
			animationTarget.Sample ();
			animationTarget [animationName].enabled = lastState;
		}

		[ContextMenu ("Replay")]
		public void Replay () {
			if (!animationTarget)
				return;
			Rewind ();
			Play ();
		}

		[ContextMenu ("Play")]
		public void Play () {
			if (!animationTarget)
				return;
			animationTarget [animationName].time = startTime;
			animationTarget [animationName].layer = animationLayer;
			animationTarget [animationName].wrapMode = animationWrapMode;
			animationTarget [animationName].speed = animationSpeed;
			animationTarget.Play (animationName, animationPlayMode);
		}


		[ContextMenu ("Pause")]
		public void Pause () {
			if (!animationTarget)
				return;
			animationTarget [animationName].speed = 0f;
		}

		[ContextMenu ("Rewind")]
		public void Rewind () {
			if (!animationTarget)
				return;
			if (animationSpeed > 0) {
				animationTarget [animationName].time = startTime;
			} else {
				animationTarget [animationName].time = endTime;
			}
		}

		[ContextMenu ("Stop")]
		public void Stop () {
			if (!animationTarget)
				return;
			animationTarget.Stop (animationName);
		}
	}
}