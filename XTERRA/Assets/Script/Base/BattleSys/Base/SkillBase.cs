using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase  {
	public int sID;
	public string sName;
	public bool constSkill = true;

	public SkillBase(int InID , string InName) {
		sID = InID;
		sName = InName;
	}
}
