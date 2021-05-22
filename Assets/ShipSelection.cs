using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipSelection : MonoBehaviour
{
    public GameObject[] ship;
    public int selectedShip = 0;

    public void NextShip()
    {
        ship[selectedShip].SetActive(false);
        selectedShip = (selectedShip + 1) % ship.Length;
        ship[selectedShip].SetActive(true);
    }

    public void PreviousShip()
    {
        ship[selectedShip].SetActive(false);
        selectedShip--;
        if(selectedShip < 0)
        {
            selectedShip += ship.Length;
        }
        ship[selectedShip].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedShip", selectedShip);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
