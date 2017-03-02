using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace EzComponents {
	public class QuickEvent : MonoBehaviour {
		public UnityEvent onEvent = new UnityEvent ();

		[ContextMenu ("Invoke")]
		public void Invoke () {
			onEvent.Invoke ();
		}
	}
}