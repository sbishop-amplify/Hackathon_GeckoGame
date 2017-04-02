using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Animal {
	private const Dictionary<Food, int> MADE_OF = new Dictionary<Food, int>{ { new Meat (), 100 } };
	private const Dictionary<Food, int> DIET = new HashSet<Food> (new []{ new Meat(), new Berry(), new Fruit() });

	public override string Species { get { return "Bear"; } }

	public override Dictionary<Food, int> MadeOf { get { return MADE_OF; } }

	public override int MaxHealth { get { return 500; } }

	public override float MinTemp { get { return 20.0; } }

	public override float MaxTemp { get { return 80.0; } }

	public override float TempDelta { get { return 2.0; } }

	public override HashSet<Food> Diet { get { return DIET; } }

	public override int MaxFullness { get { return 100; } }

	public override int Metabolism  { get { return 10; } }

	public override int HungerThreshold  { get { return 30; } }

	public override int HealRate { get { return 30; } }
}
