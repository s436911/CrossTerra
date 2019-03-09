using UnityEngine;
using System.Collections;

public class Scene01_Start_Scene : MonoBehaviour {

	public void next_Scene(string next){
		Application.LoadLevel (next);
	}
}
