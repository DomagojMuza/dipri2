using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiverDialogTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Dialog questDialog;

    public Dialog normalDialog;

    public Sprite npcImage;

    public Quest[] quests;
    public void TriggerDialog()
    {
        if(quests == null)
        {
            FindObjectOfType<DialogManager>().StartDialog(normalDialog, npcImage);
            return;
        }
        FindObjectOfType<DialogManager>().StartDialog(questDialog, npcImage, quests);
        quests = null;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggero se");
        if(other.tag == "Player")
        {
            Debug.Log("Player triggero");
            TriggerDialog();
        }
    }
}
