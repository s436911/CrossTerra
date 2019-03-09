using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Species : BaseType {
	public float size = 1;
	public string model;
	public string skin;

	public float atkCT;
	public float arbkCT;
	public float matkCT;
	public float armorCT;
	public float hpCT;

	public float resistCR;
	public float accCR;
	public float avdCR;

	public float spdRT = 1;
	public float aspdRT = 1;

	public List<int> cSkills = new List<int>();

	public override void ImportReset() {
		cSkills = new List<int>();
	}
}
