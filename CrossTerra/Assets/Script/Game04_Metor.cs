using UnityEngine;
using System.Collections;

public class Game04_Metor : MonoBehaviour {

	public GameObject destrotAnim;
	public int speed = 0;

	public bool rock = false;
	public bool ice = false;
	public bool frag = false;
	public bool magnet = false;
	public bool satellite = false;

	public bool healthL = false;
	public bool healthS = false;
	public bool powerL = false;
	public bool powerS = false;
	public bool Undead = false;
	public bool Speed = false;
	public bool Wave = false;
	public bool Drill = false;

	public bool Unknown = false;

	public bool isActive = false;
	private Rigidbody2D metorRigid;


	// Use this for initialization
	void Start () {
		int temp = 0;

		if(rock && Random.Range(0,100) < 30){//隕石速度突變
			speed += 1;
			if(Random.Range(0,100) < 30){//隕石速度突變
				speed += 1;
			}
		}

		transform.localScale *= Random.Range(0.8f,1.5f);

		if(Unknown){
			temp = Random.Range(1,7 + 1);
			if(temp == 1){
				Speed = true;
			}else if(temp == 2){
				healthL = true;
			}else if(temp == 3){
				healthS = true;
			}else if(temp == 4){
				powerL = true;
			}else if(temp == 5){
				powerS = true;
			}else if(temp == 6){
				Undead = true;
			}else if(temp == 7){
				Drill = true;
			}else{
				Wave = true;
			}
		}

		if(transform.position.x > 350 ){
			temp = -10;
		}else if(transform.position.x < 150){
			temp = 10;
		}else {
			temp = 0;
		}

		metorRigid = GetComponent<Rigidbody2D>();
		metorRigid.velocity = new Vector2(Random.Range(temp -15,temp + 15),Random.Range(-10 + (-15 * speed),-10 + (-60 * speed)));
		metorRigid.angularVelocity = Random.Range(-50,50);
	}

	// Update is called once per frame
	void Update () {
		if(transform.position.y < -100 || transform.position.y > 1100 || transform.position.x > 500 || transform.position.x < -50){
			Destroy(gameObject);
		}
	}

	public void DestroyGameObject (){
		// Destroy this gameobject, this can be called from an Animation Event.
		GameObject tempObj = Instantiate(destrotAnim,transform.position,Quaternion.identity) as GameObject;
		Destroy (gameObject);
	}
}
