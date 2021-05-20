using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    public Text text;

    int coins;
    // Start is called before the first frame update
    void Start()
    {
        text.text = Player.MyInstance.MyGoldAmount.ToString();
        if(instance == null)
        {
            instance = this;
        }
        
    }

    public void ChangeCoinsScore(int score)
    {
        text.text = score.ToString();
    }
}
