using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food {

	public static readonly Food ACORN = new Food("Acorn", 3);
	public static readonly Food BERRY = new Food("Berry", 3);
	public static readonly Food BUGS = new Food("Bugs", 1);
	public static readonly Food FRUIT = new Food("Fruit", 5);
	public static readonly Food LEAF = new Food("Leaf", 2);
	public static readonly Food MEAT = new Food("Meat", 5);

	public Food(string name, int satiation) {
		Name = name;
		Satiation = satiation;
	}

	public string Name { get; private set; }

	// How much the food fills us per unit
	public int Satiation { get; private set; }

	public override bool Equals (object obj) {
		return (obj is Food) && Name.Equals((obj as Food).Name);
	}

	public override string ToString () {
		return Name;
	}

	public override int GetHashCode() {
		return Name.GetHashCode ();
	}
}
