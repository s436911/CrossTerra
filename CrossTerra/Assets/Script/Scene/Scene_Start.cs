using UnityEngine;
using System.Collections;

public class Scene_Start : MonoBehaviour {

	private bool skiper = false ;

	IEnumerator wait() {
		yield return new WaitForSeconds(3.5f);

		if(!skiper){			
			Const02_SceneSwitcher.next_Scene("S01_Preload");
			skiper = true;//不可下達skip制令		
		}

		yield break;
	}

	void Start() {
		StartCoroutine (wait ());
	}
	
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.Escape)){
			if(!skiper){			
				Const02_SceneSwitcher.next_Scene("S01_Preload");	
				skiper = true;//不可下達skip制令	
			}
		}
	}

}
