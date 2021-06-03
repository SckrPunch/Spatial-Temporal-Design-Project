using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avalanche : MonoBehaviour
{
    public float speed = 50.0f;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target.transform);
            Debug.Log("look");
        }
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}
