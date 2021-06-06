using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questParent;

    [SerializeField]
    public Camera cam;

    [SerializeField]
    private CanvasGroup canvasGroup;

    public Animator animator;

    private static QuestLog instance;

    public List<Quest> quests;

    private Quest selected;

    [SerializeField]
    private Text questDescription;

    public static QuestLog MyInstance {
        get 
        {
            if(instance == null) 
            {
                instance = GameObject.FindObjectOfType<QuestLog>();
            }
            return instance;
        } 
        set => instance = value; }

    
    public void AceptQuest(Quest quest)
    {
        quests.Add(quest);
        quest.QuestEvaluate();
        GameObject go = Instantiate(questPrefab, questParent);

        QuestScript qs = go.GetComponent<QuestScript>();
        quest.MyQuestScript = qs;
        qs.MyQuest = quest;

        go.GetComponent<Text>().text = quest.MyTitle;
        
    }

    public void ShowDescription(Quest quest)
    {
        if(selected != null)
        {
            selected.MyQuestScript.DeSelect();
        }

        string objectives = "\nObjectives\n";

        selected = quest;


        foreach( Objective obj in quest.MyCollectObjectives)
        {
            objectives += obj.MyType + ": " + obj.MyCurrentAmount +
                "/" + obj.MyAmount + "\n" + "Complete:" + obj.MyComplete + "\n\n"; 
        }

        questDescription.text = string.Format("{0} <size=16>{1}</size>", 
            quest.MyDescription, objectives);

    }

    public void OpenClose()
    {
        if(canvasGroup.alpha == 1)
        {
            Close();
        }
        else
        {
            animator.SetBool("IsOpen", true);
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void Close()
    {
        animator.SetBool("IsOpen", false);
        questDescription.text = "";
        selected.MyQuestScript.DeSelect();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            OpenClose();
        }
    }
}
