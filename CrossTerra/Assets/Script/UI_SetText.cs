using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_SetText : MonoBehaviour {
	//public string defaultText;
	//public string CdefaultText;
	public bool world;
	public bool tips = true;

	private Text thisText;
	
	// Use this for initialization
	void Start () {
		thisText = GetComponent<Text> ();

		//thisText.text = Self08_GameInformation.isEnglish ? defaultText : CdefaultText;

		if(tips){
			int a = Random.Range(0,40);
			if(a == 1){
				thisText.text = Self08_GameInformation.isEnglish ? "You can change ID in Option!" : "你可以在設定中更換ID!";
			}else if(a == 2){
				thisText.text = Self08_GameInformation.isEnglish ? "You can change Team Mark in Option!" : "你可以在設定中更換隊徽!";
			}else if(a == 3){
				thisText.text = Self08_GameInformation.isEnglish ? "You can change Language in Option!" : "你可以在設定中更換語言!";
			}else if(a == 4){
				thisText.text = Self08_GameInformation.isEnglish ? "You can change Volume in Option!" : "你可以在設定中更換音量!";
			}else if(a == 5){
				thisText.text = Self08_GameInformation.isEnglish ? "You can buy new Ship in your Base!" : "你可以在基地中購買新戰艦!";
			}else if(a == 6){
				thisText.text = Self08_GameInformation.isEnglish ? "You can buy new equipment in your Base!" : "你可以在基地中購買新裝備!";
			}else if(a == 7){
				thisText.text = Self08_GameInformation.isEnglish ? "You can upgrade Ship in your Base!" : "你可以在基地中升級戰艦!";
			}else if(a == 8){
				thisText.text = Self08_GameInformation.isEnglish ? "Machinegun is designed to destroy the high speed ship!" : "機槍主要用來對付高機動性船隻!";
			}else if(a == 9){
				thisText.text = Self08_GameInformation.isEnglish ? "Depthcharge is designed to destroy the Submarine!" : "深水炸彈主要用來對付潛艇!";
			}else if(a == 10){
				thisText.text = Self08_GameInformation.isEnglish ? "Aircraft is designed to destroy the large ship like Titan or Carrier!" : "艦載機主要用來對付大型船隻，像是泰坦或航空母艦!";
			}else if(a == 11){
				thisText.text = Self08_GameInformation.isEnglish ? "Long range anti-air MachineGun is designed to protect other ship!" : "長程防空機砲主要用來保護其他船隻!";
			}else if(a == 12){
				thisText.text = Self08_GameInformation.isEnglish ? "Short range anti-air MachineGun is designed to protect himself!" : "短程防空機砲主要用來保護自身!";
			}else if(a == 13){
				thisText.text = Self08_GameInformation.isEnglish ? "Torpedo can damege enemy above or below the water surface!" : "魚雷可以攻擊水面上和水面下的目標!";
			}else if(a == 14){
				thisText.text = Self08_GameInformation.isEnglish ? "Every weapon can damage our ship, so heavy weapon carefully!" : "每一種武器都可以傷害到我方船隻，所以要謹慎使用重型武器!";
			}else if(a == 15){
				thisText.text = Self08_GameInformation.isEnglish ? "Destroy enemy as soon as possible before Titan arrive!" : "在泰坦抵達前盡快清除敵艦!";
			}else if(a == 16){
				thisText.text = Self08_GameInformation.isEnglish ? "Put Submarine in front line can protect our fleet from Torpedo!" : "把潛艇放置在前線可以減少魚雷對艦隊的損傷!";
			}else if(a == 17){
				thisText.text = Self08_GameInformation.isEnglish ? "Only Torpedo and Depthcharge can damege the enemy below the water surface!" : "只有魚雷和深水炸彈可以對水面下的敵人造成傷害!";
			}else if(a == 18){
				thisText.text = Self08_GameInformation.isEnglish ? "Destroy enemy Carrier as soon as possible!" : "盡快破壞敵方的航空母艦!";
			}else if(a == 19){
				thisText.text = Self08_GameInformation.isEnglish ? "Put some anti-air MachineGun or anti-air Missile in your fleet!" : "放置一些防空機槍在你的艦隊中吧!";
			}else if(a == 20){
				thisText.text = Self08_GameInformation.isEnglish ? "Battleship move very slow ,but they have heavy fire power and armor!" : "主力艦的速度緩慢，但擁有強大的火力和裝甲!";
			}else if(a == 21){
				thisText.text = Self08_GameInformation.isEnglish ? "Cruiser have a lots of equipments to against different situation!" : "巡洋艦有大量的裝備去對應各種戰況!";
			}else if(a == 22){
				thisText.text = Self08_GameInformation.isEnglish ? "Destroyer is fast and powerful!" : "驅逐艦快速且火力強大!";
			}else if(a == 23){
				thisText.text = Self08_GameInformation.isEnglish ? "Small ship is very easy to upgrade!" : "小型船隻很容易升級!";
			}else if(a == 24){
				thisText.text = Self08_GameInformation.isEnglish ? "Small ship is good to attract enemy fire power!" : "小型船隻但於吸引敵方砲火!";
			}else if(a == 25){
				thisText.text = Self08_GameInformation.isEnglish ? "Get high rank in battle can increase rewards!" : "在戰鬥中獲得高評價可以增加報酬!";
			}else if(a == 26){
				thisText.text = Self08_GameInformation.isEnglish ? "If you see Flying Container destroy them!" : "如果你看見會飛的貨櫃，破壞他們吧!";
			}else if(a == 27){
				thisText.text = Self08_GameInformation.isEnglish ? "Small ship is very easy to upgrade!" : "小型船隻很容易升級!";
			}else if(a == 28){
				thisText.text = Self08_GameInformation.isEnglish ? "Small ship is good to attract enemy fire power!" : "小型船隻但於吸引敵方砲火!";
			}else if(a == 29){
				thisText.text = Self08_GameInformation.isEnglish ? "Use trade team to get some free coins!" : "用交易船隊領取一些現金吧!";
			}else if(a == 30){
				thisText.text = Self08_GameInformation.isEnglish ? "Naval Mine is designed to destroy the huge fleet!" : "水雷主要用來對付大型艦隊!";
			}else{
				thisText.text = Self08_GameInformation.isEnglish ? "MIGstudio presents!" : "MIGstudio presents!";
			}
		}

		if (world) {
			Self08_GameInformation.Print = this;
		}

	}
	
	public void set_Text(string data){
		thisText.text = data;
	}
}
