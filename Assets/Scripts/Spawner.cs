using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnDelay;
    public GameObject spawnee;
    public int NumberOfEnemys;
    bool spawnAvailble = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (NumberOfEnemys < 1 && spawnAvailble) StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        spawnAvailble = false;
        float randomX = Random.Range(0, 1900);
        float randomZ = Random.Range(0, 700);

        GameObject enemy = Instantiate(spawnee, new Vector3(0, 0, -450), Quaternion.identity);
        GameObject child = enemy.transform.GetChild(0).gameObject;
        child.GetComponent<Enemy>().spawner = this;
        child.transform.position = new Vector3(randomX, 0, randomZ);
        child.GetComponent<EnemyController>().target = FindObjectOfType<Player>().transform;
        NumberOfEnemys++;
        yield return new WaitForSeconds(spawnDelay);
        spawnAvailble = true;

    }
}
