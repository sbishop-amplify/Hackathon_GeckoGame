using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : Animal {
	private const Dictionary<Food, int> MADE_OF = new Dictionary<Food, int>{ { new Meat (), 50 } };
	private const Dictionary<Food, int> DIET = new HashSet<Food> (new []{ new Leaf(), new Berry() });

	public override string Species { get { return "Deer"; } }

	public override Dictionary<Food, int> MadeOf { get { return MADE_OF; } }

	public override int MaxHealth { get { return 300; } }

	public override float MinTemp { get { return 30.0; } }

	public override float MaxTemp { get { return 75.0; } }

	public override float TempDelta { get { return 3.0; } }

	public override HashSet<Food> Diet { get { return DIET; } }

	public override int MaxFullness { get { return 50; } }

	public override int Metabolism  { get { return 5; } }

	public override int HungerThreshold  { get { return 25; } }

	public override int HealRate { get { return 20; } }
}
