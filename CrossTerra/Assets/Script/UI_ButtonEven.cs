using UnityEngine;
using System.Collections;

public class UI_ButtonEven : MonoBehaviour {


	public void save(){//儲存指令
		Self10_IO.SaveFile();
	} 

	public void next_Scene(string next){
		Time.timeScale = 1;
		Const02_SceneSwitcher.next_Scene(next);
	} 

	public void next_Scene(){
		Time.timeScale = 1;
		Const02_SceneSwitcher.next_Scene(Self08_GameInformation.NextScene);
	}

	public void quit_Game(){
		Application.Quit();
	}

	public void TimePause(){
		Time.timeScale = 0;
	}
	
	public void TimeRecover(){
		Time.timeScale = 1;
	}

	public void Connect(string link)	{
		try	{
            Application.OpenURL(link);
        }
		catch {}
	}
}
