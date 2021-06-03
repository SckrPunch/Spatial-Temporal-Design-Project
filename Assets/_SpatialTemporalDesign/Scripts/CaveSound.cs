using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSound : MonoBehaviour
{
    public bool hasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.tag == "Player")
        {
            hasPlayed = true;
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
