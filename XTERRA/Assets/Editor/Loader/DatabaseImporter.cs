using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Excel;
using System.Data;
using System.Text.RegularExpressions;

public class DatabaseImporter : EditorBase {
	private List<string> logList = new List<string>();
	private string worldDatabase = "WorldDatabase";
	private string divineDatabase = "DivineDatabase";
	private string artifactDatabase = "ArtifactDatabase";

	private int functionIndex = 0;

	private string[] functionList = {
		"SelectService",
		"Database",
		"AssetBundle"
	};

	protected override void OnInit() {

	}

	protected override void OnEnd() {

	} 

	void OnGUI() {
		BeginV(); {
			EditorTools.TitleField("DatabaseImporter");
			BeginH(); {
				BeginV("Box", 250); {
					functionIndex = EditorTools.PopupField(functionList, functionIndex, "功能選擇");

					if (functionIndex == 0) {

					} else if (functionIndex == 1) {
						ImportInterface();

					} else if (functionIndex == 2) {
						//AssetBundleInterface();

					}
				} EndV();

				if (functionIndex == 0) {
					BeginV("Box"); {
						EditorTools.LabelField("SelectService");
						EditorTools.LabelField("Database	：匯入Excel或圖片的資料");
						EditorTools.LabelField("AssetBundle	：設定資源路徑或打包");
					} EndV();

				} else if (functionIndex == 1) {

				} else if (functionIndex == 2) {

				}

			} EndH();
			EditorTools.Space();
			EditorTools.Mig();
		}
		EndV();
	}
	/*
	private void AssetBundleInterface() {
		EditorTools.LabelField("Set");
		if (GUILayout.Button("設定全部檔案")) {
			SetAssetBundleFileSeqence(PathSetting.EntityDBAssetPath, "EntityData", "EntityDataBase");
			SetAssetBundleFileSeqence(PathSetting.ImageDBAssetPath, "ImageData", "ImageDatabase");
			SetAssetBundleFileSeqence(PathSetting.AudiosDBAssetPath, "Audios", "Audios");

			SetAssetBundleDirectorySeqence(PathSetting.EntityMapsDBAssetPath, "EntityMaps", "EntityMaps");
			SetAssetBundleDirectorySeqence(PathSetting.GalleryDBAssetPath, "Gallery", "Gallery");
			SetAssetBundleDirectorySeqence(PathSetting.ComicsDBAssetPath, "Comics", "Comics");
		}

		if (GUILayout.Button("設定EntityDataBase")) {
			SetAssetBundleFileSeqence(PathSetting.EntityDBAssetPath, "EntityData", "EntityDataBase");
		}

		if (GUILayout.Button("設定ImageDatabase")) {
			SetAssetBundleFileSeqence(PathSetting.ImageDBAssetPath, "ImageData", "ImageDatabase");			
		}

		if (GUILayout.Button("設定Audios")) {
			SetAssetBundleFileSeqence(PathSetting.AudiosDBAssetPath, "Audios", "Audios");
		}

		if (GUILayout.Button("設定EntityMaps")) {
			SetAssetBundleDirectorySeqence(PathSetting.EntityMapsDBAssetPath, "EntityMaps", "EntityMaps");
		}

		if (GUILayout.Button("設定Gallery")) {
			SetAssetBundleDirectorySeqence(PathSetting.GalleryDBAssetPath, "Gallery", "Gallery");			
		}

		if (GUILayout.Button("設定Comics")) {
			SetAssetBundleDirectorySeqence(PathSetting.ComicsDBAssetPath, "Comics", "Comics");
			
		}
		EditorTools.LabelField("Build");
	}
	*/

