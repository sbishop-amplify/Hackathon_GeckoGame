using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudClass : MonoBehaviour {
	public float tempChange;
	public string type;

	public CloudClass (string type, float temp) {
		this.type = type;
		this.tempChange = temp;
	}

	public float getTempChange(){
		return this.tempChange;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
