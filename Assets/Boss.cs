using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject minion1;
    public GameObject minion2;

    public void Damage()
    {
        if(minion1.activeSelf == false && minion2.activeSelf == false)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