	private void ImportInterface() {
		EditorTools.LabelField("Database - " + worldDatabase);

		if (GUILayout.Button("匯入整個 " + worldDatabase)) {
			ReadXLSX<Cterrain>(worldDatabase, PathSetting.dataBasePath, "CTerrainData", PathSetting.cTerrainsPath);
			ReadXLSX<Building>(worldDatabase, PathSetting.dataBasePath, "BuildingData", PathSetting.buildingsPath);
			ReadXLSX<Biome>(worldDatabase, PathSetting.dataBasePath, "BiomeData", PathSetting.biomesPath, true);
		}

		if (GUILayout.Button("匯入 BiomeDataS")) {
			ReadXLSX<Biome>(worldDatabase, PathSetting.dataBasePath, "BiomeDatas", PathSetting.biomesPath, true);
		}
		if (GUILayout.Button("匯入 CTerrainDataS")) {
			ReadXLSX<Cterrain>(worldDatabase, PathSetting.dataBasePath, "CTerrainDatas", PathSetting.cTerrainsPath);
		}
		if (GUILayout.Button("匯入 BuildingDataS")) {
			ReadXLSX<Building>(worldDatabase, PathSetting.dataBasePath, "BuildingDatas", PathSetting.buildingsPath);
		}


		EditorTools.LabelField("Database - " + divineDatabase);

		if (GUILayout.Button("匯入整個 " + divineDatabase)) {
			ReadXLSX<Species>(divineDatabase, PathSetting.dataBasePath, "SpeciesDatas", PathSetting.speciesPath);
		}
		if (GUILayout.Button("匯入 SpeciesDatas")) {
			ReadXLSX<Species>(divineDatabase, PathSetting.dataBasePath, "SpeciesDatas", PathSetting.speciesPath);
		}


		EditorTools.LabelField("Database - " + artifactDatabase);

		if (GUILayout.Button("匯入整個 " + artifactDatabase)) {
			ReadXLSX<WeaponData>(artifactDatabase, PathSetting.dataBasePath, "WeaponDatas", PathSetting.weaponsPath);
		}
		if (GUILayout.Button("匯入 WeaponDatas")) {
			ReadXLSX<WeaponData>(artifactDatabase, PathSetting.dataBasePath, "WeaponDatas", PathSetting.weaponsPath);
		}


		/*			
		EditorTools.LabelField("Image");
		if (GUILayout.Button("匯入Gallery")) {
			//開始讀取圖片資訊			
			if (!Directory.Exists(PathSetting.GalleryDBAssetPath)) {
				Directory.CreateDirectory(PathSetting.GalleryDBAssetPath);
			}

			string[] directorys = Directory.GetDirectories(PathSetting.GalleryDBAssetPath);

			foreach (string directory in directorys) {
				string[] files = Directory.GetFiles(directory);

				foreach (string file in files) {
					try {
						TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(file);
						if (importer != null) {
							importer.wrapMode = TextureWrapMode.Clamp;
							importer.textureType = TextureImporterType.Sprite;
							importer.mipmapEnabled = false;
							importer.maxTextureSize = 2048;
							//importer.textureFormat = TextureImporterFormat.PVRTC_RGB4;
							importer.SaveAndReimport();
						}
					} catch {
						Debug.Log("錯誤的圖片類型:" + file);
					}
				}
			}
		}*/
	}
	/*
	private void SetAssetBundleFileSeqence(string path , string assetBundle , string debug ) {
		try {
			foreach (string file in Directory.GetFiles(path)) {
				if (Path.GetExtension(file) != ".meta") {
					string assetPath = file.Substring(file.IndexOf("Assets"));
					AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(assetBundle + "/" + Path.GetFileNameWithoutExtension(assetPath), "");
				}
			}
			Debug.Log("#設定AssetBundle：" + debug +"設定成功");
		} catch {
			Debug.Log("#設定AssetBundle：" + debug + "設定失敗");
		}		
	}

	private void SetAssetBundleDirectorySeqence(string path, string assetBundle, string debug) {
		try {
			string[] subPath = Directory.GetDirectories(path);
			foreach (string filePath in subPath) {
				foreach (string file in Directory.GetFiles(filePath)) {
					if (Path.GetExtension(file) != ".meta") {
						string assetPath = file.Substring(file.IndexOf("Assets"));
						AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
						assetImporter.SetAssetBundleNameAndVariant(assetBundle + "/[" + filePath.Substring(filePath.LastIndexOf(assetBundle) + assetBundle.Length + 1) + "]" + Path.GetFileNameWithoutExtension(assetPath), "");
						if (assetBundle == "Gallery") {
							TextureImporter importer = assetImporter as TextureImporter;
							TextureImporterSettings texSettings = new TextureImporterSettings();

							importer.textureType = TextureImporterType.Sprite;
							importer.ReadTextureSettings(texSettings);
							//importer.SetPlatformTextureSettings(,"Standalone", 2048, TextureImporterFormat.);
							//importer.SetPlatformTextureSettings("iPhone", 2048, TextureImporterFormat.PVRTC_RGBA4);
							//importer.SetPlatformTextureSettings("Android", 2048, TextureImporterFormat.ETC_RGB4, 1, true);
							importer.SetTextureSettings(texSettings);
							importer.SaveAndReimport();
						}
					}
				}
			}
			Debug.Log("#設定AssetBundle：" + debug + "設定成功");
		} catch {
			Debug.Log("#設定AssetBundle：" + debug + "設定失敗");
		}
	}*/

