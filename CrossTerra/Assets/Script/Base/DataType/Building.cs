using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : BaseType {
	public short  rarity;
	public float  buildWeights;
	public float  removeChance;
	public Sprite texture;

	public Building() {
	}

	public Building(Building value) {
		Mirror(value);
		this.buildWeights = value.buildWeights;
		this.removeChance = value.removeChance;
		this.rarity = value.rarity;
		this.texture = value.texture;
	}
}
