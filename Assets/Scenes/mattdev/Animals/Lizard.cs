using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : Animal {
	private static Dictionary<Food, int> MADE_OF = new Dictionary<Food, int>{ { new Meat (), 2 } };
	private static Food[] DIET = new Food[]{ new Bugs() };

	public override string Species { get { return "Lizard"; } }

	public override Dictionary<Food, int> MadeOf { get { return MADE_OF; } }

	public override int MaxHealth { get { return 40; } }

	public override float MinTemp { get { return 75.0f; } }

	public override float MaxTemp { get { return 90.0f; } }

	public override float TempDelta { get { return 5.0f; } }

	public override Food[] Diet { get { return DIET; } }

	public override int MaxFullness { get { return 10; } }

	public override int Metabolism  { get { return 2; } }

	public override int HungerThreshold  { get { return 5; } }

	public override int HealRate { get { return 8; } }
}
