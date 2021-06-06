using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReward : MonoBehaviour
{
    public delegate void TestDelegate(int s);
    static void M(string s)
    {
    }
    public static QuestReward instance;
    public Dictionary<string, TestDelegate> questReward = new Dictionary<string, TestDelegate>();

    public static QuestReward MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<QuestReward>();
            }
            return instance;
        }
        set => instance = value;
    }

    public void Start()
    {
        

        questReward.Add("gold", (x) => { Player.MyInstance.goldAmount += x; });
        questReward.Add("hp", (x) => { Player.MyInstance.HP += x; });
        questReward.Add("dmg", (x) => {
            Player player = FindObjectOfType<Player>();
            CannonController[] controllers = player.GetComponentsInChildren<CannonController>();
            foreach(CannonController controller in controllers)
            {
                controller.cannonballDmg += x;
            }
        });
        questReward.Add("rtf", (x) => { Debug.Log(x); });

        questReward["rtf"].DynamicInvoke(5);


    }

    
   
}
