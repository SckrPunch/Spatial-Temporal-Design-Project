using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CthuluSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject cthulu;
    public float range = 10f;
    GameObject player;
    bool hasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
     player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpawned && Vector3.Distance(transform.position, player.transform.position) < range)
        {
            hasSpawned = true;
            Instantiate(cthulu, transform.position, transform.rotation);
        }
    }
}
