using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;

public class RegionEditor : EditorBase {
    private List<Biome> biomes;
	private List<Cterrain> cterrians;
	private List<Building> buildings;
	private sbyte editTab = 0;
    
    void OnGUI() {

		BeginV();
		{
			BeginH();
			{
				EditTabSwitch();
			}
			EndH();

			BeginV("Box");
			{
				if (editTab == 0) {
					BiomeEdit();

				} else if (editTab == 1) {
					CTerrainEdit();

				} else if (editTab == 2) {
					BuildingEdit();

				} else {

				}
			}
			EndV();
		}
		EndV();

        GUILayout.Label("");
        /*
        BeginH();
        {
            if (GUILayout.Button("Save")) {
                biomesData.SetDirty();
            }
        }
        EndH();
        */
    }

	private void EditTabSwitch() {
		if (GUILayout.Button("Biome", GUILayout.Width(120))) {
			editTab = 0;
			GUI.FocusControl("");
		}
		if (GUILayout.Button("CTerrain", GUILayout.Width(120))) {
			editTab = 1;
			GUI.FocusControl("");
		}
		if (GUILayout.Button("Building", GUILayout.Width(120))) {
			editTab = 2;
			GUI.FocusControl("");
		}
	}

	private void CTerrainEdit() {
		BeginH();
		{
			/*
            if (GUILayout.Button("新增區域")) {
               
            }
			if (GUILayout.Button("讀取區域")) {
				
			}*/
		}
		EndH();

		BeginH();
		{
			GUILayout.Label("BID", GUILayout.Width(50));
			GUILayout.Label("名稱", GUILayout.Width(80));
			GUILayout.Label("補給消耗", GUILayout.Width(60));
			GUILayout.Label("圖片", GUILayout.Width(80));
		}
		EndH();

		for (int id = 0; id < cterrians.Count; id++) {
			BeginH();
			{

				EditorTools.TextField(cterrians[id].sID.ToString(), 50);
				cterrians[id].sName = EditorGUILayout.TextField(cterrians[id].sName, GUILayout.Width(80));
				cterrians[id].supCost = (short)Mathf.Clamp(EditorTools.IntField(cterrians[id].supCost, 60), 0, 10);
				cterrians[id].texture = (Sprite)EditorGUILayout.ObjectField(cterrians[id].texture , typeof(Sprite), false, GUILayout.Width(60), GUILayout.Height(60));
			}
			EndH();
		}
	}

	private void BuildingEdit() {
        BeginH();
        {
			/*
            if (GUILayout.Button("新增區域")) {
               
            }
			if (GUILayout.Button("讀取區域")) {
				
			}*/
		}
		EndH();
        
        BeginH();
        {
			GUILayout.Label("BID", GUILayout.Width(50));
			GUILayout.Label("名稱", GUILayout.Width(80));
            GUILayout.Label("建築權重", GUILayout.Width(60));
            GUILayout.Label("移除率", GUILayout.Width(60));
            GUILayout.Label("稀有度", GUILayout.Width(60));
			GUILayout.Label("圖片", GUILayout.Width(80));
		}
        EndH();

        for (int id = 0; id < buildings.Count; id++) {
            BeginH();
            {

				EditorTools.TextField(buildings[id].sID.ToString(), 50);
				buildings[id].sName = EditorGUILayout.TextField(buildings[id].sName, GUILayout.Width(80));
				buildings[id].buildWeights = EditorTools.FloatField(buildings[id].buildWeights, 60);
				buildings[id].removeChance = EditorTools.FloatField(buildings[id].removeChance, 60);
				buildings[id].rarity = (short)EditorTools.IntField(buildings[id].rarity, 60);
				buildings[id].texture = (Sprite)EditorGUILayout.ObjectField(buildings[id].texture, typeof(Sprite), false, GUILayout.Width(60), GUILayout.Height(60));
			}
            EndH();
        }
    }

    private void BiomeEdit() {
        BeginH();
        {
			/*
            if (GUILayout.Button("新增區域")) {
               
            }
			if (GUILayout.Button("讀取區域")) {
				
			}*/
		}
        EndH();

        BeginH();
        {
			GUILayout.Label("RID", GUILayout.Width(50));
			GUILayout.Label("名稱", GUILayout.Width(80));
			GUILayout.Label("陸塊", GUILayout.Width(45));
			GUILayout.Label("最小規模", GUILayout.Width(45));
			GUILayout.Label("溫度修正", GUILayout.Width(45));
			GUILayout.Label("補給消耗", GUILayout.Width(45));
			GUILayout.Label("最大建築", GUILayout.Width(45));
			GUILayout.Label("高度", GUILayout.Width(45));
			GUILayout.Label("顏色", GUILayout.Width(60));
        }
        EndH();

        for (int id = 0; id < biomes.Count; id++) {
            BeginH();
            {
				EditorTools.TextField(biomes[id].sID.ToString() , 50);
				EditorGUILayout.TextField(biomes[id].sName, GUILayout.Width(80));
				EditorGUILayout.TextField(biomes[id].land.ToString(), GUILayout.Width(45));
				EditorTools.IntField(biomes[id].minSize, 45);
				EditorTools.IntField(biomes[id].tempShift, 45);
				EditorTools.IntField(biomes[id].supCost, 45);
				EditorTools.IntField(biomes[id].maxBuilding, 45);
				EditorTools.IntField(biomes[id].height, 45);
				biomes[id].color = EditorGUILayout.ColorField(biomes[id].color, GUILayout.Width(60));
			}
            EndH();
        }
    }

    protected override void OnInit() {
		Biome[] biomeLoader = Resources.LoadAll<Biome>(PathSetting.biomesPath);
		biomes = new List<Biome>(biomeLoader);

		Cterrain[] cterrianLoader = Resources.LoadAll<Cterrain>(PathSetting.cTerrainsPath);
		cterrians = new List<Cterrain>(cterrianLoader);

		Building[] buildingLoader = Resources.LoadAll<Building>(PathSetting.buildingsPath); 
		buildings = new List<Building>(buildingLoader);
	}


	protected override void OnEnd() {		
		foreach (Biome data in biomes) {
			EditorUtility.SetDirty(data);
		}

		foreach (Cterrain data in cterrians) {
			EditorUtility.SetDirty(data);
		}

		foreach (Building data in buildings) {
			EditorUtility.SetDirty(data);
		}
	}
}
