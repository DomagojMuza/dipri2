using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public float Dmg;

    void Start()
    {
        AudioSource sound = GetComponent<AudioSource>();
        sound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
