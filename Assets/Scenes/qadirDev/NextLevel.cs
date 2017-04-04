using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour {

	public void StartNextLevel(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("Master");		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			StartNextLevel ();
		}
	}

}
