using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public Text dialogText;

    public Image npcImage;

    private Queue<string> recenice;

    public Animator animator;

    private Quest[] Quests;

    public QuestGiverDialogTrigger trigger;


    void Start()
    {
        recenice = new Queue<string>();
    }

    public void StartDialog(Dialog dialog, Sprite npc, Quest[] quests = null)
    {
        Player.MyInstance.DisableCannons();
        Debug.Log(npc);
        Quests = quests;
        animator.SetBool("IsOpen", true);


        npcImage.sprite = npc;
        recenice.Clear();

        foreach(string recenica in dialog.recenice)
        {
            recenice.Enqueue(recenica);
        }
        DisplayNextSentance();
    }

    public void DisplayNextSentance()
    {
        if(recenice.Count == 0)
        {
            trigger.open = false;
            trigger = null;
            EndDialog();
            return;
        }

        string recenica = recenice.Dequeue();
        dialogText.text = recenica;
    }

    public void EndDialog()
    {
       if(Quests != null)
        {
            foreach(Quest q in Quests)
            {
                QuestLog.MyInstance.AceptQuest(q);
            }
       }
        Quests = null;
        animator.SetBool("IsOpen", false);
        Player.MyInstance.EnableCannons();
    }

   
}
