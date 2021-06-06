using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAfterQuest : MonoBehaviour
{


    public static QuestAfterQuest instance;

    public Quest[] QuestsAfterQuest;

    public static QuestAfterQuest MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<QuestAfterQuest>();
            }
            return instance;
        }
        set => instance = value;
    }

   public void AddQuestAfterQuest(int x){
       StartCoroutine(Accept(x));
   }

   IEnumerator Accept(int x){
       yield return new WaitForSeconds(1f);
       QuestLog.MyInstance.AceptQuest(QuestsAfterQuest[x]);
   }


}
