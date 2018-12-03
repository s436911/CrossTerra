using UnityEngine;
using System.Collections;
using System.IO; 
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

public class Self10_IO{
	//文本中每行的內容.
	public static string fname = "StudioData.mig";
	public static int lineNum = 3 + 1;

	public static void CreateFile() { 
		//文件流信息.
		StreamWriter sw; 
		FileInfo t = new FileInfo(Application.persistentDataPath + "//" + fname); 

		sw = t.CreateText();

		for(int i = 1; i < lineNum;i++){
			sw.WriteLine(saveInfo(i));
		}

		//關閉流.
		sw.Close();
		//銷毀流.
		sw.Dispose();
	}

	public static void SaveFile() { 
		//文件流信息.
		StreamWriter sw; 
		FileInfo t = new FileInfo(Application.persistentDataPath + "//" + fname); 

		File.Delete(Application.persistentDataPath + "//" + fname);
		sw = t.CreateText();
		
		for(int i = 1; i < lineNum;i++){
			sw.WriteLine(saveInfo(i));
		}
		
		//關閉流.
		sw.Close();
		//銷毀流.
		sw.Dispose(); 
	} 
		
	public static bool LoadFile() { 
		try{
			string[] tempArray;
			StreamReader sr = null;
			string temper;
			int i = 1;
			
			try{
				sr = File.OpenText(Application.persistentDataPath + "//" + fname); 
			}catch{
				//路徑與名稱未找到文件則直接回傳null.
				Debug.Log ("Load ERR");
			}
			string line;
			
			ArrayList arrlist = new ArrayList();
			
			while ((line = sr.ReadLine()) != null){
				arrlist.Add(line);
				if(i == 1 ){
					if(line != SystemInfo.deviceUniqueIdentifier){
						Debug.Log ("ID ERR");
						Application.Quit();
					}
				}else if(i == 2){
					tempArray = Regex.Split(line, "W", RegexOptions.IgnoreCase);					

					Self08_GameInformation.gameVolume = int.Parse(decode(2 ,tempArray[1]));
					Self08_GameInformation.recode[0] = int.Parse(decode(1 ,tempArray[2]));
					Self08_GameInformation.recode[1] = int.Parse(decode(1 ,tempArray[3]));
					Self08_GameInformation.recode[2] = int.Parse(decode(1 ,tempArray[4]));
					Self08_GameInformation.recode[3] = int.Parse(decode(1 ,tempArray[5]));
					Self08_GameInformation.recode[4] = int.Parse(decode(1 ,tempArray[6]));
					Self08_GameInformation.recode[5] = int.Parse(decode(1 ,tempArray[7]));
					Self08_GameInformation.recode[6] = int.Parse(decode(1 ,tempArray[8]));
					Self08_GameInformation.recode[7] = int.Parse(decode(1 ,tempArray[9]));
					Self08_GameInformation.recode[8] = int.Parse(decode(1 ,tempArray[10]));
					Self08_GameInformation.recode[9] = int.Parse(decode(1 ,tempArray[11]));
				}else if(i == 3){
					tempArray = Regex.Split(line, "@", RegexOptions.IgnoreCase);					

					Self08_GameInformation.recodeN[0] = tempArray[0];
					Self08_GameInformation.recodeN[1] = tempArray[1];
					Self08_GameInformation.recodeN[2] = tempArray[2];
					Self08_GameInformation.recodeN[3] = tempArray[3];
					Self08_GameInformation.recodeN[4] = tempArray[4];
					Self08_GameInformation.recodeN[5] = tempArray[5];
					Self08_GameInformation.recodeN[6] = tempArray[6];
					Self08_GameInformation.recodeN[7] = tempArray[7];
					Self08_GameInformation.recodeN[8] = tempArray[8];
					Self08_GameInformation.recodeN[9] = tempArray[9];
				}
				
				i++;
			}
			//關閉流.
			sr.Close();
			//銷毀流.
			sr.Dispose();
			//將數組鏈表容器返回.
			return true;
		}catch{
			return false;
		}


	}

