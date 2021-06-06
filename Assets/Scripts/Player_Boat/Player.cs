using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public void Awake()
    {
        HP = 100;
        cannons = gameObject.GetComponentsInChildren<CannonController>();
    }

    private CannonController[] cannons;
    public int goldAmount;

    public int HP;

    private static Player instance;

    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
        set => instance = value;
    }

    public int MyGoldAmount
    {
        get => goldAmount;
        set => goldAmount = value;

    }

    public void AddGold(int goldAmount)
    {
        MyGoldAmount += goldAmount;
        ScoreManager.instance.ChangeCoinsScore(MyGoldAmount);
        CheckQuest(goldAmount, "Gold");
        
    }

    public void CheckQuest(int addAmount, string typeOfObject)
    {
        Debug.Log(addAmount + " "+ typeOfObject);
        foreach (Quest quest in QuestLog.MyInstance.quests)
        {
            if (quest.MyComplete == false)
            {
                foreach (Objective obj in quest.MyCollectObjectives)
                {
                    if (obj.MyType == typeOfObject && !obj.MyComplete)
                    {
                        obj.MyCurrentAmount += addAmount;
                        obj.Evaluate();
                    }
                }
                quest.Evaluate();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Cannonball")
        {
            HP -= other.GetComponent<Cannonball>().Dmg;
        }
    }

    public void EnableCannons(){
        foreach (CannonController cannon in cannons)
        {
            cannon.enabled = true;
        }
    }

    public void DisableCannons(){
        foreach (CannonController cannon in cannons)
        {
            cannon.enabled = false;
        }
    }
}
