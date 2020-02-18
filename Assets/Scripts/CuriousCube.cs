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

	MouseController mouse;
	Animator cubeAnimator;
	Material cubeMat;
	Vector3 originalPos;
	Vector3 newPos;

	void Start() {
		mouse = GameObject.Find("Camera").GetComponent<MouseController>();
		cubeAnimator = GetComponent<Animator>();
		cubeMat = transform.Find("Body").GetComponent<MeshRenderer>().material;
		originalPos = transform.position;
		newPos = transform.position;

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

		// StartCoroutine(changeNewPos());
	}

	void FixedUpdate() {
		// ROTATION
		relativeMousePosition = new Vector2(mouse.mousePosition.x - transform.position.x,
			mouse.mousePosition.y - transform.position.y);
		transform.eulerAngles = new Vector3((relativeMousePosition.y * 2), (-relativeMousePosition.x * 2), 0f);

		// FACE ANIMATION
		cubeAnimator.SetFloat("X", -relativeMousePosition.x);
		cubeAnimator.SetFloat("Y", relativeMousePosition.y);


		// SCALE
		mouseDiff = (50f - (Mathf.Pow(Mathf.Abs(relativeMousePosition.x), 1.5f) + Mathf.Pow(Mathf.Abs(relativeMousePosition.y), 1.5f))) * .06f;
		float mouseDiffScale = Mathf.Clamp(mouseDiff, .5f, 1.6f);
		transform.localScale = new Vector3(mouseDiffScale, mouseDiffScale, mouseDiffScale);

		// COLORS
		float mouseDiffColor = Mathf.Clamp(mouseDiff, .2f, 1f);
		Color newColor = new Color(mouseDiffColor * redIntensity, mouseDiffColor * greenIntensity, mouseDiffColor * blueIntensity, 1f);
		cubeMat.SetColor("_Color", newColor);

		// SHAKING
		// transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.fixedDeltaTime);
		transform.position = originalPos + new Vector3(relativeMousePosition.x, relativeMousePosition.y, 0f)
			* mouseDiff * .002f;
	}

	IEnumerator changeNewPos()
	{
		while (true)
		{
			// SHAKING
			newPos = new Vector3(originalPos.x + Random.Range(-5f, 5f),
				originalPos.y + Random.Range(-5f, 5f),
				originalPos.z);

			yield return new WaitForSeconds(.05f);
		}
	}
}