	public static bool PreLoadFile() { 
		try{
			string[] tempArray;
			StreamReader sr = null;
			int i = 1;
			
			try{
				sr = File.OpenText(Application.persistentDataPath + "//" + fname); 
			}catch{
				//路徑與名稱未找到文件則直接回傳null.
				return false;
			}

			string line;
			
			while ((line = sr.ReadLine()) != null){
				if(i == 2){
					tempArray = Regex.Split(line, "W", RegexOptions.IgnoreCase);

					Self08_GameInformation.gameVolume = int.Parse(decode(2, tempArray[1]));	
				}
				
				i++;
			}
			//關閉流.
			sr.Close();
			//銷毀流.
			sr.Dispose();
			//將數組鏈表容器返回.
			
			return true;
		}catch{
			return false;
		}
		
		
	}

	public static void DeleteFile(){
		File.Delete(Application.persistentDataPath + "//" + fname);
	}

	public static string saveInfo(int line ){
		string reInfo = "";

		if(line == 1){
			reInfo += SystemInfo.deviceUniqueIdentifier;
		}else if(line == 2){
			reInfo += encode(0,"0");
			reInfo += "W";
			reInfo += encode(2,Self08_GameInformation.gameVolume.ToString());
			reInfo += "W";
			reInfo += encode(1,Self08_GameInformation.recode[0].ToString());
			reInfo += "W";
			reInfo += encode(1,Self08_GameInformation.recode[1].ToString());
			reInfo += "W";
			reInfo += encode(1,Self08_GameInformation.recode[2].ToString());
			reInfo += "W";
			reInfo += encode(1,Self08_GameInformation.recode[3].ToString());
			reInfo += "W";
			reInfo += encode(1,Self08_GameInformation.recode[4].ToString());
			reInfo += "W";
			reInfo += encode(1,Self08_GameInformation.recode[5].ToString());
			reInfo += "W";
			reInfo += encode(1,Self08_GameInformation.recode[6].ToString());
			reInfo += "W";
			reInfo += encode(1,Self08_GameInformation.recode[7].ToString());
			reInfo += "W";
			reInfo += encode(1,Self08_GameInformation.recode[8].ToString());
			reInfo += "W";
			reInfo += encode(1,Self08_GameInformation.recode[9].ToString());
		}else if(line == 3){
			reInfo += Self08_GameInformation.recodeN[0];
			reInfo += "@";
			reInfo += Self08_GameInformation.recodeN[1];
			reInfo += "@";
			reInfo += Self08_GameInformation.recodeN[2];
			reInfo += "@";
			reInfo += Self08_GameInformation.recodeN[3];
			reInfo += "@";
			reInfo += Self08_GameInformation.recodeN[4];
			reInfo += "@";
			reInfo += Self08_GameInformation.recodeN[5];
			reInfo += "@";
			reInfo += Self08_GameInformation.recodeN[6];
			reInfo += "@";
			reInfo += Self08_GameInformation.recodeN[7];
			reInfo += "@";
			reInfo += Self08_GameInformation.recodeN[8];
			reInfo += "@";
			reInfo += Self08_GameInformation.recodeN[9];
		}else{
			Debug.Log ("Info ERR");
			return reInfo;
		}
		return reInfo;
	}


