using UnityEngine;
using System.Collections;

namespace EzComponents {
	public class QuickMessenger : MonoBehaviour {
		public GameObject target = null;
		public string message = "";

		[ContextMenu ("Send")]
		public void Send () {
			if (target) {
				target.SendMessage (message, SendMessageOptions.DontRequireReceiver);
			} else {
				gameObject.SendMessage (message, SendMessageOptions.DontRequireReceiver);
			}
		}

		[ContextMenu ("Broadcast")]
		public void Broadcast () {
			if (target) {
				target.BroadcastMessage (message, SendMessageOptions.DontRequireReceiver);
			} else {
				gameObject.BroadcastMessage (message, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}