using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Linq; 

public enum GameEvent {
	animalDied, 
	animalIsHot,
	levelChanged,
	gameOver
};


public class GameManager : MonoBehaviour {

	private int level;//= UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
	private int numDead ;//= 0;
	private float updateOn ;//= 1.0f;
	private float timeElapsed ;//= 0.0f;
	private int INITIAL_GAME_DURATION ;//= 30 * level; //for thirty seconds, JUST FOR NOW	
	private int INITIAL_AMOUNT_OF_ANIMALS ;//= 1 * level; //JUST FOR NOW
	private int numAnimals ;//= INITIAL_AMOUNT_OF_ANIMALS;
	private float amountOfTime ;//= INITIAL_GAME_DURATION;

	private Animal[] animals; 
	private Plant[] plants; 

	void Awake()
	{
		/*GameObject[] lizObjects = GameObject.FindGameObjectsWithTag ("Animal"); 
		animals = new Animal[lizObjects.Length]; 
		for(int i = 0; i < animals.Length; i++)
		{
			animals [i] = lizObjects [i].GetComponent<Animal> (); 
		}

		GameObject[] bushObjects = GameObject.FindGameObjectsWithTag ("Plant"); 
		plants = new Plant[bushObjects.Length]; 
		for(int i = 0; i < plants.Length; i++)
		{
			plants [i] = bushObjects [i].GetComponent<Plant> (); 
		}*/

		animals = GameObject.FindGameObjectsWithTag ("Animal").Select (g => g.GetComponent<Animal> ()).ToArray ();
		plants = GameObject.FindGameObjectsWithTag ("Plant").Select(g => g.GetComponent<Plant> ()).ToArray ();


		level = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
		numDead = 0;
		updateOn = 1.0f;
		timeElapsed = 0.0f;
		INITIAL_GAME_DURATION = 30 * level; //for thirty seconds, JUST FOR NOW	
		INITIAL_AMOUNT_OF_ANIMALS = animals.Length;; //JUST FOR NOW
		numAnimals = INITIAL_AMOUNT_OF_ANIMALS;
		amountOfTime = INITIAL_GAME_DURATION;
	}

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
			//level++;
			UnityEngine.SceneManagement.SceneManager.LoadScene("LevelTransition");		
			break;
		default:
			return;
		}
	}

	private int AnimalsAlive {
		get { return animals.Count (a => a != null && a.IsAlive); }
	}

	private bool IsGameOver {
		get {
			return AnimalsAlive <= INITIAL_AMOUNT_OF_ANIMALS / 2;
		}
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;
		if (timeElapsed >= updateOn) {
			timeElapsed = 0.0f;
			amountOfTime -= 1;
			for (int i = 0; i < plants.Length; i++)
				if(plants[i] != null)
					plants [i].DoTick (); 
			for (int i = 0; i < animals.Length; i++)
				if(animals[i] != null)
					animals [i].DoTick (); 
			
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
