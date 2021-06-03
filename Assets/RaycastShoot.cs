using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    public float weaponRange = 50f;

    private Camera fpsCam;

    // Start is called before the first frame update
    void Start()
    {
        fpsCam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                if(hit.collider.gameObject.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<Enemy>().Damage();
                }
                if(hit.collider.gameObject.tag == "Boss")
                {
                    hit.collider.gameObject.GetComponent<Boss>().Damage();
                }
            }
        }
    }
}
