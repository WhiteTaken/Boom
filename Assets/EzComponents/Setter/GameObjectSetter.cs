using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/Setter/GameObjectSetter")]
	public class GameObjectSetter : MonoBehaviour {
		[SerializeField]
		protected GameObject gameObjectValue = null;

		virtual public GameObject Value {
			get {
				return gameObjectValue;
			}
			set {
				gameObjectValue = value;
			}
		}

		[SerializeField]
		protected GameObjectEvent gameObjectEvent;

		virtual public GameObjectEvent Event {
			get {
				return gameObjectEvent;
			}
		}

		[ContextMenu ("Set")]
		virtual public void Set () {
			gameObjectEvent.Invoke (gameObjectValue);
		}

		virtual public void Set (GameObject value) {
			gameObjectValue = value;
			gameObjectEvent.Invoke (value);
		}
	}
}
