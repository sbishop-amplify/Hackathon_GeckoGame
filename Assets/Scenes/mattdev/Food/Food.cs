using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Food : MonoBehaviour {

	public abstract string Name { get; }

	// How much the food fills us per unit
	public abstract int Satiation { get; }

	public override bool Equals (object obj) {
		return (obj is Food) && Name.Equals((obj as Food).Name);
	}

	public override string ToString () {
		return Name;
	}

	public override int GetHashCode() {
		return Name.GetHashCode ();
	}
}
