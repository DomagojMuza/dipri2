using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [SerializeField]
    private string title;

    [SerializeField]
    private string description;

    [SerializeField]
    private CollectObjective[] collectObjectives;

    public QuestScript MyQuestScript { get; set; }

    private bool complete;

    public string MyTitle
    {
        get { return title; }
        set { title = value; }
    }

    public string MyDescription { get => description; set => description = value; }
    public CollectObjective[] MyCollectObjectives { get => collectObjectives; set => collectObjectives = value; }
    public bool MyComplete { get => complete; set => complete = value; }

    public void Evaluate()
    {
        foreach(Objective obj in MyCollectObjectives)
        {
            if (!obj.MyComplete)
            {
                return;
            }
         
        }
        MyComplete = true;

        //QuestReward.MyInstance.openWith["rft"].DynamicInvoke(2);
        Debug.Log(MyTitle + " Complete");
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}

[System.Serializable]
public abstract class Objective
{
    [SerializeField]
    private int amount;

    private int currentAmount;

    [SerializeField]
    private string type;

    private bool complete = false;


    public int MyAmount { get => amount; }
    public string MyType { get => type; set => type = value; }
    public int MyCurrentAmount { get => currentAmount; set => currentAmount = value; }


    public bool MyComplete { get => complete; set => complete = value; }

    public void Evaluate()
    {
        if (MyCurrentAmount >= MyAmount && !MyComplete)
        {
            MyCurrentAmount = MyAmount;
            MyComplete = true;
        }
    }
}

[System.Serializable]
public class CollectObjective : Objective
{
    public void UpdateItemCount(int goldAdded)
    {
        Player.MyInstance.MyGoldAmount += goldAdded;
    }
}
