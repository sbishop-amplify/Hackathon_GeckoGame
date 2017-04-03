using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

	public void startGame(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("QadirDev");		
	}

	// Use this for initialization
	void Start () {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("StartScreen");
	}

	// Update is called once per frame
	void Update () {

	}
}