	private void ReadXLSX<T>(string xmlName, string dataBasePath, string tabName, string dataPath, bool shiftMode = false) where T : BaseType {
		logList = new List<string>();
		ExcelBase xls = new ExcelBase();

		try {
			xls = ExcelHelper.LoadExcel(dataBasePath + xmlName + ".xlsx");
		} catch {
			Debug.LogError("#DatabaseImporter : ReadXLSX Failed : 請確認 Excel 是否關閉...");
		}

		for (int i = 0; i < xls.Tables.Count; i++) {
			if (xls.Tables[i].TableName == tabName) {
				Debug.Log("@DatabaseImporter : ReadXLSX Success : " + xls.Tables[i].TableName + "...");

				if (!Directory.Exists(PathSetting.resourcesPath + dataPath)) {
					Directory.CreateDirectory(PathSetting.resourcesPath + dataPath);
				}
				
				for (int row = 3; row <= xls.Tables[i].NumberOfRows; row++) {
					if (shiftMode && row % 2 == 0) {
						continue;
					}

					T data = CreateInstance<T>();
					int id;
					bool newMode = true;
					
					for (int column = 1; column <= xls.Tables[i].NumberOfColumns; column++) {
						string value = (xls.Tables[i].GetValue(row, column)).ToString();

						//必須有ID
						if (column == 1 && string.IsNullOrEmpty(value.Trim())) {	
							break;

						} else {

							//檢查是否有檔案 決定新增或是複寫
							if (column == 1) {
								int.TryParse(value, out id);
								data = Resources.Load<T>(dataPath + id.ToString("0000"));
								if (data == null) {
									data = CreateInstance<T>();
								} else {
									newMode = false;
									data.ImportReset();
								}
							}
							
							//錯誤時中止執行
							if (data.GetType() == typeof(Biome)) {
								if (!BiomeSpawner(data as Biome, column, value.Trim())) {
									logList.Add("#匯入 Biome 資料第" + row + "行，第" + column + "列格式有誤");
									break;
								}
							} else if (data.GetType() == typeof(Cterrain)) {
								if (!CTerrainSpawner(data as Cterrain, column, value.Trim())) {
									logList.Add("#匯入 Cterrain 資料第" + row + "行，第" + column + "列格式有誤");
									break;
								}
							} else if (data.GetType() == typeof(Building)) {
								if (!BuildingSpawner(data as Building, column, value.Trim())) {
									logList.Add("#匯入 Building 資料第" + row + "行，第" + column + "列格式有誤");
									break;
								}
							} else if (data.GetType() == typeof(Species)) {
								if (!SpeciesSpawner(data as Species, column, value.Trim())) {
									logList.Add("#匯入 Species 資料第" + row + "行，第" + column + "列格式有誤");
									break;
								}
							} else if (data.GetType() == typeof(WeaponData)) {
								if (!WeaponsSpawner(data as WeaponData, column, value.Trim())) {
									logList.Add("#匯入 WeaponDatas 資料第" + row + "行，第" + column + "列格式有誤");
									break;
								}
							}

							//新增或複寫
							if (column == xls.Tables[i].NumberOfColumns) {
								if (newMode) {
									AssetDatabase.CreateAsset(data, PathSetting.resourcesPath + dataPath + data.sID.ToString("0000") + ".asset");
								} else {
									EditorUtility.SetDirty(data);
								}
							}
						}
					}
				}
				break;
			} 
			/* else if (xls.Tables[i].TableName == "GalleryDatabase" && (importHandler == 0 || importHandler == 2)) {
				Debug.Log("@DatabaseImporter : ReadXLSX Success : " + xls.Tables[i].TableName + "...");
				for (int row = 3; row <= xls.Tables[i].NumberOfRows; row++) {
					ImageData image = ScriptableObject.CreateInstance<ImageData>();

					for (int column = 1; column <= xls.Tables[i].NumberOfColumns; column++) {
						string value = (xls.Tables[i].GetValue(row, column)).ToString();
						if (column == 1 && string.IsNullOrEmpty(value)) {
							break;

						} else if (!ImageSpawner(image, column, value)) {
							logList.Add("#匯入資料第" + row + "行，第" + column + "列格式有誤");
							break;
						}
					}
				}

			} else if (xls.Tables[i].TableName == "LangDatabase" && (importHandler == 0 || importHandler == 3)) {
				Debug.Log("@DatabaseImporter : ReadXLSX Success : " + xls.Tables[i].TableName + "...");
				LangData lang = ScriptableObject.CreateInstance<LangData>();
				for (int row = 3; row <= xls.Tables[i].NumberOfRows; row++) {		
					for (int column = 1; column <= xls.Tables[i].NumberOfColumns; column++) {
						string value = (xls.Tables[i].GetValue(row, column)).ToString();
						if (column == 1 && string.IsNullOrEmpty(value)) {
							break;

						} else if (!LangSpawner(lang, column, value)) {
							logList.Add("#匯入資料第" + row + "行，第" + column + "列格式有誤");
							break;
						}
					}
				}
				AssetDatabase.CreateAsset(lang, PathSetting.LangDBAssetPath + "LangData.asset");
			}*/
		}
		LogImportInfo();
	}
	
