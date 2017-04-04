using System;
using System.Collections;
using System.Collections.Generic;

public class Arbol : Plant {
	private static List<KeyValuePair<KeyValuePair<Food, int>, int>> FOOD_LIST = new List<KeyValuePair<KeyValuePair<Food, int>, int>> {
		// Generate 10 leaves every tick
		new KeyValuePair<KeyValuePair<Food, int>, int> (new KeyValuePair<Food, int>(Food.LEAF, 10), 1),
		// Generate 1 acorn every 3 ticks
		new KeyValuePair<KeyValuePair<Food, int>, int> (new KeyValuePair<Food, int>(Food.ACORN, 1), 3)
	};

	public override string Species { get { return "Tree"; } }

	// public override int Shade { get { return 5; } }

	public override List<KeyValuePair<KeyValuePair<Food, int>, int>> FoodList { get { return FOOD_LIST; } }
}
