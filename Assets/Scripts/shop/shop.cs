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
        
        if(MyPlayer.goldAmount >= 8000){
            MyPlayer.goldAmount -= 8000;
            ScoreManager.instance.ChangeCoinsScore(MyPlayer.goldAmount);
            cannon1.cannonballDmg +=2;
            cannon2.cannonballDmg = cannon1.cannonballDmg;
            MyPlayer.dmgLVL+= 0.5f;
        }
        
    }

    void BuyDmgBig()
    {
        
        if(MyPlayer.goldAmount >= 16000){
            MyPlayer.goldAmount -= 16000;
            ScoreManager.instance.ChangeCoinsScore(MyPlayer.goldAmount);
            cannon1.cannonballDmg +=5;
            cannon2.cannonballDmg = cannon1.cannonballDmg;
            MyPlayer.dmgLVL+= 1f;
        }
        
    }

    void BuyHPSmall()
    {
        if(MyPlayer.HP < 100){
            if(MyPlayer.goldAmount >= 3500){
                MyPlayer.goldAmount -= 3500;
                ScoreManager.instance.ChangeCoinsScore(MyPlayer.goldAmount);
                MyPlayer.HP +=10;
                MyPlayer.addHP(10);
            }
        }    
        else{
            Debug.Log("Your HP is already full");
            MyPlayer.HP = 100;
        }    
    }

    void BuyHPBig()
    {
        if(MyPlayer.HP < 100){
            if(MyPlayer.goldAmount >= 6000){
                MyPlayer.goldAmount -= 6000;
                ScoreManager.instance.ChangeCoinsScore(MyPlayer.goldAmount);
                MyPlayer.HP +=17;
                MyPlayer.addHP(17);
            }
        }    
        else{
            Debug.Log("Your HP is already full");
            MyPlayer.HP = 100;
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
