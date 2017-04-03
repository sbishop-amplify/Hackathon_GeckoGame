using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Animal {
	private static Dictionary<Food, int> MADE_OF = new Dictionary<Food, int>{ { new Meat (), 10 } };
	private static Food[] DIET = new Food[]{ new Acorn(), new Bugs() };

	public override string Species { get { return "Bird"; } }

	public override Dictionary<Food, int> MadeOf { get { return MADE_OF; } }

	public override int MaxHealth { get { return 80; } }

	public override float MinTemp { get { return 50.0f; } }

	public override float MaxTemp { get { return 90.0f; } }

	public override float TempDelta { get { return 4.0f; } }

	public override Food[] Diet { get { return DIET; } }

	public override int MaxFullness { get { return 20; } }

	public override int Metabolism  { get { return 3; } }

	public override int HungerThreshold  { get { return 10; } }

	public override int HealRate { get { return 5; } }
}
