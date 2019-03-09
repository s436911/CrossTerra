using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class BattleManager : MonoBehaviour {
	public GameObject t_tempPrefab;

	public int t_enum = 5;
	public int t_anum = 5;
	
	public bool t_trigger = false;

	protected void Start() {
		Stub();
		Init();
	}

	List<EntityStatus> t_entityList = new List<EntityStatus>();
	
	private void Stub() {
		Database.LoadDatabase();

		EntityStatus caseStatus = new EntityStatus();
		caseStatus.SetWeapon(Database.weaponDataBase[2]);
		caseStatus.specID = 1;
		t_entityList.Add(caseStatus);

		caseStatus = new EntityStatus();
		caseStatus.specID = 2;
		caseStatus.SetWeapon(Database.weaponDataBase[3]);
		caseStatus.SetWeapon(Database.weaponDataBase[5]);
		t_entityList.Add(caseStatus);

		caseStatus = new EntityStatus();
		caseStatus.specID = 3;
		caseStatus.SetWeapon(Database.weaponDataBase[4]);
		t_entityList.Add(caseStatus);

		//骷髏巨人
		caseStatus = new EntityStatus();
		caseStatus.specID = 4;
		caseStatus.SetWeapon(Database.weaponDataBase[1]);
		t_entityList.Add(caseStatus);

		//火焰骷髏巨人
		caseStatus = new EntityStatus();
		caseStatus.specID = 5;
		caseStatus.SetWeapon(Database.weaponDataBase[2]);
		t_entityList.Add(caseStatus);
	}

	private void Init() {
		for (int i = 0; i < t_anum; i++) {
			int caseEntityID = Random.Range(2, 5);

			CreateEntity( t_entityList[caseEntityID] , 0 , new Vector2(Random.Range(-1F, 1F), Random.Range(-1F, 1F)));
		}

		for (int i = 0; i < t_enum; i++) {
			int caseEntityID = Random.Range(0, 2);

			CreateEntity( t_entityList[caseEntityID], 1, new Vector2(Random.Range(0, 2) > 0 ? Random.Range(5F, 8F) : Random.Range(-5F, -8F), Random.Range(0F, 2F) > 0 ? Random.Range(-2, 2F) : Random.Range(-2, 2)));
		}
	}

	protected void Update() {
		if (t_trigger) {
			t_trigger = false;
		}
	}

	public void CreateEntity(EntityStatus InStatus, sbyte InForce , Vector2 InPos) {
		GameObject lastCrt = Instantiate(t_tempPrefab);
		lastCrt.name = (InForce == 0 ? "PC_" : "NPC_") +  Database.speciesBase[InStatus.specID].sName;
		EntityBase lastEB = lastCrt.GetComponent<EntityBase>();
		lastEB.Init(InStatus , InForce);
		lastEB.transform.SetParent(BattleSys.Ins.entitySet);
		lastEB.transform.position = InPos;
	}
}
