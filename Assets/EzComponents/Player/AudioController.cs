using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/Player/AudioController")]
	public class AudioController : MonoBehaviour {
		[SerializeField]
		protected AudioSource audioTarget;

		public AudioSource AudioTarget {
			get {
				return audioTarget;
			}
			set {
				audioTarget = value;
			}
		}

		[SerializeField]
		protected float smoothTime = 1f;

		public float SmoothTime {
			get {
				return smoothTime;
			}
			set {
				smoothTime = value;
			}
		}

		[SerializeField]
		protected float updateInterval = 0.2f;

		public float UpdateInterval {
			get {
				return updateInterval;
			}
			set {
				updateInterval = value;
			}
		}

		[SerializeField]
		protected FloatEvent onTimeChanged = new FloatEvent ();

		public FloatEvent OnTimeChanged {
			get {
				return onTimeChanged;
			}
		}

		[SerializeField]
		protected FloatEvent onNormalizedTimeChanged = new FloatEvent ();

		public FloatEvent OnNormalizedTimeChanged {
			get {
				return onNormalizedTimeChanged;
			}
		}

		float lastInterval = 0;

		[ContextMenu ("Play")]
		public void Play () {
			if (!audioTarget)
				return;
			audioTarget.Play ();
		}

		[ContextMenu ("Pause")]
		public void Pause () {
			if (!audioTarget)
				return;
			audioTarget.Pause ();
		}

		[ContextMenu ("Stop")]
		public void Stop () {
			if (!audioTarget)
				return;
			audioTarget.Stop ();
		}

		[ContextMenu ("SmoothIn")]
		public void SmoothIn () {
			if (!audioTarget)
				return;
			audioTarget.Play ();
		}

		[ContextMenu ("SmoothOut")]
		public void SmoothOut () {
			if (!audioTarget)
				return;
			audioTarget.Stop ();
		}

		public float GetVolume () {
			if (!audioTarget)
				return 0;
			return audioTarget.volume;
		}

		public void SetVolume (float volume) {
			if (!audioTarget)
				return;
			audioTarget.volume = volume;
			
		}

		public float GetTime () {
			if (!audioTarget)
				return 0;
			return audioTarget.time;
		}

		public void SetTime (float time) {
			if (!audioTarget)
				return;
			audioTarget.time = time;
		}

		public float GetNormalizedTime () {
			if (!audioTarget)
				return 0;
			if (!audioTarget.clip)
				return 0;
			float d = audioTarget.clip.length;
			if (d == 0)
				return 0;
			float p = audioTarget.time;
			return (float)p / (float)d;
		}

		public void SetNormalizedTime (float time) {
			if (!audioTarget)
				return;
			if (!audioTarget.clip)
				return;
			float d = audioTarget.clip.length;
			if (d == 0)
				return;
			audioTarget.time = time * d;
		}

		public float GetDuration () {
			if (!audioTarget)
				return 0;
			if (!audioTarget.clip)
				return 0;
			return audioTarget.clip.length;
		}

		void Start () {
			if (!audioTarget)
				audioTarget = GetComponent<AudioSource> ();
			if (!audioTarget)
				enabled = false;
		}

		void Update () {
			if (audioTarget && audioTarget.isPlaying && audioTarget.clip) {
				lastInterval += Time.deltaTime;
				if (lastInterval >= updateInterval) {
					lastInterval = 0;
					onTimeChanged.Invoke (GetTime ());
					onNormalizedTimeChanged.Invoke (GetNormalizedTime ());
				}
			}
		}
	}
}