using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/Setter/IntSetter")]
	public class IntSetter : MonoBehaviour {
		[SerializeField]
		protected int intValue = 0;

		virtual public int Value {
			get {
				return intValue;
			}
			set {
				intValue = value;
			}
		}

		[SerializeField]
		protected IntEvent intEvent;

		virtual public IntEvent Event {
			get {
				return intEvent;
			}
		}

		[ContextMenu ("Set")]
		virtual public void Set () {
			intEvent.Invoke (intValue);
		}

		virtual public void Set (int value) {
			intValue = value;
			intEvent.Invoke (value);
		}
	}
}