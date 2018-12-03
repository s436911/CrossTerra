using UnityEngine;
using System.Collections;

public class Game09_Comet : MonoBehaviour {
	public int speed = 0;
			
	public bool Unknown = false;
	
	public bool isActive = false;
	private Rigidbody2D metorRigid;
	
	
	// Use this for initialization
	void Start () {
		int temp = 0;

		transform.localScale *= Random.Range(0.7f,1f);
				
		if(transform.position.x > 350 ){
			temp = -10;
		}else if(transform.position.x < 150){
			temp = 10;
		}else {
			temp = 0;
		}
		Vector3 TGT = new Vector3 (Random.Range(-75,525),0,0);

		Vector2 direction = (Vector2)(TGT - transform.position);
		Vector2 directionAbs = new Vector2(Mathf.Abs(direction.x),Mathf.Abs(direction.y));
		
		if( directionAbs.x > directionAbs.y){
			direction /= directionAbs.x;
		}else if(directionAbs.x < directionAbs.y){
			direction /= directionAbs.y;
		}else{
			direction = Vector2.zero;
		}


		metorRigid = GetComponent<Rigidbody2D>();
		metorRigid.velocity = direction * 50;

		Vector3 relative = transform.InverseTransformPoint(TGT);
		float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
		transform.Rotate(0,0, -angle);

	}
	
	void Update () {
		if(transform.position.y < -800 || transform.position.y > 2200 || transform.position.x > 1200 || transform.position.x < -700){
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D impacter){
		try{
			if(impacter.gameObject.GetComponent<Game04_Metor>()){
				impacter.gameObject.GetComponent<Game04_Metor>().DestroyGameObject();
			}
		}catch{
			if(impacter.gameObject.layer == 8){
			
			}else{
				Debug.Log ("ERR 00");
			}
		}
	}
}