using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
	public Vector3 mousePosition;

	public float reactionSpeed = 3f;

	void Update() {
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(mouseRay, out hit)) {
			mousePosition = Vector2.Lerp(mousePosition, hit.point, reactionSpeed * Time.deltaTime);
		}
	}
}
