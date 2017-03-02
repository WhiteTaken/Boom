using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/Setter/TransformSetter")]
	public class TransformSetter : MonoBehaviour {
		[SerializeField]
		protected Transform transformValue = null;

		virtual public Transform Value {
			get {
				return transformValue;
			}
			set {
				transformValue = value;
			}
		}

		[SerializeField]
		protected TransformEvent transformEvent;

		virtual public TransformEvent Event {
			get {
				return transformEvent;
			}
		}

		[ContextMenu ("Set")]
		virtual public void Set () {
			transformEvent.Invoke (transformValue);
		}

		virtual public void Set (Transform value) {
			transformValue = value;
			transformEvent.Invoke (value);
		}
	}
}
