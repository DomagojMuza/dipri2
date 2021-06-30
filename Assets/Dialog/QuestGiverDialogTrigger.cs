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

    public bool open = false;

    public Quest[] quests;
    public void TriggerDialog()
    {

        if(quests != null)
        {
            FindObjectOfType<DialogManager>().trigger = this;
            FindObjectOfType<DialogManager>().StartDialog(questDialog, npcImage, quests);
            quests = null;
            canBeDestroyed = true;
            return;
        }
        if(normalDialog.recenice.Length>= 1)
        {
            FindObjectOfType<DialogManager>().trigger = this;
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


    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && !open){
            InteractText();
        }
        if(other.tag == "Player" && Input.GetKeyDown(KeyCode.F) && !open)
        {
            if(questRelated)
            {
            Player.MyInstance.CheckQuest(1, gameObject.name);
            questRelated = false;
            } 
            open = true; 
            GameObject text = GameObject.FindGameObjectWithTag("InteractText");
            text.GetComponent<CanvasGroup>().alpha = 0;
            TriggerDialog();
  
        }        
    }
    void OnTriggerExit(){
        GameObject text = GameObject.FindGameObjectWithTag("InteractText");
        text.GetComponent<CanvasGroup>().alpha = 0;
    }

    private void InteractText(){
        GameObject text = GameObject.FindGameObjectWithTag("InteractText");
        text.GetComponent<CanvasGroup>().alpha = 1;
    }
}
