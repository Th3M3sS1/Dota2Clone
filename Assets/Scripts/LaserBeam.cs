using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Transform myPos;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            GetComponent<LineRenderer>().SetPosition(0, myPos.position);
            GetComponent<LineRenderer>().SetPosition(1, target.gameObject.GetComponentInChildren<MeshRenderer>().transform.position);
        }

        else
            Destroy(gameObject);
    }
}
