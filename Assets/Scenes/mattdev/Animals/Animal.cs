
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour {
	// A constant for percent of damage taken each turn while starving
	private const int STARVE_DAMAGE = 5;
	private Environment MyEnvironment;
	private GameManager Manager;

	void Awake()
	{
		MyEnvironment = GameObject.FindGameObjectWithTag ("Environment").GetComponent<Environment> ();
		Manager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}

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

			Debug.Log(string.Format("{0} HP: {1} -> {2} (out of {3})", Name, health, value, MaxHealth));
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
		Debug.Log(string.Format("Checking temp of {0} against environment temp ({1})", Name, currentTemp));
		// Alter our body temperature if we don't match
		if (BodyTemp != currentTemp) {
			float prev = BodyTemp;
			Debug.Log(string.Format("Body temp adjusting from {0}...", BodyTemp));
			if (currentTemp > BodyTemp) {
				float change = BodyTemp + TempDelta;
				BodyTemp = change < currentTemp ? change : currentTemp;
			} else {
				float change = BodyTemp - TempDelta;
				BodyTemp = change > currentTemp ? change : currentTemp;
			}
			Debug.Log(string.Format("{0} BodyTemp: {1} -> {2}", Name, prev, BodyTemp));
		}

		// How far off are we?
		float howBad = 0.0f;
		if (BodyTemp < MinTemp) {
			howBad = MinTemp - currentTemp;
		} else if(BodyTemp > MaxTemp){
			howBad = currentTemp - MaxTemp;
		}
		if (howBad > 0.0) {
			Debug.Log(string.Format("{0} BodyTemp is {1} off from range of {2}-{3}", Name, howBad, MinTemp, MaxTemp));
			// Gonna take some ouchies
			float range = MaxTemp - MinTemp;
			if (range < 1.0) {
				// Most damage you can take is ALL OF IT
				range = 1.0f;
			}
			int damage = (int)(MaxHealth * howBad / range);
			Debug.Log(string.Format("{0} took temp damage: {1} (({2}*{3})/{4})", Name, damage, MaxHealth, howBad, range));
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
			Debug.Log(string.Format("{0} Fullness: {1} -> {2} (out of {3})", Name, fullness, value, MaxFullness));
			fullness = value;
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
		Debug.Log(string.Format("Checking {0} hunger", Name));
		// Restore HealRate% of health each turn (if we have enough food)
		int burnOff = Fullness > Metabolism ? Metabolism : Fullness;
		// Heal if we burn off any food
		if (burnOff > 0) {
			Debug.Log(string.Format("{0} food burnoff: {1}", Name, burnOff));
			// Amount that you heal, arranged confusingly for int division.
			// Logic is: MaxHealth * (HealRate/100) * (burnOff/Metabolism)
			int heal = (MaxHealth * HealRate * burnOff) / (100 * Metabolism);
			// Burn off food and heal
			Fullness -= burnOff;
			Health += heal;
			// Report a heal
		}

		if (Fullness < HungerThreshold) {
			Debug.Log(string.Format("{0} is hungry!", Name));
			// We're HUNGRY. Look for food we'll eat
			foreach (Food f in Diet) {
				// How much we want to eat (currently to full)
				int hunger = MaxFullness - Fullness;
				// How many units we want to eat
				int desired = (hunger + f.Satiation - 1) / f.Satiation;
				// Eat as much as we want/can
				int ate = MyEnvironment.RemoveFood(f, desired);
				if (ate > 0) {
					Debug.Log(string.Format("{0} ate {1} {2}(s)", Name, ate, f));
					Fullness += ate * f.Satiation;
				}
				if (Fullness == MaxFullness) break;
			}
		}
		// Check if we're starving to death.
		if (burnOff < 1 && Starving) {
			Debug.Log(string.Format("{0} is starving!", Name));
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
	public bool TooHot { get { return BodyTemp > MaxTemp; } }
	public bool Hungry { get { return Fullness < HungerThreshold; } }
	public bool Starving { get { return Fullness < 1; } }

	//////////////////////
	/// ACTIVITY STUFF ///
	//////////////////////

	public void DoTick() {
		// Check the temperature where we are
		var xy = gameObject.transform.position;
		float temp = MyEnvironment.GetTemp (xy.x, xy.y);
		CheckTemp(temp);
		if (!IsAlive) {
			if (TooCold) {
				Debug.Log(string.Format("{0} froze to death!", Name));
			} else {
				Debug.Log(string.Format("{0} suffered a heat stroke!", Name));
			}
			Die ();
			return;
		}

		// Check for and eat food if we're hungry
		CheckHunger();
		if (!IsAlive) {
			Debug.Log(string.Format("{0} starved to death!", Name));
			Die ();
			return;
		}

		// We survived!
		if (TooCold) {
			Debug.Log(string.Format("{0} is too cold!", Name));
			// Report
		} else if (TooHot) {
			Debug.Log(string.Format("{0} is too hot!", Name));
			// Report
		}
		if (Starving) {
			Debug.Log(string.Format("{0} is starving!", Name));
			// Report
		} else if (Hungry) {
			Debug.Log(string.Format("{0} is hungry!", Name));
			// Report
		}
	}

	private void Die() {
		// Pass in location/environment
		// Add contents of body to environment
		foreach(KeyValuePair<Food, int> food in MadeOf) {
			Debug.Log(string.Format("{0} dropped {1} {2}(s)", Name, food.Value, food.Key));
			MyEnvironment.AddFood(food.Key, food.Value);
		}
		Manager.UpdateGameState (GameEvent.animalDied);
		Destroy(this.gameObject);
	}

	public override string ToString()
	{
		return Name.Equals(Species) ? Name : string.Format("{0} ({1})", Name, Species);
	}
}