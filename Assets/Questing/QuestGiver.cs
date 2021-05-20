using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    public Quest[] quests;


    //Debugging
    [SerializeField]
    private QuestLog tmpLog;


    private void Awake()
    {
        
    }
}
