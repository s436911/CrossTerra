using UnityEngine;
using System.Collections;

public class Game12_Enemy2 : MonoBehaviour {
	public Vector2 direction = Vector2.zero;
	public GameObject bullet;
	public GameObject light;


	private bool locked = false;
	private float CD = 0;
	private GameObject tempObj;
	public bool elite = false;
	private Vector3 relative;


	public void continueAct(){
		GetComponent<Rigidbody2D>().velocity = direction ;
	}

	// Use this for initialization
	void Start () {
		if (transform.position.x > 450 ){
			direction.x *= -1;
		}

		GetComponent<Rigidbody2D>().velocity = direction ;
	}
	
	// Update is called once per frame
	void Update () {

		if(transform.position.x < -300 || transform.position.x > 750){
			Destroy(gameObject);
		}
		
		
		if(CD == 0){
			if(!locked){
				CD = Time.time;
				
				if(transform.position.y < 800 && transform.position.y > 0 && transform.position.x > 50 && transform.position.x < 400){
					if(Random.Range(0,100) < 6 || (transform.position.x > 220 && transform.position.x < 230)){
						locked = true;
						GetComponent<Rigidbody2D>().velocity = Vector2.zero;
						light.SetActive(true);
						bullet.SetActive(true);
					}
					
				}
				
				relative = transform.InverseTransformPoint(Game02_UfoController.ufoController.transform.position);
				float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
				transform.Rotate(0,0, -angle);
			}else{
				if(elite){
					CD = Time.time;

					relative = transform.InverseTransformPoint(Game02_UfoController.ufoController.transform.position);
					float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
					transform.Rotate(0,0, -angle / Mathf.Abs(angle) * 1.75f);
				}
			}
			
		}else{
			if(Time.time - CD > 0.05f){
				CD = 0;
			}
		}
	}
}
