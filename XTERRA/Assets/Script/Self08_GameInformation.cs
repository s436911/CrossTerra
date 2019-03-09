using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO; 
using System.Collections.Generic;
using System;

public class Self08_GameInformation : MonoBehaviour {
	//----------------------------通用-----------------------------------

	//NONE SAVE
	public static GameObject engine;
	public static bool FirstTime = false;


	public static UI_SetText Print;//訊息用
	public static string NextScene;//切換畫面用
	public static bool IsPause = true;//暫停狀態

	//--------------------------Save---------------------------------
	public static bool isEnglish = true;
	public static int[] recode = new int[10];
	public static string[] recodeN = new string[10];
	public static float gameVolume = 30;

	//--------------------------任務載入------------------------------
	public static int stageID;//關卡ID
	public static string stageName;//關卡ID
	public static int AIBonus;//AI加血

	public static int[] enemyMark = new int[2];//敵軍隊徽
	public static Texture MisType = null;//任務類型圖示

	//----------------------遊戲設置-------------------------------


	void Awake(){
		engine = gameObject;
	}

	void Start(){
		recodeN[0] = "- - -";
		recodeN[1] = "- - -";
		recodeN[2] = "- - -";
		recodeN[3] = "- - -";
		recodeN[4] = "- - -";
		recodeN[5] = "- - -";
		recodeN[6] = "- - -";
		recodeN[7] = "- - -";
		recodeN[8] = "- - -";
		recodeN[9] = "- - -";


		//save1 CID 
		//save2 NID LV EXP SLV LAN MUS MK1 MK2 
		//save1 PNG 

		Self10_IO.PreLoadFile();
	}

	public static bool loader(){//讀取步驟
		FileInfo file = new FileInfo(Application.persistentDataPath + "//" + Self10_IO.fname); 
		if (!file.Exists) {//如果初次讀取創建.
			FirstTime = true;
			
			Self10_IO.SaveFile();

			return true;
		}else {
			return Self10_IO.LoadFile();
		}
	}
}
