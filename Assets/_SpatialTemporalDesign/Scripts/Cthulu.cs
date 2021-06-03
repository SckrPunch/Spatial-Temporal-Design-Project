using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cthulu : MonoBehaviour
{
    public float fireRate = 1000f;
    float nextFire;
    public float radius = 100.0F;
    public float power = 50.0F;

    // Start is called before the first frame update
    void Start()
    {
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFire)
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
            }

            nextFire = Time.time + fireRate;
        }
    }
}
