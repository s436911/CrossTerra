using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Main_PlayerManager : MonoBehaviour {
    public static Main_PlayerManager direct; 
    public Text foodText;
    public Text moneyText;
    public Text lifeText;

	public int foodMax = 30;
	public int foodNow = 30;
	public int money = 1000;
	public int lifeMax = 10;
	public int lifeNow = 10;

    // Use this for initialization
    void Start () {
        direct = this;
        ShowInfomation();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowInfomation() {
        ShowFood();
        ShowMoney();
        ShowLife();
    }

    public void ShowFood() {
        foodText.text = foodNow + " / " + foodMax;
    }

    public void ShowLife() {
        lifeText.text = lifeNow + " / " + lifeMax;
    }

    public void ShowMoney() {
        moneyText.text = money.ToString();
    }

    public void Eat(int num) {
        foodNow -= num;
        if (foodNow <= 0) {
            foodNow = 0;

            //******************測試
            Application.LoadLevel("S02_Menu");
        }
        ShowFood();
    }

    public void Supply(int num) {
        foodNow += num;
        if (foodNow > foodMax) {
            foodNow = foodMax;            
        }
        ShowFood();
    }

    public void Use(int num) {
        foodNow -= num;
        if (foodNow < 0) {
            foodNow = 0;
        }
        ShowFood();
    }

    public void Recover(int num) {
        lifeNow += num;
        if (lifeNow > lifeMax) {
            lifeNow = lifeMax;
        }
        ShowLife();
    }

    public void Damage(int num) {
        lifeNow -= num;
        if (lifeNow < 0) {
            lifeNow = 0;
        }
        ShowLife();
    }
}
