using UnityEngine;
using System.Collections;

public class Game10_Enemy : MonoBehaviour {
	public bool drop = false;
	public Vector2 direction = Vector2.zero;
	public GameObject bullet;

	private float CD = 0;
	private float Gape = 0.8f;
	private GameObject tempObj;

	// Use this for initialization
	void Start () {
		if (transform.position.x > 450 ){
			direction.x *= -1;
		}
		if(drop){
			Gape *= 1.8f;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < -300 || transform.position.x > 750 || transform.position.y < -100){
			Destroy(gameObject);
		}


		if(CD == 0){
			CD = Time.time;


			if(transform.position.y < 800 && transform.position.y > 0 && transform.position.x > 50 && transform.position.x < 400){
				tempObj = Instantiate(bullet,transform.position,Quaternion.identity) as GameObject;
				tempObj.transform.parent = transform.parent;
			}


			if(!drop){
				if(transform.position.y > 600 ){
					GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-direction.x * 0.25f,direction.x),Random.Range(direction.y * 0.5f,-direction.y));
				}else{
					GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-direction.x * 0.25f,direction.x),Random.Range(direction.y,-direction.y * 0.5f));
				}
			}else{
				if(Random.Range(0,10) < 5){
					GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-direction.x ,direction.x),Random.Range(0,-direction.y));
				}else{
					GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-direction.x * 0.5f ,direction.x * 0.5f),Random.Range(0,-direction.y));
				}
			}
			
		}else{
			if(Time.time - CD > Gape){
				CD = 0;
			}
		}
	}
}
