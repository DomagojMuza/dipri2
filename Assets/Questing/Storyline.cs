using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Storyline : MonoBehaviour
{

    private static Storyline instance;

    public Story[] storyline;
    private Queue<Story> storyQueue = new Queue<Story>();

    public static Storyline MyInstance {
        get 
        {
            if(instance == null) 
            {
                instance = GameObject.FindObjectOfType<Storyline>();
            }
            return instance;
        } 
        set => instance = value; }

    public void Awake(){
        foreach(Story s in storyline){
            storyQueue.Enqueue(s);
        }
    }

    public void Start(){
        SpawnStory();
    }

    public void SpawnStory(){
        Story s = storyQueue.Dequeue();

        if(s.tag.Contains("Boss")){         
            GameObject spawn = Instantiate(s.npc, new Vector3(0, 0, -450), Quaternion.identity);
            GameObject child = spawn.transform.GetChild(0).gameObject;
            child.transform.name = s.tag;
            NavMeshAgent agent = child.GetComponent<NavMeshAgent>();
            child.GetComponent<EnemyController>().waitTimeSet = 50f/agent.angularSpeed;
            child.transform.localPosition = new Vector3(s.x, 0, s.z);
            child.transform.localPosition = new Vector3(s.x, 0, s.z);
            agent.SetDestination(new Vector3(s.x, 0, s.z));
            child.GetComponent<EnemyController>().target = FindObjectOfType<Player>().transform;
        }else{
            GameObject spawn = Instantiate(s.npc, new Vector3(s.x, 0, s.z), Quaternion.identity);
            spawn.transform.name = s.tag;
        }
        
    }
}

[System.Serializable]
public class Story{
    public GameObject npc;
    public float x, z;

    public string tag;
}
