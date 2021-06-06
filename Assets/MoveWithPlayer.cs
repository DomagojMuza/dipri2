using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveWithPlayer : MonoBehaviour
{
    private Player instance;
    private Transform PlayerTransform;

    void Start()
    {
        instance = GameObject.FindObjectOfType<Player>();
        PlayerTransform = instance.transform;
        Debug.Log(instance);
    }

    void LateUpdate()
    {
        if (PlayerTransform != null)
        {
            Vector3 newPosition = PlayerTransform.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;
        }
    }
}
