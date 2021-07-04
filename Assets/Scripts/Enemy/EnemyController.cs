using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;

    public Transform target;
    NavMeshAgent agent;
    bool orbit = false;

    public float Damage = 5;

    public LayerMask whatIsWater;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public Transform leftCannons;
    public Transform rightCannons;
    private Transform[] currentShootPoints;

    bool isAttacking = false;
    bool isReloading = false;
    bool looked = false;
    bool initAttack = false;

    public System.DateTime startTime;

    float waitTime = 0f;
    public float waitTimeSet;

    public float BlastPower = 100f;
    public GameObject Cannonball;

    private Vector3 sideToTurn;

    Enemy enemy;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        Damage = FindObjectOfType<Player>().HP * Mathf.Sqrt(FindObjectOfType<Player>().dmgLVL);

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);


        if (distance > 130 && !orbit && enemy.HP > 0) Patroling();
        if (distance <= 130 && distance > (lookRadius + 2) && !orbit && enemy.HP > 0) Chase();
        if (distance <= lookRadius && enemy.HP > 0) 
        {
            Attack();
        }
        else
        {
            orbit = false;
        }
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

    void Chase()
    {
        orbit = false;
        looked = false;
        agent.SetDestination(target.position);
    }

    void Attack()
    {
        WhatSide();
        orbit = true;
        var offsetPlayer = target.transform.position - transform.position;
        var dir = Vector3.Cross(offsetPlayer, sideToTurn);
        agent.SetDestination(transform.position + dir);
        if(!initAttack){
            waitTime = waitTimeSet;
        }
        else{
            waitTime = 0;
        }
        StartCoroutine(ShootPrepare());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void WhatSide()
    {
        if(!looked)
        {
            Vector3 heading = target.position - transform.position;
            AngleDir(transform.forward, heading + new Vector3(1, 0, 1), transform.up);
            looked = true;

        }
    }


    void AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            currentShootPoints = leftCannons.GetComponentsInChildren<Transform>();
            sideToTurn = Vector3.down;
        }
        else if (dir < 0f)
        {
            currentShootPoints = rightCannons.GetComponentsInChildren<Transform>();
            sideToTurn = Vector3.up;
        }
        else
        {
            currentShootPoints = leftCannons.GetComponentsInChildren<Transform>();
            sideToTurn = Vector3.down;
        }
    }

    IEnumerator ShootPrepare()
    {
        if(!isAttacking && !isReloading)
        {
            isAttacking = true;
            isReloading = true;
            yield return new WaitForSeconds(waitTime);
            StartCoroutine(Shoot(currentShootPoints));
        }
    }

    IEnumerator Shoot(Transform[] shootPoints)
    {
            reshuffle(shootPoints);

            foreach (Transform point in shootPoints)
            {

                GameObject CreatedCannonball = Instantiate(Cannonball, point.position, point.rotation);
                CreatedCannonball.gameObject.GetComponent<Cannonball>().Dmg = Damage;
                CreatedCannonball.GetComponent<Rigidbody>().AddForce(point.transform.right * BlastPower);
                CreatedCannonball.GetComponent<Rigidbody>().AddForce(point.transform.up * (BlastPower / 3));

                Destroy(CreatedCannonball, 2.5f);
                yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
            }
            isAttacking = false;
            StartCoroutine(Reload());
        
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(5f);
        isReloading = false;
    }

    void reshuffle(Transform[] points)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < points.Length; t++)
        {
            Transform tmp = points[t];
            int r = Random.Range(t, points.Length);
            points[t] = points[r];
            points[r] = tmp;
        }
    }
}
