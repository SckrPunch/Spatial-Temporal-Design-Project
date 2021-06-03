using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectator : MonoBehaviour
{
    public GameObject tomato;
    public float range = 50f;
    public float fireRate = 3f;

    GameObject target;
    float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < range)
        {
            if (Time.time > nextFire)
            {
                Instantiate(tomato, transform.position, Quaternion.identity);
                nextFire = Time.time + fireRate;
            }
        }
    }
}
