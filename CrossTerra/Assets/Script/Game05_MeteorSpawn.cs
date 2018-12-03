using UnityEngine;
using System.Collections;

public class Game05_MeteorSpawn : MonoBehaviour {
	public static Game05_MeteorSpawn meteorSpawn;

	public GameObject IceHit;
	public GameObject Drill;
	public GameObject Undead;
	public GameObject shockWave;

	public GameObject power;

	public GameObject[] UFOFab;
	public GameObject[] ItemFab;
	public GameObject[] CometFab;

	public GameObject[] MeteorFab;
	public GameObject[] MeteorIceFab;
	public GameObject[] MeteorMagFab;
	public GameObject[] MeteorSpearFab;
	public GameObject[] MeteorSatelliteFab;

	private GameObject MeteorPower;

	private float CD = 0;
	private GameObject tempObj;
	private float level;
	public int areaNow = 0;
	public int areaRandNow = 0;
	private Vector2 tempPos;

	private const int meteorTypeNum = 5;

	void Start(){
		meteorSpawn = this;
	}

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		if(CD == 0){
			CD = Time.time;
			level = Game02_UfoController.ufoController.S_LV * 0.05f;//100SLV = 12.5 level


			if(Random.Range(0,300) < 16 + level){
				if(Random.Range(0,10) < 3){
					if(Random.Range(0,10) < 5){
						tempPos = new Vector2(600,600);
						
						if(Random.Range(0,10) < 7){
							tempObj = Instantiate(UFOFab[0],tempPos,Quaternion.identity) as GameObject;
						}else{
							tempObj = Instantiate(UFOFab[1],tempPos,Quaternion.identity) as GameObject;
						}
					}else{
						tempPos = new Vector2(-150,600);
						
						if(Random.Range(0,10) < 8){
							tempObj = Instantiate(UFOFab[0],tempPos,Quaternion.identity) as GameObject;
						}else{
							tempObj = Instantiate(UFOFab[1],tempPos,Quaternion.identity) as GameObject;
						}
					}
				}else if(Random.Range(0,10) < 5){
					if(Random.Range(0,10) < 5){
						tempPos = new Vector2(600,800);
						
						if(Random.Range(0,10) < 8){
							tempObj = Instantiate(UFOFab[2],tempPos,Quaternion.identity) as GameObject;
						}else{
							tempObj = Instantiate(UFOFab[3],tempPos,Quaternion.identity) as GameObject;
						}
					}else{
						tempPos = new Vector2(-150,800);
						
						if(Random.Range(0,10) < 7){
							tempObj = Instantiate(UFOFab[2],tempPos,Quaternion.identity) as GameObject;
						}else{
							tempObj = Instantiate(UFOFab[3],tempPos,Quaternion.identity) as GameObject;
						}
					}
				}else{
					tempObj = Instantiate(UFOFab[4],new Vector2(Random.Range(150,300),Screen.height + 100),Quaternion.identity) as GameObject;
				}
				tempObj.transform.parent = transform;
			}	

			if(Random.Range(0,2000) < 2 + level){
				tempObj = Instantiate(CometFab[(int)Random.Range(0,CometFab.Length)],new Vector2(Random.Range(transform.position.x - 300,transform.position.x + 300),1600),Quaternion.identity) as GameObject;
				tempObj.transform.parent = transform;
			}			

			if(Random.Range(0,100) < 60 + level){
				meteorDrop(300,0);//隕石掉落左側
			}

			if(Random.Range(0,100) < 60 + level){
				meteorDrop(0,300);//隕石掉落右側
			}

			//隕石掉落中左
			if(Random.Range(0,100) < 32 + level){
				meteorDrop(200,100);
			}

			//隕石掉落中右
			if(Random.Range(0,100) < 32 + level){
				meteorDrop(100,200);
			}

			//隕石掉落大中
			if(Random.Range(0,100) < 4 + level){
				meteorDrop(200,200);
			}

			//隕石掉落小中
			if(Random.Range(0,100) < -24 + level){
				meteorDrop(100,100);
			}

			//物品掉落中央
			if(Random.Range(0,10) < 2){
				tempObj = Instantiate(ItemFab[(int)Random.Range(0,ItemFab.Length)],new Vector2(Random.Range(transform.position.x - 200,transform.position.x + 200),transform.position.y),Quaternion.identity) as GameObject;
				tempObj.transform.parent = transform;
			}

			//能量掉落中央
			if(!MeteorPower){
				MeteorPower = Instantiate(power,new Vector2(Random.Range(transform.position.x - 200,transform.position.x + 200),transform.position.y),Quaternion.identity) as GameObject;
				MeteorPower.transform.parent = transform;
			}

		}else{
			if(Time.time - CD > 3){
				CD = 0;
			}
		}
	}

	private void meteorDrop(float left,float right){
		try{
			int area;

			if(Random.Range(0,100) < 85){
				area = areaNow;
			}else{
				area = areaRandNow;
			}


			if(area == 0){
				tempObj = Instantiate(MeteorFab[(int)Random.Range(0,MeteorFab.Length)],new Vector2(Random.Range(transform.position.x - left,transform.position.x + right),transform.position.y),Quaternion.identity) as GameObject;
			}else if(area == 1){
				tempObj = Instantiate(MeteorFab[(int)Random.Range(0,MeteorFab.Length)],new Vector2(Random.Range(transform.position.x - left,transform.position.x + right),transform.position.y),Quaternion.identity) as GameObject;
			}else if(area == 2){
				tempObj = Instantiate(MeteorIceFab[(int)Random.Range(0,MeteorIceFab.Length)],new Vector2(Random.Range(transform.position.x - left,transform.position.x + right),transform.position.y),Quaternion.identity) as GameObject;
			}else if(area == 3){
				tempObj = Instantiate(MeteorMagFab[(int)Random.Range(0,MeteorMagFab.Length)],new Vector2(Random.Range(transform.position.x - left,transform.position.x + right),transform.position.y),Quaternion.identity) as GameObject;
			}else if(area == 4){
				tempObj = Instantiate(MeteorSpearFab[(int)Random.Range(0,MeteorSpearFab.Length)],new Vector2(Random.Range(transform.position.x - left,transform.position.x + right),transform.position.y),Quaternion.identity) as GameObject;
			}else if(area == 5){
				tempObj = Instantiate(MeteorSatelliteFab[(int)Random.Range(0,MeteorSatelliteFab.Length)],new Vector2(Random.Range(transform.position.x - left,transform.position.x + right),transform.position.y),Quaternion.identity) as GameObject;
			}
			tempObj.transform.parent = transform;
		}catch{
			tempObj = Instantiate(MeteorFab[(int)Random.Range(0,MeteorFab.Length)],new Vector2(Random.Range(transform.position.x - left,transform.position.x + right),transform.position.y),Quaternion.identity) as GameObject;
			tempObj.transform.parent = transform;
		}
	}

	public void clear(){
		foreach(Transform meteor in transform){
			Destroy(meteor.gameObject);
		}
		areaNow = 0;
		areaRandNow = 0;

		CD = 0.1f;
	}

	public void areaSwitch(){
		areaNow = Random.Range(0,meteorTypeNum + 1);
		areaRandNow = Random.Range(0,meteorTypeNum + 1);

		if(areaNow == 0 || areaNow == 1){
			Debug.Log ("Now area : Normal");
		}else if(areaNow == 2){
			Debug.Log ("Now area : Ice");
		}else if(areaNow == 3){
			Debug.Log ("Now area : Mag");
		}else if(areaNow == 4){
			Debug.Log ("Now area : Spear");
		}else if(areaNow == 5){
			Debug.Log ("Now area : Satellite");
		}

		if(areaRandNow == 0 || areaRandNow == 1){
			Debug.Log ("Rand area : Normal");
		}else if(areaRandNow == 2){
			Debug.Log ("Rand area : Ice");
		}else if(areaRandNow == 3){
			Debug.Log ("Rand area : Mag");
		}else if(areaRandNow == 4){
			Debug.Log ("Now area : Spear");
		}else if(areaRandNow == 5){
			Debug.Log ("Now area : Satellite");
		}
	}
}
