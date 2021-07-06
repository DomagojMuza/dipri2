using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Merchant : MonoBehaviour
{

    NavMeshAgent agent;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public System.DateTime startTime;


void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
      Patroling();
    }

    void Patroling()
    {

        if (!walkPointSet) SetWalkPoint();
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToGo = transform.position - walkPoint;

        if (distanceToGo.magnitude < 1f)
        {
            walkPointSet = false;
        }
        
        System.TimeSpan ts = System.DateTime.UtcNow - startTime;
        if(walkPointSet && ts.Seconds>=20){
            SetWalkPoint();
        }
    }

    void SetWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Debug.Log(Physics.Raycast(walkPoint + new Vector3(0, 4, 0), -transform.up, 20f));

        if(Physics.Raycast(walkPoint + new Vector3(0, 4, 0), -transform.up, 20f))
        {
            walkPointSet = true;
            startTime = System.DateTime.UtcNow;
        }
    }
}
