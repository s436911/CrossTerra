using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block {
    public Vector2 location;
    public Cterrain cterrain;    
    public Building building = null;
	protected Area area;
	protected int cell = 0;

	public float buildChance = 8;
			
	public Block(Vector2 location ) {
		this.location = location;
		float seedA = 2.1f;
		float seedB = 1.1f;

		

		//float temp = Perlin.Fbm(location.x * Main_MapManager.genInput, location.y * Main_MapManager.genInput, seedA, 1) > 0 ? 1 : 0;
		//float temp2 = Perlin.Fbm(location.x * Main_MapManager.genInput, location.y * Main_MapManager.genInput, seedB, 1) > 0 ? 1 : 0;

		float temp = Mathf.PerlinNoise(location.x * Main_MapManager.genInput, location.y * Main_MapManager.genInput) > 0.5f ? 1 : 0;
		location += new Vector2(150, 150);

		float temp2 = Mathf.PerlinNoise(location.x * Main_MapManager.genInput, location.y * Main_MapManager.genInput) > 0.5f ? 1 : 0;

		if (temp == 0) {
			SetCell(temp2 == 0 ? 0 : 1);
		} else {
			SetCell(temp2 == 0 ? 2 : 3);
		}

		/*
		float temp = Perlin.Fbm(location.x * Main_MapManager.genInput, location.y * Main_MapManager.genInput, seedA, 1) ;
		if (temp > 0) {
			SetCell(temp > 0.135F ? 0 : 1);
		} else {
			SetCell(temp < -0.135F ? 2 : 3);
		}*/
	}
	
    public void Build(Building building) {
        if(this.building == null) {
            area.nowBuilding++;
        }
        this.building = building;        
    }

    public void ReBuild() {      //重新建築
        if (this.building != null && this.building.removeChance > Random.Range(0, 100f)) {
            this.building = null;
        }else if(BuildAble()) {
            this.building = area.biome.GetBuilding();
        }
    }

    public bool BuildAble() {   //該地基可否建造
		return (this.building == null && 
            area.biome.buildings.Count > 0  && 
            area.nowBuilding < area.biome.maxBuilding &&
            buildChance * area.BuildChance() > Random.Range(0, 100f));
    }

	public int GetSupCost() {
		return area.biome.supCost + cterrain.supCost;
	}

	public int GetHeight() {
		if (cterrain) {
			return area.biome.height + cterrain.height;
		}
		return area.biome.height;
	}

	public void SetArea(Area valueArea) {
		area = valueArea;

		this.cterrain = area.biome.GetCterrian();

		if (BuildAble()) {
			this.building = area.biome.GetBuilding();
			area.nowBuilding++;
		} 
	}


	public void SetCell(int valueCell) {
		cell = valueCell;
	}

	public int GetCell() {
		return cell;
	}

	public Area GetArea() {
		return area;
	}
}
