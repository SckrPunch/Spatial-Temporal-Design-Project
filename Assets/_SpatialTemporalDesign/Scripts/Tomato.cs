using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    public GameObject splat;
    public float speed = 10f;

    Rigidbody rb;
    GameObject target;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player");
        direction = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector3(direction.x, direction.y, direction.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(splat, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
