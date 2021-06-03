using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Damage()
    {
        gameObject.SetActive(false);
    }
}
