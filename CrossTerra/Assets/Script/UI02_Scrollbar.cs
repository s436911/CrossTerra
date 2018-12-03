using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI02_Scrollbar : MonoBehaviour {
	public GameObject scroll_g;
	private Scrollbar scroll;

	private RectTransform rect;
	private RectTransform temp;

	private float high ;
	private float y ;
	// Use this for initialization

	void Start () {  
		scroll = scroll_g.GetComponent<Scrollbar> ();

		temp = scroll_g.GetComponent<RectTransform> ();
		rect = GetComponent<RectTransform> ();  

		high = rect.rect.height;
		high -= temp.rect.height;

		y = rect.anchoredPosition.y;
	}  
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (high + " * " + scroll.value);
		rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, (high * scroll.value) + y);
	}

	//rectTransform.anchoredPosition = rect2.anchoredPosition;
	//rectTransform.sizeDelta = rect2.sizeDelta;
	//rectTransform.eulerAngles = rect2.eulerAngles;
}
