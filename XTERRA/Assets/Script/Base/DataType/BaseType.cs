using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseType : ScriptableObject {
	public int sID;
	public string sName;

	public virtual void ImportReset() {

	}

	public virtual void Mirror(BaseType value) {
		this.sID = value.sID;
		this.sName = value.sName;
	}
}
