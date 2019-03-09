using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game07_Buffer : MonoBehaviour {
	private float startTime = 0;
	private float timer = 0;

	public void initialize(float time){
		startTime = Time.time;
		timer = time;
	}

	public void pause(){
		timer = 0;
		GetComponent<Image>().fillAmount = 0;
	}

	void Update(){
		if(timer != 0){
			if(Time.time - startTime > timer){
				startTime = 0;
				timer = 0;
			}else{
				GetComponent<Image>().fillAmount = 1 - (Time.time - startTime) / timer;
			}
		}		
	}
}
