using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiverDialogTrigger : Destroyble
{
    // Start is called before the first frame update
    public Dialog questDialog;

    public Dialog normalDialog;

    public bool questRelated;

    public Sprite npcImage;

    bool canBeDestroyed = false;

    public Quest[] quests;
    public void TriggerDialog()
    {

        if(quests != null)
        {
            FindObjectOfType<DialogManager>().StartDialog(questDialog, npcImage, quests);
            quests = null;
            canBeDestroyed = true;
            return;
        }
        if(normalDialog != null)
        {
            FindObjectOfType<DialogManager>().StartDialog(normalDialog, npcImage);
            return;
        }
        return;
        
    }

    void Update(){
        if(canBeDestroyed){
            DestroyOutsideView();
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(questRelated)
            {
            Player.MyInstance.CheckQuest(1, gameObject.name);
            questRelated = false;
            }   
            TriggerDialog(); 
        }        
    }
}
