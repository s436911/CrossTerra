using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_ECC : MonoBehaviour {

	public string Cinfo;
	public int reduce;

	// Use this for initialization
	void Awake () {
		if(!Self08_GameInformation.isEnglish){
			GetComponent<Text>().text = Cinfo;
			GetComponent<Text>().fontSize = GetComponent<Text>().fontSize - reduce;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
