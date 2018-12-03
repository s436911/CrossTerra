using UnityEngine;
using System.Collections;

public class Const02_SceneSwitcher : MonoBehaviour {    	
	public static GameObject[] click = new GameObject[4];
	public float fadeGap;//淡出、淡入速度
	public Texture fadeText;//淡出圖片

	private static bool trigger = true ;//一開始進行動作//true透明化false黑化
	private static bool needSwitch = false ;//需要切換畫面
	private static string tgtScene ;//切換tgt/tgtScene
	private Color fader = new Color(1f,1f,1f,1f);//初始亮度

	void Start () {
		DontDestroyOnLoad(gameObject);//所有場景保存		

		int i = 0;
		
		foreach (Transform a in transform) {
			click[i] = a.gameObject;
			i++;
		}
	}

	//--------------Common-------------------

	public static void next_Scene(string tgt){//更換畫面	
		switch_Back();//變黑
		needSwitch = true;//開切換畫面
		tgtScene = tgt;//儲存目標頁面        
	}

	public static void switch_Back(){//變換狀態/true切換黑化/false切換透明化
		trigger = trigger == true ? false : true;
	}
    

	//--------------Game---------------------


	void OnGUI () {
		GUI.color = fader;//設定目標使用的淡出淡入GUI
		GUI.DrawTexture(new Rect(0, 0, Screen.width+10, Screen.height+10),fadeText);//淡出淡入面板
	}
	
	void Update () {
		//Debug.Log (System.DateTime.Now.Month + "/" + System.DateTime.Now.Day + "/" + System.DateTime.Now.Hour + "/" + System.DateTime.Now.Minute);

		//Debug.Log (nowPlay + "ss" + Bgm[nowPlay].isPlaying);
		if (Input.GetKeyUp (KeyCode.Mouse0) ){
			if(!click[0].activeSelf){
				click[0].transform.position = Input.mousePosition;
				click[0].SetActive(true);
			}else if (!click[1].activeSelf){
				click[1].transform.position = Input.mousePosition;
				click[1].SetActive(true);
			}else if (!click[2].activeSelf){
				click[2].transform.position = Input.mousePosition;
				click[2].SetActive(true);
			}else if (!click[3].activeSelf){
				click[3].transform.position = Input.mousePosition;
				click[3].SetActive(true);
			}
		}

		if(trigger == true && fader.a > 0f){//透明
			fader.a = fader.a - fadeGap * Time.deltaTime;
		}
		
		if(trigger == false && fader.a < 1f){//變黑
			fader.a = fader.a + fadeGap * Time.deltaTime;
		}
		
		if (fader.a >= 1f && needSwitch == true){//完全變黑&&需要更換場景_切換場景
			click[0].SetActive(false);
			click[1].SetActive(false);
			click[2].SetActive(false);
			click[3].SetActive(false);

			Application.LoadLevel(tgtScene);   


			needSwitch = false;//設置成不需要更換場景
			switch_Back();
		}
	}
}
