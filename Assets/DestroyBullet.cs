using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Despawn", 3);
    }


    void Despawn()
    {
        Destroy(gameObject);
    }
}
