using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CannonController : MonoBehaviour
{
    public float BlastPower = 5;

    bool isAttacking = false;
    bool isReloading = false;

    


    public int cannonballDmg = 5;

    public GameObject Cannonball;
    public Transform placeholder;


    public int key;

    private Transform[] shootPoints;

    private void Start()
    {
        shootPoints = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(key) && !isAttacking && !isReloading)
        {
            isAttacking = true;
            isReloading = true;
            StartCoroutine(Shoot());
            
        }
    }

    IEnumerator Shoot()
    {
        reshuffle(shootPoints);

        foreach (Transform point in shootPoints)
        {

            GameObject CreatedCannonball = Instantiate(Cannonball, point.position, point.rotation);
            CreatedCannonball.gameObject.GetComponent<Cannonball>().Dmg = cannonballDmg;
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