using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamare : MonoBehaviour
{
    public Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.back, mainCam.transform.rotation * Vector3.down);
    }
}
