using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryShoot : MonoBehaviour
{
    public GameObject minion1;
    public GameObject minion2;
    public GameObject rocket;
    public float projectileSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2") && minion1.activeSelf == false && minion2.activeSelf == false)
        {
            GameObject missile = Instantiate(rocket, transform.position, transform.rotation);
            missile.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * projectileSpeed);
        }
    }
}
