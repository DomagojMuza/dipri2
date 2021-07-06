using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MSpawner : Spawner
{
    bool spawnAvailble = true;

    void Start()
    {
    }

    void Update()
    {   
        if (NumberOfEnemys < HowManyToSpawn && spawnAvailble) StartCoroutine(Spawn());
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
        child.transform.localPosition = new Vector3(coordinates[0], 0, coordinates[1]);
        NumberOfEnemys++;
        yield return new WaitForSeconds(spawnDelay);
        spawnAvailble = true;

    }

}
