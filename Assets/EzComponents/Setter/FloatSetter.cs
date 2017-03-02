using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/Setter/FloatSetter")]
	public class FloatSetter : MonoBehaviour {
		[SerializeField]
		protected float floatValue = 0;

		virtual public float Value {
			get {
				return floatValue;
			}
			set {
				floatValue = value;
			}
		}

		[SerializeField]
		protected FloatEvent floatEvent;

		virtual public FloatEvent Event {
			get {
				return floatEvent;
			}
		}

		[ContextMenu ("Set")]
		virtual public void Set () {
			floatEvent.Invoke (floatValue);
		}

		virtual public void Set (float value) {
			floatValue = value;
			floatEvent.Invoke (value);
		}
	}
}
