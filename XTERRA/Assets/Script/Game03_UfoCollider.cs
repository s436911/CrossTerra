using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game03_UfoCollider : MonoBehaviour {
	public static Game03_UfoCollider ufoCollider;

	public Game05_MeteorSpawn meteorSpawn ;
	public GameObject stopPanel;
	public GameObject ufoShower;

	public Animator touchShower;
	public Image[] buffer;
	public Image energeShower;
	public GameObject[] HPbar;


	public int S_Health = 3;
	public float S_Energe = 1;
	public float S_EnergeReduce = 0.012f;

	public ParticleSystem lightObj;

	private int health = 3;
	private bool danger = false;
	private Vector2 originPos;

	private Color lightNormal = new Color(1,1,0.2f);
	private Color lightDanger = new Color(1,0,0);

	private float CD = 0;

	private float CD_Undead = 0;
	private float CD_Speed = 0;
	private float CD_Ice = 0;
	private float CD_Drill = 0;
	private float CD_Magnet = 0;

	void Start () {
		ufoCollider = this;
		originPos = transform.position;
		setter();
	}

	public void setter(){
		Game02_UfoController.reset();
		transform.position = originPos;
		health = S_Health;
		S_Energe = 1;
		ufoShower.SetActive(true);

		CD_Undead = 0;
		CD_Speed = 0;
		CD_Drill = 0;
		CD_Ice = 0;
		CD_Magnet = 0;

		buffer[0].GetComponent<Game07_Buffer>().pause();
		buffer[1].GetComponent<Game07_Buffer>().pause();
		buffer[2].GetComponent<Game07_Buffer>().pause();
		buffer[3].GetComponent<Game07_Buffer>().pause();
		buffer[4].GetComponent<Game07_Buffer>().pause();
	}

	public void destroyUFO(){
		Game02_UfoController.ufoController.EndMove();
		meteorSpawn.clear();

		foreach(Transform buff in transform){
			if(buff.gameObject.layer == 10){
				Destroy(buff.gameObject);
			}
		}

		ufoShower.SetActive(false);
		stopPanel.SetActive(true);
		Time.timeScale = 0;
		health = S_Health;
		S_Energe = 1;

	}

	// Update is called once per frame
	void Update () {

		if(S_Energe <= 0 ){
			destroyUFO();
		}

		if(health >= 1){
			HPbar[0].SetActive(true);
		}else{
			HPbar[0].SetActive(false);
		}

		if(health >= 2){
			HPbar[1].SetActive(true);
		}else{
			HPbar[1].SetActive(false);
		}

		if(health >= 3){
			HPbar[2].SetActive(true);
		}else{
			HPbar[2].SetActive(false);
		}


		if(health > 1){
			if(danger){
				//danger action
				danger = false;
				lightObj.startColor = lightNormal;
			}
		}else if(health == 1){
			if(!danger){
				danger = true;
				lightObj.startColor = lightDanger;
			}
		}else{
			destroyUFO();
		}

		if(CD == 0){
			CD = Time.time;

			S_Energe -= S_EnergeReduce;
		}else{
			energeShower.fillAmount = S_Energe;

			if(CD_Undead == 0){

			}

			if(Time.time - CD > 1){
				CD = 0;
				if(Time.time - CD_Undead > 5){
					CD_Undead = 0;
				}
				if(Time.time - CD_Speed > 7){
					CD_Speed = 0;
					Game02_UfoController.speedJet = 1;
				}
				if(Time.time - CD_Drill > 7){
					CD_Drill = 0;
				}
				if(Time.time - CD_Ice > 2){
					CD_Ice = 0;
					Game02_UfoController.speedIce = 1;
				}
				if(Time.time - CD_Magnet > 2){
					CD_Magnet = 0;
					Game02_UfoController.reverse = false;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		try{
			if(other.gameObject.GetComponent<Game13_Laser>()){
				if(!other.gameObject.GetComponent<Game13_Laser>().isActive){
					health = CD_Undead == 0 ? health - 2: health;

					other.gameObject.GetComponent<Game13_Laser>().isActive = true;
					GetComponent<AudioSource>().Play();
					Const03_CameraShake.shake = 0.3f;
				}
			}
		}catch{

		}
	}


	void OnCollisionEnter2D(Collision2D impacter){
		try{
			if(impacter.gameObject.GetComponent<Game04_Metor>()){
				if(!impacter.gameObject.GetComponent<Game04_Metor>().isActive ){
					if(impacter.gameObject.GetComponent<Game04_Metor>().rock){//碰撞
						health = CD_Undead == 0 && CD_Drill == 0? health - 1: health;
						
						if(CD_Undead == 0){
							if(impacter.gameObject.GetComponent<Game04_Metor>().ice){
								CD_Ice = Time.time;
								buffer[3].GetComponent<Game07_Buffer>().initialize(3);
								Game02_UfoController.ice();
								
								GameObject tempObj = Instantiate(Game05_MeteorSpawn.meteorSpawn.IceHit,transform.position,Quaternion.identity) as GameObject;
								tempObj.transform.parent = transform;
							}if(impacter.gameObject.GetComponent<Game04_Metor>().magnet){
								CD_Magnet = Time.time;
								buffer[4].GetComponent<Game07_Buffer>().initialize(3);
								Game02_UfoController.reverse = true;
								
								//GameObject tempObj = Instantiate(Game05_MeteorSpawn.meteorSpawn.IceHit,transform.position,Quaternion.identity) as GameObject;
								//tempObj.transform.parent = transform;
							}
						}
						
						impacter.gameObject.GetComponent<Game04_Metor>().DestroyGameObject();
						Const03_CameraShake.shake = 0.3f;
						GetComponent<AudioSource>().Play();
					}else{
						impacter.gameObject.GetComponent<Animator>().Play("ItemTouch");
						touchShower.Play("Touch");
						
						if(impacter.gameObject.GetComponent<Game04_Metor>().powerS){//回復能量小
							S_Energe = S_Energe + 0.5f <= 1 ? S_Energe + 0.5f : 1;
							touchShower.GetComponent<Image>().color = new Color(0.9f,0.9f,0.3f);
						}else if(impacter.gameObject.GetComponent<Game04_Metor>().powerL){//回復能量大
							S_Energe = 1;
							touchShower.GetComponent<Image>().color = new Color(0.9f,0.9f,0.3f);
						}else if(impacter.gameObject.GetComponent<Game04_Metor>().healthL){//回復血量大
							health = S_Health;
							touchShower.GetComponent<Image>().color = new Color(1,0.2f,0.2f);
						}else if(impacter.gameObject.GetComponent<Game04_Metor>().healthS){//回復血量小
							health = health < S_Health ? health + 1 : S_Health;
							touchShower.GetComponent<Image>().color = new Color(1,0.2f,0.2f);
						}else if(impacter.gameObject.GetComponent<Game04_Metor>().Undead){//無敵
							CD_Undead = Time.time;
							GameObject tempObj = Instantiate(Game05_MeteorSpawn.meteorSpawn.Undead,transform.position,Quaternion.identity) as GameObject;
							tempObj.transform.parent = transform;
							
							buffer[0].GetComponent<Game07_Buffer>().initialize(6);
							touchShower.GetComponent<Image>().color = new Color(0.4f,0.6f,1);
						}else if(impacter.gameObject.GetComponent<Game04_Metor>().Speed){//加速
							CD_Speed = Time.time;
							
							Game02_UfoController.jet();
							buffer[1].GetComponent<Game07_Buffer>().initialize(8);
							touchShower.GetComponent<Image>().color = new Color(0.4f,1,0.2f);
						}else if(impacter.gameObject.GetComponent<Game04_Metor>().Wave){//衝擊波
							GameObject tempObj = Instantiate(Game05_MeteorSpawn.meteorSpawn.shockWave,transform.position,Quaternion.identity) as GameObject;
							tempObj.transform.parent = Game05_MeteorSpawn.meteorSpawn.transform;
							touchShower.GetComponent<Image>().color = new Color(1,0.2f,0.9f);
						}else if(impacter.gameObject.GetComponent<Game04_Metor>().Drill){//無敵
							CD_Drill = Time.time;
							GameObject tempObj = Instantiate(Game05_MeteorSpawn.meteorSpawn.Drill,transform.position,Quaternion.identity) as GameObject;
							tempObj.transform.parent = transform;
							
							buffer[2].GetComponent<Game07_Buffer>().initialize(8);
							touchShower.GetComponent<Image>().color = new Color(1,0.6f,0);
						}else{
							Debug.Log (" ERR 01");
						}
					}
					impacter.gameObject.GetComponent<Game04_Metor>().isActive = true;
					impacter.gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
					impacter.gameObject.GetComponent<Rigidbody2D>().drag = 0.1f;
					impacter.gameObject.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-50,50);
				}
			}else if(impacter.gameObject.GetComponent<Game11_Canon>()){
				health = CD_Undead == 0 ? health - 1: health;

				if(CD_Undead == 0){
					if(impacter.gameObject.GetComponent<Game11_Canon>().ice){
						CD_Ice = Time.time;
						buffer[3].GetComponent<Game07_Buffer>().initialize(3);
						Game02_UfoController.ice();
						
						GameObject tempObj = Instantiate(Game05_MeteorSpawn.meteorSpawn.IceHit,transform.position,Quaternion.identity) as GameObject;
						tempObj.transform.parent = transform;
					}
				}
				
				Destroy(impacter.gameObject);
				GetComponent<AudioSource>().Play();
				Const03_CameraShake.shake = 0.3f;
			}else if(impacter.gameObject.GetComponent<Game09_Comet>()){
				health =  0;
				Const03_CameraShake.shake = 0.45f;
				GetComponent<AudioSource>().Play();
			}
		}catch{
			if(impacter.gameObject.layer == 8){
				
			}else{
				Debug.Log ("ERR 00");
			}
		}
	}
}
