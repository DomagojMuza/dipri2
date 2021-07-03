using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
  public void OnTriggerEnter(Collider other)
    {
       
        if(other.tag == "Player")
        {
           Player.MyInstance.CheckQuest(1, gameObject.name);
           Destroy(gameObject);
        }
    }
}
