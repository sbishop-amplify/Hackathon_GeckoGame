using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudClass : MonoBehaviour {

	public CloudClass (string type, float temp) {
		this.Type = type;
		this.TempChange = temp;
	}

	public string Type { get; private set; }
	public float TempChange { get; private set; }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
