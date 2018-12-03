using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game15_Score : MonoBehaviour {

	public Text topN;
	public Text nextN;
	public Text nowN;

	public Text top;
	public Text next;
	public Text now;

	private int range = 0;
	private int index = 0;

	private int temper = 0;
	private string temperN ;


	// Use this for initialization
	void OnEnable () {
		range = (int)Game02_UfoController.S_range;

		for(int i = 0; i < Self08_GameInformation.recode.Length ; i++){
			if(range > Self08_GameInformation.recode[i]){
				index = i;
				move ();
				i = Self08_GameInformation.recode.Length;
			}
		}
	}

	private void move (){
		for(int i = Self08_GameInformation.recode.Length - 1; i > index; i--){
			temper = Self08_GameInformation.recode[i];
			temperN = Self08_GameInformation.recodeN[i];

			Self08_GameInformation.recode[i] = Self08_GameInformation.recode[i-1];
			Self08_GameInformation.recodeN[i] = Self08_GameInformation.recodeN[i-1];
		}
		Self08_GameInformation.recode[index] = range;
		Self08_GameInformation.recodeN[index] = "UFOGO";

		topN.text = Self08_GameInformation.recodeN[0];
		top.text = Self08_GameInformation.recode[0].ToString();

		nextN.text = index == 0 ? Self08_GameInformation.recodeN[0] : Self08_GameInformation.recodeN[index-1];
		next.text = index == 0 ? Self08_GameInformation.recode[0].ToString() : Self08_GameInformation.recode[index-1].ToString();

		now.text = Self08_GameInformation.recode[index].ToString();
	}

	public void writeName(){
		Self08_GameInformation.recodeN[index] = nowN.text;
	}
}
