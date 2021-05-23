using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadShip : MonoBehaviour
{
    public GameObject[] shipPrefabs;
    public Transform spawnPoint;

    private void Awake()
    {
        int selectedShip = PlayerPrefs.GetInt("selectedShip");
        GameObject prefab = shipPrefabs[selectedShip];
        GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
    
}
