using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/Setter/BoolSetter")]
	public class BoolSetter : MonoBehaviour {
		[SerializeField]
		protected bool boolValue = false;

		virtual public bool Value {
			get {
				return boolValue;
			}
			set {
				boolValue = value;
			}
		}

		[SerializeField]
		protected BoolEvent boolEvent;

		virtual public BoolEvent Event {
			get {
				return boolEvent;
			}
		}

		[ContextMenu ("Set")]
		virtual public void Set () {
			boolEvent.Invoke (boolValue);
		}

		virtual public void Set (bool value) {
			boolValue = value;
			boolEvent.Invoke (value);
		}
	}
}
