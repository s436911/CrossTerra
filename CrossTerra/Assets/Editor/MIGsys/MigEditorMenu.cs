using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class MigEditorMenu : EditorWindow {
	/*
    private static void FileCheck() {
        string infoName;
        string assetPath = "Assets/Resources/Datas/";

        if (!Directory.Exists(assetPath)) {
            Directory.CreateDirectory(assetPath);
        }
        
        infoName = "regionsData";
        if(!File.Exists(assetPath + infoName + ".asset")) {

			RegionsData regionInfo = CreateInstance<RegionsData>();
            AssetDatabase.CreateAsset(regionInfo, assetPath + infoName + ".asset");
        }
    }*/
	
    [MenuItem("MigEditor/EditFunc/DataEditer", false, 0)]
    static void DataEditer() {
        GetWindow(typeof(RegionEditor));
    }

	[MenuItem("MigEditor/EditFunc/DataImporter", false, 0)]
	static void DataImporter() {
		GetWindow(typeof(DatabaseImporter));
	}
}