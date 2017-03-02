using UnityEngine;
using System.Collections;

namespace EzComponents {
	public class EzMessageSender : MonoBehaviour {
		public GameObject target = null;
		public string message = "";

		[ContextMenu ("Send")]
		public void Send () {
			if (target) {
				target.SendMessage ("EzMessage", message, SendMessageOptions.DontRequireReceiver);
			} else {
				gameObject.SendMessage ("EzMessage", message, SendMessageOptions.DontRequireReceiver);
			}
		}

		[ContextMenu ("Broadcast")]
		public void Broadcast () {
			if (target) {
				target.BroadcastMessage ("EzMessage", message, SendMessageOptions.DontRequireReceiver);
			} else {
				gameObject.BroadcastMessage ("EzMessage", message, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}