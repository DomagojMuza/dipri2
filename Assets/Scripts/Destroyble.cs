using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyble : MonoBehaviour
{
    public bool destroy;
    public void DestroyOutsideView(){

        if(destroy){
            double distance = Vector3.Distance(FindObjectOfType<Player>().transform.position, transform.position);
            if(distance>250){
                Destroy(gameObject);
            }
        }
        
    }
}
