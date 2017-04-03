using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Animal {
	private static Dictionary<Food, int> MADE_OF = new Dictionary<Food, int>{ { new Meat (), 8 } };
	private static Food[] DIET = new Food[]{ new Fruit(), new Bugs() };

	public override string Species { get { return "Bat"; } }

	public override Dictionary<Food, int> MadeOf { get { return MADE_OF; } }

	public override int MaxHealth { get { return 60; } }

	public override float MinTemp { get { return 60.0f; } }

	public override float MaxTemp { get { return 90.0f; } }

	public override float TempDelta { get { return 5.0f; } }

	public override Food[] Diet { get { return DIET; } }

	public override int MaxFullness { get { return 15; } }

	public override int Metabolism  { get { return 3; } }

	public override int HungerThreshold  { get { return 8; } }

	public override int HealRate { get { return 5; } }
}
