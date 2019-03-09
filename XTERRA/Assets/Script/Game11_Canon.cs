using UnityEngine;
using System.Collections;

public class Game11_Canon : MonoBehaviour {

	public int speed = 0;
	public bool ice = false;
	
	// Use this for initialization
	void Start () {


		Vector2 direction = Game02_UfoController.ufoController.transform.position - transform.position;
		Vector2 directionAbs = new Vector2(Mathf.Abs(direction.x),Mathf.Abs(direction.y));
		
		if( directionAbs.x > directionAbs.y){
			direction /= directionAbs.x;
		}else if(directionAbs.x < directionAbs.y){
			direction /= directionAbs.y;
		}else{
			direction = Vector2.zero;
		}
		

		GetComponent<Rigidbody2D>().velocity = direction * speed;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < -100 || transform.position.y > 1100 || transform.position.x > 500 || transform.position.x < -50){
			Destroy(gameObject);
		}
	}
}
