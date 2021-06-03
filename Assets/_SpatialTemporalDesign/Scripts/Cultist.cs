using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist : MonoBehaviour
{
    [SerializeField]
    public GameObject rocket;
    public float range = 50f;
    public float fireRate = 5f;
    public float projectileSpeed = 500f;

    GameObject target;
    GameObject target2;
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
        if (target2 == null)
            target2 = GameObject.FindWithTag("Cthulu");

        if (target != null && target2 != null)
        {
            if (Random.value < 0.5f)
                transform.LookAt(target.transform);
            else
                transform.LookAt(target2.transform);
        }

        if (Vector3.Distance(transform.position, target.transform.position) < range && target2 != null)
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
