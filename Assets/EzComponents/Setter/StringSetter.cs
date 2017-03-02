using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/Setter/StringSetter")]
	public class StringSetter : MonoBehaviour {
		[SerializeField]
		protected string stringValue = "";

		virtual public string Value {
			get {
				return stringValue;
			}
			set {
				stringValue = value;
			}
		}

		[SerializeField]
		protected StringEvent stringEvent;

		virtual public StringEvent Event {
			get {
				return stringEvent;
			}
		}

		[ContextMenu ("Set")]
		virtual public void Set () {
			stringEvent.Invoke (stringValue);
		}

		virtual public void Set (string value) {
			stringValue = value;
			stringEvent.Invoke (value);
		}
	}
}