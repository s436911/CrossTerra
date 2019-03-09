using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
//using UnityEditor;

public class Main_GameSetting : MonoBehaviour {

	//--------------Common-------------------

	public bool debugMode = true;

	public static float ScaleOff;//倍率
	public static bool debug;

	//--------------Game---------------------


	void Awake (){
		debug = debugMode;
	}

	void Start (){
		ScaleOff = GameObject.Find ("Canvas").transform.localScale.x;
	}

}
