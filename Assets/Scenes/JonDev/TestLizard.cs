using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLizard : MonoBehaviour {

	public GameObject checkAbove;
	public float tempChange = 0;

	// Use this for initialization
	void Start () {
//		checkAbove = gameObject.AddComponent(CheckAbove);
	}
	
	// Update is called once per frame
	void Update () {
		tempChange = CheckAbove.getCloud (transform,tempChange);
		Debug.Log ("Returned Value: "+tempChange);
	}
}
