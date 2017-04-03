using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Environment : MonoBehaviour {
	//TODO: Replace object with animal class later
	private int currentTemp = 0;
	private Dictionary<Food, int> foodSupply = new Dictionary<Food, int>();
	private ArrayList animals = new ArrayList(); 

	public int CurrentTemp { get; set; }

	public int getFoodAmount(Food food){
		return foodSupply[food];
	}

	public int addFood(Food food){
		if (foodSupply [food] != null) {
			int amt = foodSupply [food];
			foodSupply [food] = amt + 1;
		} else {
			foodSupply [food] = 1;
		}
	}

	public int takeFood(Food food){
		if (foodSupply [food] != null) {
			int amt = foodSupply [food];
			foodSupply [food] = amt - 1;
		} else {
			foodSupply [food] = 0;
		}
	}

	public void addAnimal(Animal animal){
		animals.Add (animal);
	}

	public void removeAnimal(Animal animal){
		animals.Remove (animal);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
