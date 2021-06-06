using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class shop : MonoBehaviour
{

    public Player MyPlayer;
    WaterBoat WaterBoat;
    public CannonController cannon1;
    public CannonController cannon2;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;
    public Button button7;

    // Start is called before the first frame update
    void Start()
    {
        MyPlayer = FindObjectOfType<Player>();
        CannonController[] topovi = MyPlayer.GetComponentsInChildren<CannonController>();
        cannon1 = topovi[0];
        cannon2 = topovi[1];
        WaterBoat = MyPlayer.GetComponent<WaterBoat>();
        button1.onClick.AddListener(() => BuyDmgSmall());
        button2.onClick.AddListener(() => BuyHPSmall());
        button3.onClick.AddListener(() => AddSpeed());
        button4.onClick.AddListener(() => BuyManeuverability());
        button5.onClick.AddListener(() => BuyHPBig());
        button6.onClick.AddListener(() => AddGoldFromLuttery());
        button7.onClick.AddListener(() => BuyDmgBig());
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void AddSpeed(){
        if(MyPlayer.goldAmount >= 40000){
            MyPlayer.goldAmount -= 40000;
            ScoreManager.instance.ChangeCoinsScore(MyPlayer.goldAmount);
            MyPlayer.GetComponent<WaterBoat>().MaxSpeed += 200;
        }
    }

    void BuyManeuverability()
    {
        if(MyPlayer.goldAmount >= 80000){
            MyPlayer.goldAmount -= 80000;
            ScoreManager.instance.ChangeCoinsScore(MyPlayer.goldAmount);
            MyPlayer.GetComponent<WaterBoat>().SteerPower += 200;
        }
    }

    void BuyDmgSmall()
    {
        
        if(MyPlayer.goldAmount >= 10000){
            MyPlayer.goldAmount -= 10000;
            ScoreManager.instance.ChangeCoinsScore(MyPlayer.goldAmount);
            cannon1.cannonballDmg +=5;
            cannon2.cannonballDmg = cannon1.cannonballDmg;
        }
        
    }

    void BuyDmgBig()
    {
        
        if(MyPlayer.goldAmount >= 20000){
            MyPlayer.goldAmount -= 20000;
            ScoreManager.instance.ChangeCoinsScore(MyPlayer.goldAmount);
            cannon1.cannonballDmg +=12;
            cannon2.cannonballDmg = cannon1.cannonballDmg;
        }
        
    }

    void BuyHPSmall()
    {
        
        if(MyPlayer.goldAmount >= 5000){
            MyPlayer.goldAmount -= 5000;
            ScoreManager.instance.ChangeCoinsScore(MyPlayer.goldAmount);
            MyPlayer.HP +=5;
        }
        
    }

    void BuyHPBig()
    {
        
        if(MyPlayer.goldAmount >= 10000){
            MyPlayer.goldAmount -= 10000;
            ScoreManager.instance.ChangeCoinsScore(MyPlayer.goldAmount);
            MyPlayer.HP +=12;
        }
        
    }


    void AddGoldFromLuttery()
    {
        int gold = 0;
        if(MyPlayer.goldAmount >= 20000){
            MyPlayer.goldAmount -= 20000;
            gold = Random.Range(10000, 50000);
            if(gold >= 25000){
                gold = Random.Range(20000, 50000);
                if(gold>=40000){
                    gold = Random.Range(35000, 50000);
                }
            }
            MyPlayer.goldAmount += gold;
            ScoreManager.instance.ChangeCoinsScore(MyPlayer.goldAmount);
        }
        
    }
}
