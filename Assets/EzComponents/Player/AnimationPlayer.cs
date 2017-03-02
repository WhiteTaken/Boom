using UnityEngine;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/Player/AnimationPlayer")]
	public class AnimationPlayer : MonoBehaviour {
		public Animation animationTarget;
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

		[ContextMenu ("ReplayQueued")]
		public void ReplayQueued () {
			if (!animationTarget)
				return;
			Rewind ();
			PlayQueued ();
		}

		[ContextMenu ("Play")]
		public void Play () {
			if (!animationTarget)
				return;
			animationTarget [animationName].layer = animationLayer;
			animationTarget [animationName].wrapMode = animationWrapMode;
			animationTarget [animationName].speed = animationSpeed;
			animationTarget.Play (animationName, animationPlayMode);
		}

		[ContextMenu ("PlayQueued")]
		public void PlayQueued () {
			if (!animationTarget)
				return;
			animationTarget [animationName].layer = animationLayer;
			animationTarget [animationName].wrapMode = animationWrapMode;
			animationTarget [animationName].speed = animationSpeed;
			animationTarget.PlayQueued (animationName, animationQueueMode, animationPlayMode);
		}

		[ContextMenu ("CrossFade")]
		public void CrossFade () {
			if (!animationTarget)
				return;
			animationTarget [animationName].layer = animationLayer;
			animationTarget [animationName].wrapMode = animationWrapMode;
			animationTarget [animationName].speed = animationSpeed;
			animationTarget.CrossFade (animationName, animationFadeLength, animationPlayMode);
		}

		[ContextMenu ("CrossFadeQueued")]
		public void CrossFadeQueued () {
			if (!animationTarget)
				return;
			animationTarget [animationName].layer = animationLayer;
			animationTarget [animationName].wrapMode = animationWrapMode;
			animationTarget [animationName].speed = animationSpeed;
			animationTarget.CrossFadeQueued (animationName, animationFadeLength, animationQueueMode, animationPlayMode);
		}

		[ContextMenu ("Blend")]
		public void Blend () {
			if (!animationTarget)
				return;
			animationTarget [animationName].layer = animationLayer;
			animationTarget [animationName].wrapMode = animationWrapMode;
			animationTarget [animationName].speed = animationSpeed;
			animationTarget.Blend (animationName, animationWeight, animationFadeLength);
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
				animationTarget [animationName].normalizedTime = 0;
			} else {
				animationTarget [animationName].normalizedTime = 1;
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