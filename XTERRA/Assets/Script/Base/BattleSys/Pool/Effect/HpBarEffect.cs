using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarEffect : MonoBehaviour {
	public Transform tgt;
	public Image armorBar;
	public Image hpBar;
	public float size;

	public void Regist(Transform InTgt, bool InArmor, float InSize , Color InColor) {
		tgt = InTgt;
		size = InSize;
		armorBar.rectTransform.sizeDelta = new Vector2(armorBar.rectTransform.sizeDelta.x * InSize , armorBar.rectTransform.sizeDelta.y);
		hpBar.rectTransform.sizeDelta = new Vector2(hpBar.rectTransform.sizeDelta.x * InSize, hpBar.rectTransform.sizeDelta.y);
		hpBar.color = InColor;
		ObsUpdate(1 , InArmor ? 1 : 0);
	}

	public void ObsUpdate(float InHpRate , float InArmorRate = 0) {
		bool hurt = (InHpRate < 1 && InHpRate > 0) || (InArmorRate < 1 && InArmorRate > 0);

		if (hurt) {
			hpBar.enabled = true;
			hpBar.fillAmount = InHpRate;
			if (InArmorRate < 1 && InArmorRate > 0) {
				armorBar.enabled = true;
				armorBar.fillAmount = InArmorRate;

			} else {
				armorBar.enabled = false;
			}

		} else {
			hpBar.enabled = false;
		}
	}

	public void DeRegist() {
		tgt = null;
		gameObject.SetActive(false);
	}

	void Update () {
		if (tgt) {
			transform.position = tgt.transform.position + new Vector3( 0 , size * 1.5f, 0);
		}
	}
}
