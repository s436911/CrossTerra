using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_FCs : MonoBehaviour {

	public Text lefter;
	public GameObject TTBut;

	private void openThis(){
		lefter.transform.parent.gameObject.gameObject.SetActive(false);
		TTBut.SetActive(true);
	}

	private void closeThis(){
		TTBut.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		/*

		if(System.DateTime.Now.Month != Self08_GameInformation.FCmonth){
			openThis();
		}else if(System.DateTime.Now.Day != Self08_GameInformation.FCday){
			openThis();
		}else if((System.DateTime.Now.Hour * 60 + System.DateTime.Now.Minute ) - (Self08_GameInformation.FChour * 60 + Self08_GameInformation.FCminute) > 20){
			openThis();
		}else{
			closeThis();
			lefter.transform.parent.gameObject.SetActive(true);
			if(Self08_GameInformation.IsEnglish){
				lefter.text = (20 -((System.DateTime.Now.Hour * 60 + System.DateTime.Now.Minute ) - (Self08_GameInformation.FChour * 60 + Self08_GameInformation.FCminute))).ToString() + " minutes left";
				Debug.Log (System.DateTime.Now.Hour * 60 + System.DateTime.Now.Minute);
			}else{
				lefter.text = "剩餘 " + (20 -((System.DateTime.Now.Hour * 60 + System.DateTime.Now.Minute ) - (Self08_GameInformation.FChour * 60 + Self08_GameInformation.FCminute))).ToString() + " 分鐘";
			}
		}*/
	}
}
