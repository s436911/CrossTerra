using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageEffet : MonoBehaviour {
	public TextMeshProUGUI tmp;

	private Vector3 targetPos;
	private float destoryTime;

	public void DoDamage(Vector2 InPos, int InDamage , bool InCrt, short InForce ) {
		tmp.text = InDamage.ToString();
		targetPos = InPos + new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)) * 15f;
		transform.position = InPos;
		destoryTime = Time.time;

		if (InForce == 0) {
			tmp.color = new Color(0.8f , 0.2f , 0.2f);
		}
	}

	void Update() {
		if (destoryTime == 0) {
			return;
		}			

		if (Time.time - destoryTime <= 1f) {
			Vector3 direction = transform.position ;
			direction.y = 0f;
			transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime);
		} else {
			Destroy(gameObject);
		}
	}
}