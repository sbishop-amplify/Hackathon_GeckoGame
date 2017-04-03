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

	private Lizard[] lizards; 
	private Bush[] bushes; 

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
		GameObject[] lizObjects = GameObject.FindGameObjectsWithTag ("Lizard"); 
		lizards = new Lizard[lizObjects.Length]; 
		for(int i = 0; i < lizards.Length; i++)
		{
			lizards [i] = lizObjects [i].GetComponent<Lizard> (); 
		}

		GameObject[] bushObjects = GameObject.FindGameObjectsWithTag ("Bush"); 
		bushes = new Bush[bushObjects.Length]; 
		for(int i = 0; i < bushes.Length; i++)
		{
			bushes [i] = bushObjects [i].GetComponent<Bush> (); 
		}
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;
		if (timeElapsed >= updateOn) {
			timeElapsed = 0.0f;
			amountOfTime -= 1;
			for (int i = 0; i < bushes.Length; i++)
				if(bushes[i] != null)
					bushes [i].DoTick (); 
			for (int i = 0; i < lizards.Length; i++)
				if(lizards[i] != null)
					lizards [i].DoTick (); 
			
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
