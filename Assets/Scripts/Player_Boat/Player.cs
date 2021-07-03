using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    

    private CannonController[] cannons;
    public int goldAmount;

    public Image image;

    int check = 0;

    float translateposition=0;
    float scale = 0f;

    public float HP;

    private static Player instance;

    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
        set => instance = value;
    }

    public int MyGoldAmount
    {
        get => goldAmount;
        set => goldAmount = value;

    }

    public void Awake()
    {
        HP = 100;
        cannons = gameObject.GetComponentsInChildren<CannonController>();
        hpBar hp = FindObjectOfType<hpBar>();
        image = hp.GetComponent<Image>();
    }

    public void AddGold(int goldAmount)
    {
        MyGoldAmount += goldAmount;
        ScoreManager.instance.ChangeCoinsScore(MyGoldAmount);
        CheckQuest(goldAmount, "Gold");
        
    }

    public void CheckQuest(int addAmount, string typeOfObject)
    {
        
        foreach (Quest quest in QuestLog.MyInstance.quests)
        {
            if (quest.MyComplete == false)
            {
                foreach (Objective obj in quest.MyCollectObjectives)
                {
                    Debug.Log(obj.MyType + " "+ typeOfObject);
                    if (obj.MyType == typeOfObject && !obj.MyComplete)
                    {
                        obj.MyCurrentAmount += addAmount;
                        obj.Evaluate();
                    }
                }
                quest.Evaluate();
            }
        }
    }

    public void addHP(int value){
        float width = 800f;
        float dmg = value / 100f;
        float translate = width*dmg/2;
        float HP_over = HP - 100;
        
        image.transform.localScale += new Vector3(dmg, 0.0f, 0.0f);
        image.transform.position += new Vector3(translate, 0.0f, 0.0f);
        if(HP_over > 0){
            HP = 100;
            image.transform.localScale -= new Vector3(HP_over/100f, 0.0f, 0.0f);
            image.transform.position -= new Vector3(width*(HP_over/100f)/2, 0.0f, 0.0f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Cannonball")
        {   
            
            if(check == 1){
                return;
            }
            float damage = other.GetComponent<Cannonball>().Dmg;
            float width = 800f;
            float dmg = damage / 100f;
            float translate = width*dmg/2;
            
            scale += dmg;
            translateposition = translateposition+translate;
            Debug.Log(translateposition);
            HP -= other.GetComponent<Cannonball>().Dmg;
            if(HP > 0){
                image.transform.localScale -= new Vector3(dmg, 0.0f, 0.0f);
                image.transform.position -= new Vector3(translate, 0.0f, 0.0f);
            }
            else{
                Debug.Log("Uso sam u else kad umrem");
                HP=100;
                scale -= dmg;
                translateposition = translateposition-translate;
                image.transform.localScale += new Vector3(scale, 0.0f, 0.0f);
                image.transform.position += new Vector3(translateposition, 0.0f, 0.0f);
                check = 1;
                
                string sceneToLoad = "GameOver";
    
    
    	        SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    public void EnableCannons(){
        foreach (CannonController cannon in cannons)
        {
            cannon.enabled = true;
        }
    }

    public void DisableCannons(){
        foreach (CannonController cannon in cannons)
        {
            cannon.enabled = false;
        }
    }
}
