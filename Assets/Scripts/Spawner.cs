using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public float spawnDelay;
    public GameObject spawnee;
    public int NumberOfEnemys;
    public int waitTime;
    public string name;

    public int xMin;
    public int xMax;
    public int zMin;
    public int zMax;
    bool spawnAvailble = true;

    void Start()
    {
    }

    void Update()
    {   
        if (NumberOfEnemys < 5 && spawnAvailble) StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        float[] coordinates = GenerateCoordinates();
        float distance = Vector3.Distance(FindObjectOfType<Player>().transform.position, new Vector3(coordinates[0], 0, coordinates[1]));
        while(distance< 500){
            coordinates = GenerateCoordinates();
            distance = Vector3.Distance(FindObjectOfType<Player>().transform.position, new Vector3(coordinates[0], 0, coordinates[1]));
        }
        spawnAvailble = false;
        
        Debug.Log(coordinates[0] + " " + coordinates[1]);
        GameObject enemy = Instantiate(spawnee, new Vector3(0, 0, -450), Quaternion.identity);
        GameObject child = enemy.transform.GetChild(0).gameObject;
        child.transform.name = this.name;
        child.GetComponent<Enemy>().spawner = this;
        NavMeshAgent agent = child.GetComponent<NavMeshAgent>();
        child.GetComponent<EnemyController>().waitTimeSet = 50f/agent.angularSpeed;
        child.transform.localPosition = new Vector3(coordinates[0], 0, coordinates[1]);
        child.GetComponent<EnemyController>().target = FindObjectOfType<Player>().transform;
        NumberOfEnemys++;
        yield return new WaitForSeconds(spawnDelay);
        spawnAvailble = true;

    }

    public float[] GenerateCoordinates()
    {
        float randomX = Random.Range(xMin, xMax);
        float randomZ = Random.Range(zMin, zMax);
        float[] values = new float[2];
        values[0] = randomX;
        values[1] = randomZ;
        return values;
        
    }
}
