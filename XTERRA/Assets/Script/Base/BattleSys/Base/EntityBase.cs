using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class EntityBase : MonoBehaviour {

	[Header("Attribute")]
	public EntityStatus status;
	public sbyte force;

	[Header("System")]
	public SkeletonAnimation sanim;
	public Animator animator;
	public CircleCollider2D collider;
	public Rigidbody2D rigidbody;

	//test
	private float t_clockTime = 2;

	public EntityState nowState;
	private Vector2 tgtPos;
	private EntityBase tgtEnemy;
	private HpBarEffect hpBar;
	private bool frameActed;

	public void Init( EntityStatus InStatus , sbyte InForce) {
		Species caseSpecies = Database.speciesBase[InStatus.specID];

		SkeletonDataAsset caseSkeletonData = Resources.Load< SkeletonDataAsset >("Sanim/" + caseSpecies.model + "/" + caseSpecies.model + "_SkeletonData");
		if (caseSkeletonData == null) {
			Debug.LogError("EntityInitFailed:" + InStatus.specID);
			return;
		}

		sanim.skeletonDataAsset = caseSkeletonData;
		sanim.Initialize(true);

		status = new EntityStatus(InStatus);
		transform.localScale = Vector3.one * caseSpecies.size;
		force = InForce;
		collider.enabled = true;
		rigidbody.simulated = true;

		sanim.ReplaceSpecies(caseSpecies.skin);
		sanim.ReplaceEquip(status.primeWeapon ? status.primeWeapon.skin : "", "weapon");
		sanim.ReplaceEquip(status.subWeapon ? status.subWeapon.skin : "", "shield");
		//優化? ATKSPD

		hpBar = EffectPool.Ins.RegistHpBar(transform , status.armorMCT > 0 , Database.speciesBase[status.specID].size , force);
		DoState(EntityState.Reborn);
	}
	
	// Update is called once per frame
	protected void Update() {
		if (nowState == EntityState.Reborn) {
			return;
		}

		if (nowState == EntityState.Dead) {
			return;
		}

		frameActed = false;

		if (!HadAction()) {
			AIMove();
			AIAttack();
			AIIdle();
		}
		//優化
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * 2);
	}

	protected bool HadAction() {
		//動作中
		if (nowState == EntityState.Atk) {
			return true;
		}

		if (tgtEnemy == null) {
			tgtEnemy = SearchEnemy();

		} else if (tgtEnemy.nowState == EntityState.Dead) {
			tgtEnemy = null;
			tgtEnemy = SearchEnemy();
		}

		return tgtEnemy == null;
	}

	protected void AIIdle() {
		if (frameActed) {
			return;
		}

		DoState(EntityState.Idle);
	}

	protected void AIMove() {
		if (frameActed) {
			return;
		}

		if (tgtEnemy != null) {
			float caseDist = Vector2.Distance(tgtEnemy.transform.position, transform.position);

			if (caseDist > 1f) {
				DoState(EntityState.Walk);
				sanim.timeScale = status.spdERT;

				Vector2 caseVelocity = (tgtEnemy.transform.position - transform.position).normalized * status.spdERT * Time.deltaTime;				
				DoFace(caseVelocity.x > 0);

				caseVelocity += (Vector2)transform.position;
				transform.position = new Vector2(caseVelocity.x, caseVelocity.y);
				
				//Set
				frameActed = true;
			}
		}
	}

	protected void AIAttack() {
		if (frameActed) {
			return;
		}

		if (tgtEnemy != null && tgtEnemy.nowState != EntityState.Dead) {
			switch (status.armState) {
				case ArmState.One:
					DoState(EntityState.Atk);
					break;

				case ArmState.Two:
					DoState(EntityState.Atk, EntityState.AtkHeavy);
					break;

				case ArmState.Shield:
					DoState(EntityState.Atk);
					break;

				case ArmState.Dual:
					DoState(EntityState.Atk);
					break;

				case ArmState.TwoInOne:
					DoState(EntityState.Atk);
					break;

				default :
					Debug.LogError("AtkEquipErr");
					break;
			}

			//Set
			frameActed = true;
		}
	}

	protected void DoFace(bool InFace) {
		sanim.Skeleton.ScaleX = InFace ? -1 : 1;
	}

	protected EntityBase SearchEnemy() {
		EntityBase cacheUnit = null;
		float cacheDist = 0;

		foreach (Transform entity in BattleSys.Ins.entitySet) {
			EntityBase caseEntity = entity.GetComponent<EntityBase>();
			float caseDist;

			if (caseEntity && force != caseEntity.force && caseEntity.nowState != EntityState.Dead) {
				caseDist = Vector2.Distance(caseEntity.transform.position, transform.position);

				if (cacheUnit != null) {
					if (caseDist < cacheDist) {
						cacheUnit = caseEntity;
						cacheDist = caseDist;
					}

				} else {
					cacheUnit = caseEntity;
					cacheDist = caseDist;
				}
			}
		}

		if (cacheUnit) {
			return cacheUnit;
		}

		return null;
	}

	protected void ClearState() {
		nowState = EntityState.None;
		sanim.timeScale = 1;
	}

	protected void NoneState(Spine.TrackEntry trackEntry) {
		ClearState();
	}

	protected void MoveTo(Vector2 TgtPos) {
		tgtPos = TgtPos;
	}

	protected void DoState(EntityState InState , EntityState InAnimState = EntityState.None) {
		if (InAnimState == EntityState.None) {
			InAnimState = InState;
		}

		if (nowState != InState) {
			nowState = InState;

			switch (InAnimState) {
				case EntityState.Idle:
					sanim.PlayAnimation("idle", 0, true);
					break;

				case EntityState.Walk:
					sanim.PlayAnimation("walk", 0, true);
					break;

				case EntityState.Reborn:
					sanim.PlayAnimation("reborn", 0).Complete += NoneState;
					break;

				case EntityState.Dead:
					sanim.PlayAnimation("dead", 0);
					transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * 2 + 0.1f);

					collider.enabled = false;
					rigidbody.simulated = false;
					hpBar.DeRegist();
					break;

				case EntityState.Atk:
					sanim.timeScale = status.aspdERT;
					DoFace(tgtEnemy.transform.position.x - transform.position.x > 0);
					sanim.state.SetAnimation(0, "atk", false).Complete += delegate {
						ClearState();
						tgtEnemy.DoDamage(status.atkECT, status.arbkECT, status.matkECT);
					};
					break;

				case EntityState.AtkHeavy:
					sanim.timeScale = status.aspdERT;
					DoFace(tgtEnemy.transform.position.x - transform.position.x > 0);
					sanim.state.SetAnimation(0, "atkHeavy", false).Complete += delegate {
						ClearState();
						tgtEnemy.DoDamage(status.atkECT, status.arbkECT, status.matkECT);
					};
					break;

				default:
					break;
			}
		}
	}


	public void DoDamage(float InAtk , float InAp , float InMatk) {
		bool t_isCrt = Random.Range(0f, 100f) <= 15;

		if (t_isCrt) {
			InAtk = InAtk * 1.25f;
			InAp = InAp * 1.25f;
			InMatk = InMatk * 1.25f;
		}

		InAtk = InAtk * Random.Range(0.952f, 1.05f);
		InAp = InAp * Random.Range(0.952f, 1.05f);
		InMatk = InMatk * Random.Range(0.952f, 1.05f);

		float caseDmg = InAtk + InMatk * (1 - status.resistECR);
		float realDmg = 0;
		
		if (nowState != EntityState.Dead) {
			if (status.armorECT > 0) {
				status.armorECT -= InAp;

				if (status.armorECT > 0) {
					if (status.armorECT >= caseDmg) {
						realDmg += caseDmg;
						status.armorECT -= caseDmg;
						caseDmg = 0;
					} else {
						realDmg += status.armorECT;
						caseDmg -= status.armorECT;
						status.armorECT = 0;
					}
				}
			}


			//優化
			if (caseDmg > 0) {
				if (status.hpECT > caseDmg) {
					status.hpECT -= caseDmg;

				} else if (Database.speciesBase[status.specID].cSkills.Contains(1001)) { //計算後已死
					if (Random.Range(0f, 100f) <= 75) {
						status.hpECT = status.hpMCT * 0.05f;
					} else {
						status.hpECT = 0;
						DoState(EntityState.Dead);
					}

				} else {
					status.hpECT = 0;
					DoState(EntityState.Dead);
				}
			}
		}

		realDmg += caseDmg;
		if (realDmg > 0) {
			if (status.armorECT > 0) {
				hpBar.ObsUpdate(status.hpECT / status.hpMCT, status.armorECT / status.armorMCT);

			} else {
				hpBar.ObsUpdate(status.hpECT / status.hpMCT);
			}

			if (t_isCrt) {
				EffectPool.Ins.DoDamage(transform.position, (int)Mathf.Ceil(realDmg), t_isCrt, force);
			}
		}
	}
}

public enum EntityState {
	None,

	//AI State
	Idle,
	Walk,
	Reborn,
	Dead,
	Atk,
	
	//AnimState
	AtkHeavy,
	AtkRange,
	AtkShoot,
	AtkStaff
}
