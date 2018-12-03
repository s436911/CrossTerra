using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Database {
	public static List<Biome> biomeBase;
	public static List<Building> buildingBase;
	public static List<Cterrain> cterrianBase;

	public static void LoadDatabase() {
		Biome[] biomeLoader = Resources.LoadAll<Biome>(PathSetting.biomesRePath);
		biomeBase = new List<Biome>(biomeLoader);

		Cterrain[] CterrianLoader = Resources.LoadAll<Cterrain>(PathSetting.cTerrainsRePath);
		cterrianBase = new List<Cterrain>(CterrianLoader);

		Building[] buildingLoader = Resources.LoadAll<Building>(PathSetting.buildingsRePath);
		buildingBase = new List<Building>(buildingLoader);
	}

	public static Biome GenBiome(int areaSize) {
		int num = 0;
		while (num == 0) {
			num = Random.Range(1, biomeBase.Count);
			if (biomeBase[num].minSize > areaSize) {
				num = 0;
			}
		}
		return biomeBase[num];
	}
}
