using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/Messenger/RuntimeEvents")]
	public class EzRuntimeEvents : MonoBehaviour {

		[SerializeField]
		protected UnityEvent startEvent = new UnityEvent ();

		public UnityEvent StartEvent {
			get {
				return startEvent;
			}
		}

		[SerializeField]
		protected UnityEvent onEnableEvent = new UnityEvent ();

		public UnityEvent OnEnableEvent {
			get {
				return onEnableEvent;
			}
		}

		[SerializeField]
		protected UnityEvent onDisableEvent = new UnityEvent ();

		public UnityEvent OnDisableEvent {
			get {
				return onDisableEvent;
			}
		}

		void Start () {
			startEvent.Invoke ();
		}

		void OnEnable () {
			onEnableEvent.Invoke ();
		}

		void OnDisable () {
			onDisableEvent.Invoke ();

		}

	}
}