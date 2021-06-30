using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{ 
    public int HP;

    public GameObject ex;

    public GameObject chest;

    public Animator animator;

    public NavMeshAgent agent;

    private bool dead;

    public Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        HP = 65;
        animator = gameObject.GetComponentInParent<Animator>();
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (this.HP <= 0 && !dead)
        {
            //GameObject firework = Instantiate(ex, gameObject.transform.position + new Vector3(0, 3, 0), Quaternion.identity);
            //firework.GetComponent<ParticleSystem>().startSize = 20;
            //firework.GetComponent<ParticleSystem>().Play();
            //Destroy(firework, 1f);
            gameObject.GetComponent<WaterFloat>().AttachToSurface = false;
            StartCoroutine(death());
        }
    }

    public void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Cannonball")
        {
            this.HP -= other.GetComponent<Cannonball>().Dmg;
            GameObject firework = Instantiate(ex, other.transform.position, Quaternion.identity);
            firework.GetComponent<ParticleSystem>().Play();
            Destroy(firework, 1f);
            Destroy(other.gameObject);
        }
    }

    IEnumerator death()
    {
        dead = true;
        Player.MyInstance.CheckQuest(1, gameObject.name);
        agent.speed = 0f;
        agent.angularSpeed = 0;
        agent.acceleration = 0;
        agent.enabled = false;
        animator.SetBool("isDead", true);
        WaterFloat floater = GetComponent<WaterFloat>();
        floater.AttachToSurface = false;
        yield return new WaitForSeconds(5f);
        Instantiate(chest, gameObject.transform.position, Quaternion.identity);
        spawner.NumberOfEnemys--;
        Destroy(transform.parent.gameObject);



    }
}
