using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAbove {
	/*
	 * Checks if an object is above the lizard
	 */
	public static float getCloud(Transform transform, float currentValue){
		Debug.DrawRay (transform.position, Vector2.up);		// Draws the ray
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, Mathf.Infinity);
		if (hit.collider != null) {
			//Only gets the returned value if it doesn't have any
			if (hit.collider.gameObject.tag == "Cloud" && currentValue == 0) {
				Debug.Log ("Hit");
				Debug.Log (hit.collider);
				float value =  hit.collider.gameObject.GetComponent<TestCloud>().getTemp();
				currentValue = value;
				// Will only get the 
			}

		} else {
			Debug.Log ("No hit");
		
			return 0;
		}
		return currentValue;
	}
}
