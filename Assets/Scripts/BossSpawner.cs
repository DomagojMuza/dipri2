using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public void Spawn(Story s)
    {
        GameObject enemy = Instantiate(s.npc, new Vector3(0, 0, -450), Quaternion.identity);
        GameObject child = enemy.transform.GetChild(0).gameObject;
        child.transform.name = this.name;
        child.GetComponent<Enemy>().spawner = null;
        NavMeshAgent agent = child.GetComponent<NavMeshAgent>();
        child.GetComponent<EnemyController>().waitTimeSet = 50f/agent.angularSpeed;
        child.transform.localPosition = new Vector3(s.x, 0, s.z);
        agent.SetDestination(new Vector3(s.x, 0, s.z));
        child.GetComponent<EnemyController>().target = FindObjectOfType<Player>().transform;

    }
}
