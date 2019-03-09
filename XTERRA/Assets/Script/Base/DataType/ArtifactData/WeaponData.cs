using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : BaseType {
	public WeaponType weaponType;
	public HandType handType;

	public float size = 1;
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

	public void SetWeaponType(string InValue) {
		switch (InValue) {
			case "SW":
				weaponType = WeaponType.SW;
				handType = HandType.One;
				break;
			case "AX":
				weaponType = WeaponType.AX;
				handType = HandType.One;
				break;
			case "HM":
				weaponType = WeaponType.HM;
				handType = HandType.One;
				break;
			case "SP":
				weaponType = WeaponType.SP;
				handType = HandType.One;
				break;


			case "LS":
				weaponType = WeaponType.LS;
				handType = HandType.Shield;
				break;
			case "MS":
				weaponType = WeaponType.MS;
				handType = HandType.Shield;
				break;
			case "HS":
				weaponType = WeaponType.HS;
				handType = HandType.Shield;
				break;


			case "GSW":
				weaponType = WeaponType.GSW;
				handType = HandType.Two;
				break;
			case "GAX":
				weaponType = WeaponType.GAX;
				handType = HandType.Two;
				break;
			case "GHM":
				weaponType = WeaponType.GHM;
				handType = HandType.Two;
				break;
			case "GSP":
				weaponType = WeaponType.GSP;
				handType = HandType.Two;
				break;

			default:
				weaponType = WeaponType.Error;
				handType = HandType.None;
				break;
		}
	}
}

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
}

public enum HandType {
	None,
	One,        //單持
	Two,        //雙手
	Shield,     //盾
}

