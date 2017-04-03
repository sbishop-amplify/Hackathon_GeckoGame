using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour {
	// A constant for percent of damage taken each turn while starving
	private const int STARVE_DAMAGE = 5;

	private Environment MyEnvironment = GameObject.Find ("Environment").GetComponent<Environment> ();
	private Grid MyGrid = GameObject.Find ("GridObject").GetComponent<Grid> ();
	private GameManager Manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();

	public Animal() {
		// Default name chosen Pokeyman style
		Name = Species;
		// Default body temp is center comfort range
		BodyTemp = (MaxTemp - MinTemp) / 2;
		// Default health and fullness is full
		health = MaxHealth;
		fullness = MaxFullness;
	}

	////////////////////////
	/// BASIC PROPERTIES ///
	////////////////////////

	// WHAT ARE WE?
	public abstract string Species { get; }
	// Maybe the user can name us? :3
	public string Name { get; set; }
	// WHAT WE'RE MADE OF. PROBABLY MEAT.
	public abstract Dictionary<Food, int> MadeOf { get; }
	// public abstract string Description { get; }

	////////////////////
	/// HEALTH STUFF ///
	////////////////////

	private int health;
	public int Health {
		get { return health; }
		set {
			if (value < 0) {
				value = 0;
			} else if (value > MaxHealth) {
				value = MaxHealth;
			}
			health = value;
		}
	}
	public abstract int MaxHealth { get; }

	/////////////////////////
	/// TEMPERATURE STUFF ///
	/////////////////////////

	// Current body temperature
	public float BodyTemp { get; set; }
	public abstract float MinTemp { get; }
	public abstract float MaxTemp { get; }
	// The amount that body temp fluctuates
	// each tick to adjust to surroundings
	public abstract float TempDelta { get; }

	// Converge body temperature on surroundings,
	// and apply damage if it's too hot or cold
	public void CheckTemp(float currentTemp) {
		// Alter our body temperature if we don't match
		if (BodyTemp != currentTemp) {
			if (currentTemp > BodyTemp) {
				float change = BodyTemp + TempDelta;
				BodyTemp = change < currentTemp ? change : currentTemp;
			} else {
				float change = BodyTemp - TempDelta;
				BodyTemp = change > currentTemp ? change : currentTemp;
			}
		}

		// How far off are we?
		float howBad = 0.0f;
		if (BodyTemp < MinTemp) {
			howBad = MinTemp - currentTemp;
		} else if(BodyTemp > MaxTemp){
			howBad = currentTemp - MaxTemp;
		}
		if (howBad > 0.0) {
			// Gonna take some ouchies
			float range = MaxTemp - MinTemp;
			if (range < 1.0) {
				// Most damage you can take is ALL OF IT
				range = 1.0f;
			}
			int damage = (int)(MaxHealth * howBad / range);
			Health -= damage > 0 ? damage : 1;
		}
	}

	//////////////////
	/// FOOD STUFF ///
	//////////////////

	// A list of what foods we will eat
	public abstract Food[] Diet { get; }
	// Our current food store
	private int fullness;
	public int Fullness {
		get { return fullness; }
		set {
			if (value < 0) {
				value = 0;
			} else if (value > MaxFullness) {
				value = MaxFullness;
			}
			health = value;
		}
	}
	// How much food we can store, max
	public abstract int MaxFullness { get; }
	// How much food we burn off each tick,
	// and percent of max health to restore
	public abstract int Metabolism { get; }
	// At what fullness will we eat
	public abstract int HungerThreshold { get; }
	// Percent that you heal every turn you can metabolise
	public abstract int HealRate { get; }

	// Matabolize food into health, then, if hungry, eat or take damage.
	public void CheckHunger() {
		// Restore HealRate% of health each turn (if we have enough food)
		int burnOff = Fullness > Metabolism ? Metabolism : Fullness;
		// Heal if we burn off any food
		if (burnOff > 0) {
			// Amount that you heal, arranged confusingly for int division.
			// Logic is: MaxHealth * (HealRate/100) * (burnOff/Metabolism)
			int heal = (MaxHealth * HealRate * burnOff) / (100 * Metabolism);
			// Burn off food and heal
			Fullness -= burnOff;
			Health += heal;
			// Report a heal
		}

		if (Fullness < HungerThreshold) {
			// We're HUNGRY. Look for food we'll eat
			foreach (Food f in Diet) {
				// How much we want to eat (currently to full)
				int hunger = MaxFullness - Fullness;
				// How many units we want to eat
				int desired = (hunger + f.Satiation - 1) / f.Satiation;
				// Eat as much as we want/can
				int ate = MyEnvironment.RemoveFood(f, desired);
				Fullness += ate * f.Satiation;
				if (Fullness == MaxFullness) break;
			}
		}
		// Check if we're starving to death.
		if (burnOff < 1 && Starving) {
			// Take a percentage of max health as damage
			int damage = (MaxHealth * STARVE_DAMAGE) / 100;
			Health -= damage > 0 ? damage : 1;
		}
	}

	////////////////////
	/// STATUS STUFF ///
	////////////////////

	// We good bro?
	public bool IsAlive { get { return Health > 0; } }
	public bool TooCold { get { return BodyTemp < MinTemp; } }
	public bool TooHot { get { return BodyTemp > MinTemp; } }
	public bool Hungry { get { return Fullness < HungerThreshold; } }
	public bool Starving { get { return Fullness < 1; } }

	//////////////////////
	/// ACTIVITY STUFF ///
	//////////////////////

	public void DoTick() {
		// Check the temperature where we are
		var xy = gameObject.transform.position;
		float temp = MyGrid.GetTemp (xy.x, xy.y);
		CheckTemp(temp);
		// Might make more sense to check for death after every step
		// Check for and eat food if we're hungry
		CheckHunger();

		if (IsAlive) {
			if (TooCold) {
				// Report
			} else if (TooHot) {
				// Report
			}
			if (Starving) {
				// Report
			} else if (Hungry) {
				// Report
			}
		} else {
			Die();
		}
	}

	private void Die() {
		// Pass in location/environment
		// Add contents of body to environment
		foreach(KeyValuePair<Food, int> food in MadeOf) {
			MyEnvironment.AddFood(food.Key, food.Value);
		}
		Manager.updateGameState (GameEvent.animalDied);
		Destroy(this.gameObject);
	}

	public override string ToString()
	{
		return Name.Equals(Species) ? Name : string.Format("{0} ({1})", Name, Species);
	}
}
