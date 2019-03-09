using UnityEngine;
using System.Collections;

public class Scene_Preload : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if( Self08_GameInformation.loader()){
			Debug.Log ("Load complete!");
			Const02_SceneSwitcher.next_Scene("S02_Menu");
		}else{
			Debug.Log ("Load failed!");
			gameObject.SetActive(false);
		}
	}
}
