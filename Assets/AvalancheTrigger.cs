using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvalancheTrigger : MonoBehaviour
{
    [SerializeField]
    public GameObject avalanche;
    public GameObject spawnPoint;
    bool hasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !hasSpawned)
        {
            hasSpawned = true;
            Instantiate(avalanche, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
