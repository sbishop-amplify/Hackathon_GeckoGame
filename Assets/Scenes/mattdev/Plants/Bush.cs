using System;
using System.Collections;
using System.Collections.Generic;

public class Bush : Plant {
	private static List<KeyValuePair<KeyValuePair<Food, int>, int>> FOOD_LIST = new List<KeyValuePair<KeyValuePair<Food, int>, int>> {
		// Generate 3 leaves every tick
		new KeyValuePair<KeyValuePair<Food, int>, int> (new KeyValuePair<Food, int>(Food.LEAF, 3), 1),
		// Generate 4 berries every 5 ticks
		new KeyValuePair<KeyValuePair<Food, int>, int> (new KeyValuePair<Food, int>(Food.ACORN, 4), 5),
		// Generate 5 bugs every 5 ticks. They like the berries.
		new KeyValuePair<KeyValuePair<Food, int>, int> (new KeyValuePair<Food, int>(Food.BUGS, 5), 5)
	};

	public override string Species { get { return "Bush"; } }

	// public override int Shade { get { return 0; } }

	public override List<KeyValuePair<KeyValuePair<Food, int>, int>> FoodList { get { return FOOD_LIST; } }
}
