using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI01_Slider : MonoBehaviour {

	public Slider slider;
	private float a = 0.45f;
	//public GameObject obj;
			
	// Update is called once per frame
	void Update () {

			slider.value = Mathf.MoveTowards (slider.value, 100.0f, a);
		if (slider.value == 100.0f) {
			a = -a;
		}
	}
}
