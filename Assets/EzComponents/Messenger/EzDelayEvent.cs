using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/Messenger/DelayEvent")]
	public class EzDelayEvent : MonoBehaviour {
		[SerializeField]
		protected float delay = 0;

		public float Delay {
			get {
				return delay;
			}
			set {
				delay = value;
			}
		}

		[SerializeField]
		protected UnityEvent onEvent = new UnityEvent ();

		public UnityEvent OnEvent {
			get {
				return onEvent;
			}
		}

		bool delaying = false;
		float now = 0;

		[ContextMenu ("Resend")]
		public void Resend () {
			now = delay;
			delaying = true;
		}

		[ContextMenu ("Send")]
		public void Send () {
			if (now <= 0) {
				now = delay;
			}
			delaying = true;
		}

		[ContextMenu ("Cancel")]
		public void Cancel () {
			now = delay;
			delaying = false;
		}

		[ContextMenu ("Invoke")]
		public void Invoke () {
			now = delay;
			delaying = false;
			onEvent.Invoke ();
		}

		void Update () {
			if (delaying) {
				now -= Time.deltaTime;
				if (now <= 0) {
					now = delay;
					delaying = false;
					onEvent.Invoke ();
				}
			}
		}
	}
}