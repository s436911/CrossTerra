using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area {
	public int sID;
	public string sName;
	public short nowBuilding = 0;	
	public Biome biome;
	public List<Block> blocks = new List<Block>();

	public Area(Biome biome) {
		this.biome = biome;
		sName = biome.sName + Random.Range(0, 999);
	}

	public float BuildChance() {
		return (float)(biome.maxBuilding - nowBuilding) / (float)biome.maxBuilding;
	}

	public void AddArea(Area addArea) {
		foreach (Block block in addArea.blocks) {
			blocks.Add(block);
			block.SetArea(this);
		}
		addArea = null;
	}
}
