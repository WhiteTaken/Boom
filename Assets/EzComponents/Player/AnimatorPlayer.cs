using UnityEngine;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/Player/AnimatorPlayer")]
	public class AnimatorPlayer : MonoBehaviour {
		public Animator animatorTarget;

		public Animator AnimatorTarget {
			get {
				return animatorTarget;
			}
			set {
				animatorTarget = value;
			}
		}

		public string stateName;

		public string StateName {
			get {
				return stateName;
			}
			set {
				stateName = value;
			}
		}

		void Start () {
			if (!animatorTarget) {
				animatorTarget = gameObject.GetComponent<Animator> ();
			}
		}

		[ContextMenu ("Play")]
		public void Play () {
			if (!animatorTarget)
				return;
			animatorTarget.Play (stateName);
		}
	}
}