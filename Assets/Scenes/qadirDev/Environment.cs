using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Environment : MonoBehaviour {
	//TODO: Replace object with animal class later
	private int currentTemp = 0;
	private Dictionary<Object, int> foodSupply = new Dictionary<Object, int>();
	private ArrayList animals = new ArrayList(); 

	public int getCurrentTemp(){
		return currentTemp;
	}

	public int getFoodAmount(Object food){
		return foodSupply[food];
	}

	public void addAnimals(Object animal){
		animals.Add (animal);
	}

	public void removeAnimal(Object animal){
		animals.Remove (animal);
	}

	public int changeCurrentTemp(int change){
		currentTemp += change;
		return currentTemp;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
