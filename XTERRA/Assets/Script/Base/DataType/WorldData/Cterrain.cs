using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cterrain : BaseType {
	public short supCost = 0;
	public short height = 0;
	public Sprite texture;

	public Cterrain() {
	}

	public Cterrain(Cterrain value) {
		Mirror(value);
		this.supCost = value.supCost;
		this.height = value.height;
		this.texture = value.texture;
	}
}