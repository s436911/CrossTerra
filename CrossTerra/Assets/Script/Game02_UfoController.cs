using UnityEngine;
using System.Collections;

public class Game02_UfoController : MonoBehaviour {
	public static Game02_UfoController ufoController;
	public static float S_range = 0; 
	public static Transform S_UFO;
	public static float SpeedNow = 0;
	public static float speedJet = 1;
	public static float speedIce = 1;
	public static bool reverse = false;

	public float S_LV = 0;
	public float speed = 2.5f;

	private Vector2 direction;
	private Coroutine cououtine;
		
	void Update(){
		if(S_range > S_LV * 250 + 250){//1 == 250LY
			S_LV = S_LV + 1;
			if(S_LV % 10 == 0){//10LY
				Game05_MeteorSpawn.meteorSpawn.areaSwitch();
			}
		}

		SpeedNow = speed * speedIce * speedJet;
	}

	void Awake(){
		ufoController = this;
	}

	void Start(){
		S_UFO = gameObject.transform;
	}

	public static void ice (){
		speedIce = 0.5f;
	}

	public static void jet (){
		speedJet = 2;
	}

	public static void reset(){
		Game02_UfoController.S_range = 0;
		Game02_UfoController.ufoController.S_LV = 0;
	}

	private IEnumerator Move(){
			
		while(true){
			if(!reverse){
				this.GetComponent<Rigidbody2D>().velocity = 70 * this.direction * SpeedNow;		
			}else{
				this.GetComponent<Rigidbody2D>().velocity = -70 * this.direction * SpeedNow;		
			}
			yield return null;
		}
	}
		
	public void BeginMove(){
			
		//this.target.SetBool(this.parameters.moving, true);
		this.cououtine = StartCoroutine(this.Move());
	}
		
	public void EndMove(){
		this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;	
		//this.target.SetBool(this.parameters.moving, false);
		StopCoroutine(this.cououtine);
	}
		
	public void UpdateDirection(Vector2 direction){
		this.direction = direction;
	}

	public void UpdateDirection(Vector3 direction){
		this.direction = (Vector2)direction;
	}
}
