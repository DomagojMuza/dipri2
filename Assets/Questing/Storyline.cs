using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameObject spawn = Instantiate(s.npc, new Vector3(s.x, 0, s.z), Quaternion.identity);
        spawn.transform.name = s.tag;
    }
}

[System.Serializable]
public class Story{
    public GameObject npc;
    public float x, z;

    public string tag;
}
