using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace EzComponents {
	public class EzMessageReceiver : MonoBehaviour {
		public string listen = "";
		public UnityEvent onMessage = new UnityEvent ();

		[ContextMenu ("Invoke")]
		public void Invoke () {
			onMessage.Invoke ();
		}

		public void EzMessage (string message) {
			if (message.Equals (listen)) {
				onMessage.Invoke ();
			}
		}
	}
}
