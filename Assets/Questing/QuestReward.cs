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
    public Dictionary<string, TestDelegate> openWith = new Dictionary<string, TestDelegate>();

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
        

        openWith.Add("gold", (x) => { Player.MyInstance.goldAmount += x; });
        openWith.Add("hp", (x) => { Player.MyInstance.HP += x; });
        openWith.Add("dmg", (x) => {
            Player player = FindObjectOfType<Player>();
            CannonController[] controllers = player.GetComponentsInChildren<CannonController>();
            foreach(CannonController controller in controllers)
            {
                controller.cannonballDmg += x;
            }
        });
        openWith.Add("rtf", (x) => { Debug.Log(x); });

        openWith["rtf"].DynamicInvoke(5);


    }

    
   
}
