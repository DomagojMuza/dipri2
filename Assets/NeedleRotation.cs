using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NeedleRotation : MonoBehaviour
{
    private Player instance;
    private Transform PlayerTransform;
    private RectTransform rectTransform;
    void Start()
    {
        instance = GameObject.FindObjectOfType<Player>();
        PlayerTransform = instance.transform;
        Debug.Log(instance);
        rectTransform = GetComponent<RectTransform>();

    }

    void LateUpdate()
    {
        float xRotation = PlayerTransform.localRotation.eulerAngles.y;
        rectTransform.eulerAngles = new Vector3(0, 0, xRotation);
    }
}
