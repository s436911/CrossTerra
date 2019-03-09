using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Database {
	public static List<Biome> biomeBase;
	public static List<Building> buildingBase;
	public static List<Cterrain> cterrianBase;
	public static Dictionary<int, Species> speciesBase;
	public static Dictionary<int, WeaponData> weaponDataBase;

	public static void LoadDatabase() {
		Biome[] biomeLoader = Resources.LoadAll<Biome>(PathSetting.biomesPath);
		biomeBase = new List<Biome>(biomeLoader);

		Cterrain[] CterrianLoader = Resources.LoadAll<Cterrain>(PathSetting.cTerrainsPath);
		cterrianBase = new List<Cterrain>(CterrianLoader);

		Building[] buildingLoader = Resources.LoadAll<Building>(PathSetting.buildingsPath);
		buildingBase = new List<Building>(buildingLoader);

		Species[] speciesLoader = Resources.LoadAll<Species>(PathSetting.speciesPath);
		speciesBase = new Dictionary<int, Species>();
		foreach (Species caseSpecies in speciesLoader) {
			speciesBase.Add(caseSpecies.sID, caseSpecies);
		}

		WeaponData[] weaponDatasLoader = Resources.LoadAll<WeaponData>(PathSetting.weaponsPath);
		weaponDataBase = new Dictionary<int, WeaponData>();
		foreach (WeaponData caseWeaponData in weaponDatasLoader) {
			weaponDataBase.Add(caseWeaponData.sID, caseWeaponData);
		}
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
