using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EntityBase))]
public class EntityBaseED : Editor {
	EntityBase editing;

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		if (EditorApplication.isPlaying) {
			editing = (EntityBase)target;

			EditorTools.EnumField(editing.nowState, "AI狀態");
			EditorTools.EnumField(editing.status.armState, "持有狀態");

			if (editing.status.primeWeapon) {
				EditorTools.LabelField("主手");
				EditorTools.EnumField(editing.status.primeWeapon.weaponType, "武器類型");
				EditorTools.EnumField(editing.status.primeWeapon.handType, "持有類型");
			}

			if (editing.status.subWeapon) {
				EditorTools.LabelField("副手");
				EditorTools.EnumField(editing.status.subWeapon.weaponType, "武器類型");
				EditorTools.EnumField(editing.status.subWeapon.handType, "持有類型");
			}

			EditorTools.FloatField(editing.status.atkECT, "物傷");
			EditorTools.FloatField(editing.status.matkECT, "法傷");
			EditorTools.FloatField(editing.status.arbkECT, "破甲");
			EditorTools.FloatField(editing.status.armorECT, "裝甲");
			EditorTools.FloatField(editing.status.hpECT, "體力");

			EditorTools.FloatField(editing.status.resistECR, "抗性");
			EditorTools.FloatField(editing.status.accECR, "命中");
			EditorTools.FloatField(editing.status.avdECR, "迴避");

			EditorTools.FloatField(editing.status.aspdERT, "攻速");
			EditorTools.FloatField(editing.status.spdERT, "移速");
		}
	}
}


