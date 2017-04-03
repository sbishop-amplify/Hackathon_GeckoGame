using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

	public void startGame(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("QadirDev");		
		GameManager gm = new GameManager ();
<<<<<<< HEAD
		//gm.UpdateGameState ();
=======
>>>>>>> 4e1853230e90699cad2e3a11232754240222eae1
	}

	// Use this for initialization
	void Start () {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("StartScreen");
	}

	// Update is called once per frame
	void Update () {

	}
}