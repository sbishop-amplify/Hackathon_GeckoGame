using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Animal {
	private static Dictionary<Food, int> MADE_OF = new Dictionary<Food, int>{ { new Meat (), 100 } };
	private static Food[] DIET = new Food[]{ new Meat(), new Berry(), new Fruit() };

	public override string Species { get { return "Bear"; } }

	public override Dictionary<Food, int> MadeOf { get { return MADE_OF; } }

	public override int MaxHealth { get { return 500; } }

	public override float MinTemp { get { return 20.0f; } }

	public override float MaxTemp { get { return 80.0f; } }

	public override float TempDelta { get { return 2.0f; } }

	public override Food[] Diet { get { return DIET; } }

	public override int MaxFullness { get { return 100; } }

	public override int Metabolism  { get { return 10; } }

	public override int HungerThreshold  { get { return 30; } }

	public override int HealRate { get { return 30; } }
}
