using UnityEngine;
using System.Collections;

public class Game13_Laser : MonoBehaviour {

	public bool elite = false;
	public GameObject light;
	public GameObject light2;
	public GameObject light3;
	public bool isActive = false;

	public void openAnimD(){
		light3.SetActive(true);
	}
	
	public void closeAnimD(){
		light3.SetActive(false);
	}

	public void openAnimB(){
		light2.SetActive(true);
	}

	public void closeAnimB(){
		light2.SetActive(false);
	}

	public void closeAnimA(){
		light.SetActive(false);
		if(elite){
			GetComponentInParent<Game12_Enemy2>().elite = true;
		}
	}

	public void closeAnimC(){
		gameObject.SetActive(false);
		transform.GetComponentInParent<Game12_Enemy2>().continueAct();
	}
}
