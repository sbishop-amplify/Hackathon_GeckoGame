using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgain : MonoBehaviour {

	public void RestartGame (){
		UnityEngine.SceneManagement.SceneManager.LoadScene("Master");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RestartGame ();
		}
	}
}
