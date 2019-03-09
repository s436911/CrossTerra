using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour {

	public static EffectPool Ins;
	public GameObject damageEffect;
	public GameObject damageCriEffect;
	public GameObject avoidEffect;
	public GameObject hpEffect;

	public Color hpEffectA;
	public Color hpEffectE;

	void Awake () {
		Ins = this;
	}

	public HpBarEffect RegistHpBar(Transform InTgt, bool InArmor, float InSize , short InForce) {
		GameObject lastCreate = Instantiate(hpEffect, transform);
		HpBarEffect lastHpBar = lastCreate.GetComponent<HpBarEffect>();
		lastHpBar.Regist(InTgt , InArmor, InSize , InForce == 0 ? hpEffectA : hpEffectE);
		return lastHpBar;
	}

	public void DoDamage(Vector3 InPos, int InDamage, bool InCrt, short InForce) {
		GameObject lastCreate = Instantiate(InCrt ? damageCriEffect : damageEffect, transform);
		DamageEffet lastDamage = lastCreate.GetComponent<DamageEffet>();
		lastDamage.DoDamage(InPos, InDamage, InCrt, InForce);
	}
}
