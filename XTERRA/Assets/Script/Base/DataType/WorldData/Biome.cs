using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Biome : BaseType {
    public Color  color = Color.white;
	public Sprite texture;
	public List<Building> buildings = new List<Building>();	
	public List<Cterrain> cterrians = new List<Cterrain>();

	public bool land = true;
	public short minSize = 0;
	public short tempShift = 0;
    public short maxBuilding = 0;
	public short height = 0;
	public short supCost = 0;
	
    public Biome Mirror(Biome value) {
		base.Mirror(value);
		this.tempShift = value.tempShift;
		this.color = value.color;
		this.buildings = value.buildings;
		this.minSize = value.minSize;
		this.land = value.land;
		this.height = value.height;
		this.cterrians = value.cterrians;
		this.maxBuilding = value.maxBuilding;
		this.texture = value.texture;

		return this;
    }

	public override void ImportReset() {
		buildings = new List<Building>();
		cterrians = new List<Cterrain>();
	}

	public Cterrain GetCterrian() {
		if (cterrians.Count > 0) {
			return cterrians[Random.Range(0, cterrians.Count)];
		}
		return null;
	}

	public Building GetBuilding() {
		/*
		if (buildings.Count > 0) {
            foreach (Building obj in buildings) {
				if (obj.buildWeights / GetBuildingsWeights() > Random.Range(0, 1.0f)) {
					return obj;
                }
            }
        }
		Debug.LogError("#Err:GetBuilding");*/
		return null;         
    }

   

    public float GetBuildingsWeights() {    //回傳總權重
        float caculator = 0;
        foreach (Building obj in buildings) {
            caculator += obj.buildWeights;
        }
        return caculator;
    }
}
