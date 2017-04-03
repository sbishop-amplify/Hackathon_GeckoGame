using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Environment : MonoBehaviour {
	//TODO: Replace object with animal class later
	private Dictionary<Food, int> foodSupply = new Dictionary<Food, int>();

	public float CurrentTemp { get; set; }

	public int GetFoodAmount(Food food){
		return foodSupply[food];
	}

	public void AddFood(Food food, int n) {
		if (foodSupply.ContainsKey (food)) {
			foodSupply [food] += n;
		} else {
			foodSupply [food] = n;
		}
	}

	// Removes n food from the supply, or as much as possible, and returns how much removed
	public int RemoveFood(Food food, int n) {
		if (foodSupply.ContainsKey (food)) {
			int avail = foodSupply [food];
			if (avail < n) {
				foodSupply [food] = 0;
				return avail;
			}
			foodSupply [food] = avail - n;
			return n;
		}
		return 0;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