	private bool BiomeSpawner(Biome valueData , int valueIndex , string value) {
		bool returnValue = true;
		if (valueIndex == 1) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			int.TryParse(value, out valueData.sID);

		} else if (valueIndex == 2) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			valueData.sName = value;

		} else if (valueIndex == 3) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			valueData.land = value == "T";

		} else if (valueIndex == 4) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			short.TryParse(value, out valueData.minSize);

		} else if (valueIndex == 5) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			short.TryParse(value, out valueData.tempShift);

		} else if (valueIndex == 6) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			short.TryParse(value, out valueData.supCost);

		} else if (valueIndex == 7) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			short.TryParse(value, out valueData.maxBuilding);

		} else if (valueIndex == 8) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			short.TryParse(value, out valueData.height);
			valueData.height -= 10;

		} else if (valueIndex >= 9 && valueIndex <= 28) {
			if (!string.IsNullOrEmpty(value)) {
				if (!string.IsNullOrEmpty(value)) {
					int idLoader = 0;
					int.TryParse(value, out idLoader);
					Cterrain dataLoader = Resources.Load<Cterrain>(PathSetting.cTerrainsPath + idLoader.ToString("0000"));

					if (dataLoader != null) {
						valueData.cterrians.Add(dataLoader);
					}
				}
			}

		} else if (valueIndex >= 29 && valueIndex <= 48) {
			if (!string.IsNullOrEmpty(value)) {
				int idLoader = 0;
				int.TryParse(value, out idLoader);
				Building dataLoader = Resources.Load<Building>(PathSetting.buildingsPath + idLoader.ToString("0000"));
				
				if (dataLoader != null) {
					valueData.buildings.Add(dataLoader);
				}
			}
		}
		return returnValue;
	}

	private bool BuildingSpawner(Building valueData, int valueIndex, string value) {
		bool returnValue = true;
		if (valueIndex == 1) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			int.TryParse(value, out valueData.sID);

		} else if (valueIndex == 2) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			valueData.sName = value;

		} else if (valueIndex == 3) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			float.TryParse(value, out valueData.buildWeights);

		} else if (valueIndex == 4) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			float.TryParse(value, out valueData.removeChance);

		} else if (valueIndex == 5) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			short.TryParse(value, out valueData.rarity);

		}

		return returnValue;
	}

	private bool CTerrainSpawner(Cterrain valueData, int valueIndex, string value) {
		bool returnValue = true;
		if (valueIndex == 1) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			int.TryParse(value, out valueData.sID);

		} else if (valueIndex == 2) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			valueData.sName = value;

		} else if (valueIndex == 3) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			short.TryParse(value, out valueData.supCost);

		} else if (valueIndex == 4) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			short.TryParse(value, out valueData.height);

		}

		return returnValue;
	}

	private bool SpeciesSpawner(Species valueData, int valueIndex, string value) {
		bool returnValue = true;
		int index = 1;

		if (valueIndex == index++) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			int.TryParse(value, out valueData.sID);

		} else if (valueIndex == index++) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			valueData.sName = value;

		} else if (valueIndex == index++) {
			

		} else if (valueIndex == index++) {
			

		} else if (valueIndex == index++) { // 5
			

		} else if (valueIndex == index++) {
			

		} else if (valueIndex == index++) {
			

		} else if (valueIndex == index++) {
			float.TryParse(value, out valueData.atkCT);

		} else if (valueIndex == index++) {
			float.TryParse(value, out valueData.matkCT);

		} else if (valueIndex == index++) { // 10
			float.TryParse(value, out valueData.arbkCT);

		} else if (valueIndex == index++) {
			float.TryParse(value, out valueData.armorCT);

		} else if (valueIndex == index++) {
			float.TryParse(value, out valueData.hpCT);

		} else if (valueIndex == index++) {
			float.TryParse(value, out valueData.resistCR);

		} else if (valueIndex == index++) {
			if (!string.IsNullOrEmpty(value.Trim())) {
				float.TryParse(value, out valueData.accCR);
			} else {
				valueData.accCR = 100;
			}

		} else if (valueIndex == index++) { //15
			if (!string.IsNullOrEmpty(value.Trim())) {
				float.TryParse(value, out valueData.avdCR);
			} else {
				valueData.avdCR = 100;
			}

		} else if (valueIndex == index++) {
			if (!string.IsNullOrEmpty(value.Trim())) {
				float.TryParse(value, out valueData.aspdRT);
			} else {
				valueData.aspdRT = 0;
			}

		} else if (valueIndex == index++) {
			if (!string.IsNullOrEmpty(value.Trim())) {
				float.TryParse(value, out valueData.spdRT);
			} else {
				valueData.spdRT = 0;
			}

		} else if (valueIndex == index++) {
			valueData.model = Regex.Replace(value, @"\s", "");

		} else if (valueIndex == index++) {
			valueData.skin = Regex.Replace(value, @"\s", "");

		} else if (valueIndex == index++) { //20
			returnValue = !string.IsNullOrEmpty(value.Trim());
			float.TryParse(value, out valueData.size);

		} else if (valueIndex == index++) { //21
			ConstSkillAdder(valueData, value);

		} else if (valueIndex == index++) { //22
			ConstSkillAdder(valueData, value);

		}

		return returnValue;
	}

	private void ConstSkillAdder(Species InData , string InValue) {
		if (string.IsNullOrEmpty(InValue)) {
			return;
		}

		int caseTemper = 0;
		int.TryParse(InValue, out caseTemper);
			   
		if (caseTemper > 0) {
			InData.cSkills.Add(caseTemper);
		}
	}

	private bool WeaponsSpawner(WeaponData valueData, int valueIndex, string value) {
		bool returnValue = true;
		if (valueIndex == 1) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			int.TryParse(value, out valueData.sID);

		} else if (valueIndex == 2) {
			returnValue = !string.IsNullOrEmpty(value.Trim());
			valueData.sName = value;

		} else if (valueIndex == 3) {
			valueData.SetWeaponType(value.Trim());
			if (valueData.weaponType == WeaponType.Error) {
				returnValue = false;
			}

		} else if (valueIndex == 4) {
			float.TryParse(value, out valueData.atkCT);

		} else if (valueIndex == 5) {
			float.TryParse(value, out valueData.matkCT);

		} else if (valueIndex == 6) {
			float.TryParse(value, out valueData.arbkCT);

		} else if (valueIndex == 7) {
			float.TryParse(value, out valueData.armorCT);

		} else if (valueIndex == 8) {
			float.TryParse(value, out valueData.hpCT);

		} else if (valueIndex == 9) {
			float.TryParse(value, out valueData.resistCR);

		} else if (valueIndex == 10) {
			float.TryParse(value, out valueData.accCR);

		} else if (valueIndex == 11) {
			float.TryParse(value, out valueData.avdCR);

		} else if (valueIndex == 12) {
			if (!string.IsNullOrEmpty(value.Trim())) {
				float.TryParse(value, out valueData.aspdRT);
			} else {
				valueData.aspdRT = 0;
			}

		} else if (valueIndex == 13) {
			if (!string.IsNullOrEmpty(value.Trim())) {
				float.TryParse(value, out valueData.spdRT);
			} else {
				valueData.spdRT = 0;
			}

		} else if (valueIndex == 14) {
			valueData.skin = Regex.Replace(value, @"\s", "");

		}

		return returnValue;
	}

	private void LogImportInfo() {
		foreach (string log in logList) {
			Debug.Log(log);
		}
	}	

	/*void ReadXLSX(string DataBasePath) {
		int EID = 0;
		FileStream stream = File.Open(DataBasePath + ".xlsx", FileMode.Open, FileAccess.Read);
		IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
		do {
			// sheet name
			Debug.Log("Table:" + excelReader.Name);
			while (excelReader.Read()) {
				string newLine = "";
				for (int i = 0; i < excelReader.FieldCount; i++) {
					string value = excelReader.IsDBNull(i) ? "" : excelReader.GetString(i);
					newLine += "[" + value + "]";
				}
				Debug.Log(newLine);
			}
		} while (excelReader.NextResult());
	}*/
}
