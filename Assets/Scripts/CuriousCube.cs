using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuriousCube : MonoBehaviour
{
	public Vector2 relativeMousePosition;
	public float mouseDiff;
	public float speed = 1f;
	public float redIntensity = .5f;
	public float greenIntensity = .7f;
	public float blueIntensity = .1f;
	public float mouseDiffScale;
	public float mouseDiffColor;

	public bool isClicking;
	MouseController mouse;
	Animator cubeAnimator;
	Material cubeMat;
	Vector3 originalPos;
	Vector3 newPos;

	void Start() {
		isClicking = false;
		mouse = GameObject.Find("Camera").GetComponent<MouseController>();
		cubeAnimator = GetComponent<Animator>();
		cubeMat = transform.Find("Body").GetComponent<MeshRenderer>().material;
		originalPos = transform.position;
		newPos = transform.position;
		mouseDiffScale = 0f;
		mouseDiffColor = 0f;

		// ASSIGN RANDOM COLOR
		float randomTest = Random.Range(0f, 3f);
		if (randomTest > 2f) {
			redIntensity = .75f;
			greenIntensity = .25f;
			blueIntensity = .5f;
		} else if (randomTest > 1f) {
			redIntensity = .25f;
			greenIntensity = .5f;
			blueIntensity = .75f;
		} else if (randomTest > 0f) {
			redIntensity = .5f;
			greenIntensity = .75f;
			blueIntensity = .25f;
		}
	}

	void Update() {
		isClicking = Input.GetMouseButton(0);

		mouseDiff = (100f - (Mathf.Pow(Mathf.Abs(relativeMousePosition.x), 4f)
			+ Mathf.Pow(Mathf.Abs(relativeMousePosition.y), 4f))) * .06f;
		if (mouseDiff < 0f)
			mouseDiff = 0f;

		relativeMousePosition = new Vector2(mouse.mousePosition.x - transform.position.x,
			mouse.mousePosition.y - transform.position.y);
	}

	void FixedUpdate() {
		// ROTATION
		transform.eulerAngles = new Vector3((relativeMousePosition.y * 2), (-relativeMousePosition.x * 2), 0f);

		// FACE ANIMATION
		cubeAnimator.SetFloat("X", -relativeMousePosition.x);
		cubeAnimator.SetFloat("Y", relativeMousePosition.y);

		// SCALE
		if (isClicking) mouseDiffScale += mouseDiff * 4f * Time.fixedDeltaTime;
		mouseDiffScale -= .1f * Time.fixedDeltaTime;
		mouseDiffScale = Mathf.Clamp(mouseDiffScale, .5f, 1.6f);
		transform.localScale = new Vector3(mouseDiffScale, mouseDiffScale, mouseDiffScale);

		// COLORS
		if (isClicking) mouseDiffColor += mouseDiff * 4f * Time.fixedDeltaTime;
		mouseDiffColor -= .1f * Time.fixedDeltaTime;
		mouseDiffColor = Mathf.Clamp(mouseDiffColor, .2f, 1f);
		Color newColor = new Color(mouseDiffColor * redIntensity, mouseDiffColor * greenIntensity, mouseDiffColor * blueIntensity, 1f);
		cubeMat.SetColor("_Color", newColor);


	}
}