	private static string encode(int type, string before){
		string after = ""; 

		if(before != ""){
			if(type == 0){
				for(int i = 0 ; i < before.Length;i++){
					if(before[i] == '0'){
						after += "M"; 
					}else if(before[i] == '1'){
						after += "I"; 
					}else if(before[i] == '2'){
						after += "G"; 
					}else if(before[i] == '3'){
						after += "3"; 
					}else if(before[i] == '4'){
						after += "5"; 
					}else if(before[i] == '5'){
						after += "S"; 
					}else if(before[i] == '6'){
						after += "E"; 
					}else if(before[i] == '7'){
						after += "0"; 
					}else if(before[i] == '8'){
						after += "7"; 
					}else if(before[i] == '9'){
						after += "1"; 
					}else if(before[i] == '-'){
						after += "R"; 
					}
				}
			}else if(type == 1){
				for(int i = 0 ; i < before.Length;i++){
					if(before[i] == '0'){
						after += "E"; 
					}else if(before[i] == '1'){
						after += "S"; 
					}else if(before[i] == '2'){
						after += "9"; 
					}else if(before[i] == '3'){
						after += "K"; 
					}else if(before[i] == '4'){
						after += "Y"; 
					}else if(before[i] == '5'){
						after += "D"; 
					}else if(before[i] == '6'){
						after += "R"; 
					}else if(before[i] == '7'){
						after += "A"; 
					}else if(before[i] == '8'){
						after += "X"; 
					}else if(before[i] == '9'){
						after += "1"; 
					}else if(before[i] == '-'){
						after += "H"; 
					}
				}
			}else if(type == 2){
				for(int i = 0 ; i < before.Length;i++){
					if(before[i] == '0'){
						after += "A"; 
					}else if(before[i] == '1'){
						after += "S"; 
					}else if(before[i] == '2'){
						after += "E"; 
					}else if(before[i] == '3'){
						after += "M"; 
					}else if(before[i] == '4'){
						after += "U"; 
					}else if(before[i] == '5'){
						after += "L"; 
					}else if(before[i] == '6'){
						after += "Y"; 
					}else if(before[i] == '7'){
						after += "I"; 
					}else if(before[i] == '8'){
						after += "P"; 
					}else if(before[i] == '9'){
						after += "T"; 
					}else if(before[i] == '-'){
						after += "Q"; 
					}
				}
			}		
		}
		return after;
	}

	private static string decode(int type, string before){
		string after = ""; 
		
		if(before != ""){
			if(type == 0){
				for(int i = 0 ; i < before.Length;i++){
					if(before[i] == 'M'){
						after += "0"; 
					}else if(before[i] == 'I'){
						after += "1"; 
					}else if(before[i] == 'G'){
						after += "2"; 
					}else if(before[i] == '3'){
						after += "3"; 
					}else if(before[i] == '5'){
						after += "4"; 
					}else if(before[i] == 'S'){
						after += "5"; 
					}else if(before[i] == 'E'){
						after += "6"; 
					}else if(before[i] == '0'){
						after += "7"; 
					}else if(before[i] == '7'){
						after += "8"; 
					}else if(before[i] == '1'){
						after += "9"; 
					}else if(before[i] == 'R'){
						after += "-"; 
					}
				}
			}else if(type == 1){
				for(int i = 0 ; i < before.Length;i++){
					if(before[i] == 'E'){
						after += "0"; 
					}else if(before[i] == 'S'){
						after += "1"; 
					}else if(before[i] == '9'){
						after += "2"; 
					}else if(before[i] == 'K'){
						after += "3"; 
					}else if(before[i] == 'Y'){
						after += "4"; 
					}else if(before[i] == 'D'){
						after += "5"; 
					}else if(before[i] == 'R'){
						after += "6"; 
					}else if(before[i] == 'A'){
						after += "7"; 
					}else if(before[i] == 'X'){
						after += "8"; 
					}else if(before[i] == '1'){
						after += "9"; 
					}else if(before[i] == 'H'){
						after += "-"; 
					}
				}
			}else if(type == 2){
				for(int i = 0 ; i < before.Length;i++){
					if(before[i] == 'A'){
						after += "0"; 
					}else if(before[i] == 'S'){
						after += "1"; 
					}else if(before[i] == 'E'){
						after += "2"; 
					}else if(before[i] == 'M'){
						after += "3"; 
					}else if(before[i] == 'U'){
						after += "4"; 
					}else if(before[i] == 'L'){
						after += "5"; 
					}else if(before[i] == 'Y'){
						after += "6"; 
					}else if(before[i] == 'I'){
						after += "7"; 
					}else if(before[i] == 'P'){
						after += "8"; 
					}else if(before[i] == 'T'){
						after += "9"; 
					}else if(before[i] == 'Q'){
						after += "-"; 
					}
				}
			}
		}
		
		return after;
	}
}
