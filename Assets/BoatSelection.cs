using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatSelection : MonoBehaviour
{
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    private int currentBoat;

    private void Awake()
    {
        SelectBoat(0);
    }
    private void SelectBoat(int _index)
    {
        previousButton.interactable = (_index != 0);
        nextButton.interactable = (_index != transform.childCount - 1);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }
        
    }

    public void ChangeBoat(int _change)
    {
        currentBoat += _change;
        SelectBoat(currentBoat);
    }
}
