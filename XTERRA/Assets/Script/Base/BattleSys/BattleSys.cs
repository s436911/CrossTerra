using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSys : MonoBehaviour {
	public static BattleSys Ins;
	public bool t_inDay = true;

	[Header("Battle - Core")]
	public Transform entitySet;


	public void Awake() {
		Ins = this;
	}
}
