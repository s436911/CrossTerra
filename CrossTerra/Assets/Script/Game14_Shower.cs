using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game14_Shower : MonoBehaviour {

	public bool Name = false;
	public int ID = 0;

	void Start () {
		if(Name){
			GetComponent<Text>().text = Self08_GameInformation.recodeN[ID];
		}else{
			GetComponent<Text>().text = Self08_GameInformation.recode[ID].ToString();
		}
	}
}
