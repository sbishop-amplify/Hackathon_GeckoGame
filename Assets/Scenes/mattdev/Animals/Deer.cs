using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : Animal {
	private static Dictionary<Food, int> MADE_OF = new Dictionary<Food, int>{ { Food.MEAT, 50 } };
	private static Food[] DIET = new Food[]{ Food.LEAF, Food.BERRY };

	public override string Species { get { return "Deer"; } }

	public override Dictionary<Food, int> MadeOf { get { return MADE_OF; } }

	public override int MaxHealth { get { return 300; } }

	public override float MinTemp { get { return 30.0f; } }

	public override float MaxTemp { get { return 75.0f; } }

	public override float TempDelta { get { return 3.0f; } }

	public override Food[] Diet { get { return DIET; } }

	public override int MaxFullness { get { return 50; } }

	public override int Metabolism  { get { return 5; } }

	public override int HungerThreshold  { get { return 25; } }

	public override int HealRate { get { return 20; } }
}
