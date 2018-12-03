using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Obj03_Transparent : MonoBehaviour {
	public bool mode = true;//隱形or現形
	public float startTime = 0;//運算開始時間
	public float transPlus;//運算量

	private float start ;//動作開始時間
	private float transparent = 0.0f;//透明度
	private Image image ;//目標圖片

	void Start () {
		start = Time.time;//開始時間紀錄 
		transparent = mode == true ? 0 : 255;//初始量設定

		image = GetComponent<Image> ();
		image.color = new Color(1f,1f,1f,transparent);//設定實際初始量
	}
	
	void Update () {
		if((Time.time - start) > startTime) {//開始時間   
			if(mode){
				transparent += transPlus * Time.deltaTime;//透明度運算
			}else{
				transparent -= transPlus * Time.deltaTime;//透明度運算
			}
			image.color = new Color(1f,1f,1f,transparent/255.0f);//設定實際初始量
		}
	}
}
