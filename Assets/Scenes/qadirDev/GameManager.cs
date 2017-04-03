using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private int level = 1;
	private int numLizards = 1;
	private int numDead = 0;
	private float updateOn = 1.0f;
	private float timeElapsed = 0.0f;
	private const int INITIAL_GAME_DURATION = 30; //for thirty seconds
	private const int INITIAL_AMOUNT_OF_LIZARDS = 1;
	private float amountOfTime = INITIAL_GAME_DURATION;

	private void updateGameState(GameEvent gameEvent){
		switch (gameEvent) {
		case GameEvent.gameOver:
			UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");		
			break;
		case GameEvent.animalDied:
			numLizards--;
			numDead++;
			break;
		case GameEvent.levelChanged:
			level++;
			break;
		default:
			return;
		}
	}

	private bool isGameOver(){
		return numLizards < INITIAL_AMOUNT_OF_LIZARDS / 2;
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
			if (isGameOver ())
				updateGameState (GameEvent.gameOver);
			else
				updateGameState (GameEvent.levelChanged);
		}
		if (isGameOver()) {
			updateGameState (GameEvent.gameOver);
		}
	}
}
