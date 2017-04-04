using System;
using System.Collections;
using System.Collections.Generic;

public class AppleTree : Plant {
	private static List<KeyValuePair<KeyValuePair<Food, int>, int>> FOOD_LIST = new List<KeyValuePair<KeyValuePair<Food, int>, int>> {
		// Generate 5 leaves every tick
		new KeyValuePair<KeyValuePair<Food, int>, int> (new KeyValuePair<Food, int>(Food.LEAF, 5), 1),
		// Generate 1 fruit every 3 ticks
		new KeyValuePair<KeyValuePair<Food, int>, int> (new KeyValuePair<Food, int>(Food.FRUIT, 1), 3),
		// Generate 7 bugs every 3 ticks. They like the fruits.
		new KeyValuePair<KeyValuePair<Food, int>, int> (new KeyValuePair<Food, int>(Food.BUGS, 7), 3)
	};

	public override string Species { get { return "Apple Tree"; } }

	// public override int Shade { get { return 3; } }

	public override List<KeyValuePair<KeyValuePair<Food, int>, int>> FoodList { get { return FOOD_LIST; } }
}
