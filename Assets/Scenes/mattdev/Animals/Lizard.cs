using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : Animal {
	private const Dictionary<Food, int> MADE_OF = new Dictionary<Food, int>{ { new Meat (), 1 } };
	private const Dictionary<Food, int> DIET = new HashSet<Food> (new []{ new Bugs() });

	public override string Species { get { return "Lizard"; } }

	public override Dictionary<Food, int> MadeOf { get { return MADE_OF; } }

	public override int MaxHealth { get { return 50; } }

	public override float MinTemp { get { return 75.0; } }

	public override float MaxTemp { get { return 90.0; } }

	public override float TempDelta { get { return 5.0; } }

	public override HashSet<Food> Diet { get { return DIET; } }

	public override int MaxFullness { get { return 10; } }

	public override int Metabolism  { get { return 2; } }

	public override int HungerThreshold  { get { return 5; } }

	public override int HealRate { get { return 10; } }
}
