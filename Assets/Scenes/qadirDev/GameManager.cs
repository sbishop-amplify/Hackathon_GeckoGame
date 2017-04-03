using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameEvent {
	animalDied, 
	animalIsHot,
	levelChanged,
	gameOver
};


public class GameManager : MonoBehaviour {

	private int level = 1;
	private int numAnimals = 1;
	private int numDead = 0;
	private float updateOn = 1.0f;
	private float timeElapsed = 0.0f;
	private const int INITIAL_GAME_DURATION = 30; //for thirty seconds
	private const int INITIAL_AMOUNT_OF_ANIMALS = 1;
	private float amountOfTime = INITIAL_GAME_DURATION;

<<<<<<< HEAD
=======

>>>>>>> 4e1853230e90699cad2e3a11232754240222eae1
	public void UpdateGameState(GameEvent gameEvent){
		switch (gameEvent) {
		case GameEvent.gameOver:
			UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");		
			break;
		case GameEvent.animalDied:
			numAnimals--;
			numDead++;
			break;
		case GameEvent.levelChanged:
			level++;
			break;
		default:
			return;
		}
	}

	private bool IsGameOver {
		get {
			return numAnimals < INITIAL_AMOUNT_OF_ANIMALS / 2;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;
		if (timeElapsed >= updateOn) {
			timeElapsed = 0.0f;
			amountOfTime -= 1;
		}
		if (amountOfTime <= 0) {
			if (IsGameOver)
				UpdateGameState (GameEvent.gameOver);
			else
				UpdateGameState (GameEvent.levelChanged);
		}
		if (IsGameOver) {
			UpdateGameState (GameEvent.gameOver);
		}
	}
}
