using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField]
    public GameObject rocket;
    public float range = 50f;
    public float fireRate = 300f;
    public float projectileSpeed = 10f;

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
        if (target != null)
        {
            transform.LookAt(target.transform);
        }

        if (Vector3.Distance(transform.position, target.transform.position) < range)
        {
            if (Time.time > nextFire)
            {
                GameObject missile = Instantiate(rocket, transform.position, transform.rotation);
                missile.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * projectileSpeed);
                nextFire = Time.time + fireRate;
            }
        }
    }
}
