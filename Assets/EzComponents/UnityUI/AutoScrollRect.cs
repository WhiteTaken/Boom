using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace EzComponents {
	[AddComponentMenu ("EzComponents/UnityUI/AutoScrollRect")]
	[RequireComponent (typeof(ScrollRect))]
	public class AutoScrollRect : MonoBehaviour {
		ScrollRect sr = null;
		CanvasRenderer cr = null;

		[SerializeField]
		protected float pixelSpeed = 20.0f;

		public float PixelSpeed {
			get {
				return pixelSpeed;
			}
			set {
				pixelSpeed = value;
			}
		}

		[SerializeField]
		protected ScrollAxis axis = ScrollAxis.X;

		public ScrollAxis Axis {
			get {
				return axis;
			}
			set {
				axis = value;
			}
		}

		public enum ScrollAxis {
			X,
			Y
		}

		[SerializeField]
		protected bool pingPong = false;

		public bool PingPong {
			get {
				return pingPong;
			}
			set {
				if (cr)
					cr.SetAlpha (1);
				pingPong = value;
			}
		}

		[SerializeField]
		protected float holdIn = 0.5f;

		public float HoldIn {
			get {
				return holdIn;
			}
			set {
				holdIn = value;
			}
		}

		[SerializeField]
		protected float holdOut = 0.5f;

		public float HoldOut {
			get {
				return holdOut;
			}
			set {
				holdOut = value;
			}
		}

		protected float alphaSpeed = 2f;
		protected bool flashIn = false;
		protected bool flashOut = false;
		protected float hold = 0f;

		void Start () {
			sr = GetComponent<ScrollRect> ();
			cr = sr.content.GetComponent<CanvasRenderer> ();
		}

		void Update () {
			if (!flashIn && !flashOut) {
				if (pixelSpeed == 0)
					return;
				if (axis == ScrollAxis.X) {
					float w = sr.content.rect.width - sr.viewport.rect.width;
					if (w > 0) {
						float stepX = pixelSpeed * Time.deltaTime / w;
						float next = sr.horizontalNormalizedPosition + stepX;
						if (next < 0) {
							next = 0;
						}
						if (next > 1) {
							next = 1;
						}
						sr.horizontalNormalizedPosition = next;
						if (pixelSpeed < 0 && sr.horizontalNormalizedPosition <= 0) {
							sr.horizontalNormalizedPosition = 0;
							if (pingPong) {
								pixelSpeed = -pixelSpeed;
							} else {
								flashOut = true;
							}
						}

						if (pixelSpeed > 0 && sr.horizontalNormalizedPosition >= 1) {
							sr.horizontalNormalizedPosition = 1;
							if (pingPong) {
								pixelSpeed = -pixelSpeed;
							} else {
								flashOut = true;
							}
						}
					}
				}
				if (axis == ScrollAxis.Y) {
					float h = sr.content.rect.height - sr.viewport.rect.height;
					if (h > 0) {
						float stepY = pixelSpeed * Time.deltaTime / h;
						float next = sr.verticalNormalizedPosition + stepY;
						if (next < 0) {
							next = 0;
						}
						if (next > 1) {
							next = 1;
						}
						sr.verticalNormalizedPosition = next;
						if (pixelSpeed < 0 && sr.verticalNormalizedPosition <= 0) {
							sr.verticalNormalizedPosition = 0;
							if (pingPong) {
								pixelSpeed = -pixelSpeed;
							} else {
								flashOut = true;
							}
						}
						if (pixelSpeed > 0 && sr.verticalNormalizedPosition >= 1) {
							sr.verticalNormalizedPosition = 1;
							if (pingPong) {
								pixelSpeed = -pixelSpeed;
							} else {
								flashOut = true;
							}
						}
					}
				}
			} else {
				if (!cr) {
					flashIn = false;
					flashOut = false;
				}
				if (flashIn) {
					cr.SetAlpha (cr.GetAlpha () + alphaSpeed * Time.deltaTime);
					if (cr.GetAlpha () >= 1) {
						cr.SetAlpha (1);
						hold += Time.deltaTime;
						if (hold >= holdOut) {
							hold = 0;
							flashIn = false;
						}
					}
				}
				if (flashOut) {
					hold += Time.deltaTime;
					if (hold >= holdOut) {
						cr.SetAlpha (cr.GetAlpha () + -alphaSpeed * Time.deltaTime);
						if (cr.GetAlpha () <= 0) {
							cr.SetAlpha (0);
							if (axis == ScrollAxis.X) {
								if (pixelSpeed < 0) {
									sr.horizontalNormalizedPosition = 1;
									hold = 0;
									flashIn = true;
									flashOut = false;
								}
								if (pixelSpeed > 0) {
									sr.horizontalNormalizedPosition = 0;
									hold = 0;
									flashIn = true;
									flashOut = false;
								}
							}
							if (axis == ScrollAxis.Y) {
								if (pixelSpeed < 0) {
									sr.verticalNormalizedPosition = 1;
									hold = 0;
									flashIn = true;
									flashOut = false;
								}
								if (pixelSpeed > 0) {
									sr.verticalNormalizedPosition = 0;
									hold = 0;
									flashIn = true;
									flashOut = false;
								}
							}
						}
					}
				}
			}
		}
	}
}