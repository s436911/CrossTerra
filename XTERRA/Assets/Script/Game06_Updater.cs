using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game06_Updater : MonoBehaviour {


	private bool counter = false;

	public  Text speedShower;
	public  Text rangeShower;

	// Update is called once per frame
	void Update () {
		counter = counter ? false : true;

		if(counter && Time.timeScale != 0){
			Game02_UfoController.S_range = Game02_UfoController.S_range + Game02_UfoController.SpeedNow * 15 * Time.deltaTime;
			speedShower.text = Game02_UfoController.SpeedNow * 10 + " .KM";
			rangeShower.text = (int)Game02_UfoController.S_range + " .KM";
		}
	}
}
