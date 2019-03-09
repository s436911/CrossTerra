using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatus {
	public ArmState armState;
	public WeaponData primeWeapon;
	public WeaponData subWeapon;
	public int specID;

	public float atkECT;
	public float arbkECT;
	public float matkECT;

	public float armorECT;
	public float hpECT;
	public float armorMCT;
	public float hpMCT;

	public float resistECR;
	public float accECR;
	public float avdECR;

	public float spdERT = 1;
	public float aspdERT = 1;
	
	public EntityStatus (){

	}

	public EntityStatus(EntityStatus InStaus) {
		specID		= InStaus.specID;
		armState	= InStaus.armState;

		atkECT		= InStaus.atkECT;
		arbkECT		= InStaus.arbkECT;
		matkECT		= InStaus.matkECT;
		armorECT	= InStaus.armorECT;
		hpECT		= InStaus.hpECT;
		armorMCT	= InStaus.armorMCT;
		hpMCT		= InStaus.hpMCT;

		resistECR	= InStaus.resistECR;
		accECR		= InStaus.accECR;
		avdECR		= InStaus.avdECR;

		spdERT		= InStaus.spdERT;
		aspdERT		= InStaus.aspdERT;

		//優化
		primeWeapon = InStaus.primeWeapon;
		subWeapon = InStaus.subWeapon;

		AddSpeciesStatus(Database.speciesBase[specID]);

		if (primeWeapon) {
			AddWeaponStatus(primeWeapon);
		}
		if (subWeapon) {
			AddWeaponStatus(subWeapon);
		}

		InitArmState();
		InitDaySkill();
		
		StatusNormalize();
	}

	public void SetWeapon(WeaponData InWeapon) {
		//狀態異動整理 優化
		if (InWeapon.handType == HandType.One) {
			primeWeapon = InWeapon;

		} else if (InWeapon.handType == HandType.Two) {
			primeWeapon = InWeapon;

		} else if (InWeapon.handType == HandType.Shield) {
			subWeapon = InWeapon;

		} else {
			return;
		}
	}

	public void InitDaySkill() {
		//stub//特持有模式計算插入
		if (!BattleSys.Ins.t_inDay) {
			if (Database.speciesBase[specID].cSkills.Contains(2002)) {
				aspdERT = aspdERT + 0.2f;
				spdERT = spdERT + 0.2f;

			} else if (Database.speciesBase[specID].cSkills.Contains(2001)) {
				aspdERT = aspdERT + 0.1f;
				spdERT = spdERT + 0.1f;
			}
		} else {
			if (Database.speciesBase[specID].cSkills.Contains(2002)) {
				aspdERT = aspdERT - 0.2f;
				spdERT = spdERT - 0.2f;

			} else if (Database.speciesBase[specID].cSkills.Contains(2001)) {
				aspdERT = aspdERT - 0.1f;
				spdERT = spdERT - 0.1f;
			}
		}
	}

	public void InitArmState() {
		//stub//特持有模式計算插入
		if (primeWeapon == null) {
			armState = ArmState.None;

		} else if (primeWeapon.handType == HandType.One && subWeapon == null) {
			armState = ArmState.One;

		} else if (primeWeapon.handType == HandType.One && subWeapon.handType == HandType.Shield) {
			armState = ArmState.Shield;

		} else if (primeWeapon.handType == HandType.One && subWeapon.handType == HandType.One) {
			armState = ArmState.Dual;

		} else if (primeWeapon.handType == HandType.Two && Database.speciesBase[specID].cSkills.Contains(1)) {//巨人stub
			armState = ArmState.TwoInOne;
			aspdERT = aspdERT - 0.25f;

		} else if (primeWeapon.handType == HandType.Two && Database.speciesBase[specID].cSkills.Contains(2)) {//巨人stub
			armState = ArmState.TwoInOne;
			aspdERT = aspdERT - 0.4f;

		} else if (primeWeapon.handType == HandType.Two) {
			armState = ArmState.Two;

		}
	}

	public void AddSpeciesStatus(Species InData) {

		atkECT = atkECT + InData.atkCT;
		arbkECT = arbkECT + InData.arbkCT;
		matkECT = matkECT + InData.matkCT;
		armorECT = armorECT + InData.armorCT;
		hpECT = hpECT + InData.hpCT;
		armorMCT = armorMCT + InData.armorCT;
		hpMCT = hpMCT + InData.hpCT;

		resistECR = resistECR + InData.resistCR;
		accECR = accECR + InData.accCR;
		avdECR = avdECR + InData.avdCR;

		aspdERT = aspdERT + InData.aspdRT;
		spdERT = spdERT + InData.spdRT;
	}

	public void AddWeaponStatus(WeaponData InData) {
				
		atkECT = atkECT + InData.atkCT;
		arbkECT = arbkECT + InData.arbkCT;
		matkECT = matkECT + InData.matkCT;
		armorECT = armorECT + InData.armorCT;
		hpECT = hpECT + InData.hpCT;
		armorMCT = armorMCT + InData.armorCT;
		hpMCT = hpMCT + InData.hpCT;

		resistECR = resistECR + InData.resistCR;
		accECR = accECR + InData.accCR;
		avdECR = avdECR + InData.avdCR;

		aspdERT = aspdERT + InData.aspdRT;
		spdERT = spdERT + InData.spdRT;
	}

	public void StatusNormalize() {
		aspdERT = Mathf.Clamp(aspdERT , 0 , 5);
		spdERT = Mathf.Clamp(spdERT, 0, 2);
	}
}

public enum ArmState {
	None,
	One,        //單持
	Two,        //雙手
	Dual,       //雙劍
	Shield,     //盾劍
	TwoInOne,   //雙手單持 優化?
}

/*
public enum WeaponType {
	Error,
	SW,
	AX,
	HM,
	SP,

	LS,
	MS,
	HS,

	GSW,
	GAX,
	GHM,
	GSP
}*/
