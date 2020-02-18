using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuriousCube : MonoBehaviour
{
	public Vector2 relativeMousePosition;
	public float mouseDiff;
	public float speed = 1f;

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

		StartCoroutine(changeNewPos());
	}

	void FixedUpdate() {
		// ROTATION
		relativeMousePosition = new Vector2(mouse.mousePosition.x - transform.position.x,
			mouse.mousePosition.y - transform.position.y);

		cubeAnimator.SetFloat("X", -relativeMousePosition.x);
		cubeAnimator.SetFloat("Y", relativeMousePosition.y);

		transform.eulerAngles = new Vector3((relativeMousePosition.y * 2), (-relativeMousePosition.x * 2), 0f);

		// SCALE
		mouseDiff = (50f - (Mathf.Pow(Mathf.Abs(relativeMousePosition.x), 1.5f) + Mathf.Pow(Mathf.Abs(relativeMousePosition.y), 1.5f))) * .06f;
		mouseDiff = Mathf.Clamp(mouseDiff, .5f, 1.8f);
		transform.localScale = new Vector3(mouseDiff, mouseDiff, mouseDiff);

		// COLORS
		Color newColor = new Color(mouseDiff * .7f, mouseDiff * .5f, mouseDiff * .1f, 1f);
		cubeMat.SetColor("_Color", newColor);

		// SHAKING
		transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.fixedDeltaTime);
	}

	IEnumerator changeNewPos()
	{
		while (true)
		{
			// SHAKING
			newPos = new Vector3(originalPos.x + Random.Range(-10f, 10f),
				originalPos.y + Random.Range(-10f, 10f),
				originalPos.z);

			yield return new WaitForSeconds(.1f);
		}
	}
}
