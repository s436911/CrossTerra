using UnityEngine;
using System.Collections;

public class Animation01_Common : MonoBehaviour {

	private Animation anim;

	void Start() {
		//anim = GetComponent<Animator>().HasState();
		//Debug.Log (Self10_IO.fname);
		//Self07_SaveInformation.SaveAllInformation ();
		//Self09_LoadInformation.LoadAllInformation ();
	}

	public void DestroyGameObjectP (){
		// Destroy this gameobject, this can be called from an Animation Event.
		Destroy (transform.parent.gameObject);
	}

	public void Close (){
		gameObject.SetActive (false);
	}

	public void Stop (){
		GetComponent<Animator>().speed = 0;
	}

	public void Repeat (){

	}

	public void DestroyGameObject (){
		// Destroy this gameobject, this can be called from an Animation Event.
		Destroy (gameObject);
	}

	public void openAll(){
		foreach(Transform obj in transform){
			obj.gameObject.SetActive(true);
		}
	}

	public void closeAll(GameObject except){
		foreach(Transform obj in transform){
			if(except.transform != obj){
				obj.gameObject.SetActive(false);
			}
		}
		GetComponent<Animator>().Play("PanelExit");
	}
}
