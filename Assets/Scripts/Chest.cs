using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Destroyble
{
    public AudioClip saw;
    public int gold;
    // Start is called before the first frame update
    void Start()
    {
        gold = Random.Range(900, 4000);
        GetComponent<AudioSource>().playOnAwake = false;
    }

    void Update(){
        DestroyOutsideView();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("sudar broo");
        if(other.tag == "Player")
        {
            other.GetComponent<Player>().AddGold(gold);
            AudioSource.PlayClipAtPoint(saw, transform.position);
            GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
