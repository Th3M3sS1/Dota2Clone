using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireIndicator : MonoBehaviour
{
    public Transform target;

    public LayerMask ground;

    Vector3 dropSpellPos;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, ground))
        {
            Vector3 pos = new Vector3(hit.point.x, 0.01f, hit.point.z);

            Vector3 offset = pos - target.position;

            transform.position = target.position + Vector3.ClampMagnitude(offset, 5f);

            dropSpellPos = transform.position;
        }

        if (Input.GetMouseButtonUp(0))
        {
            target.GetComponent<DragonSlaveAbility>().dropPos = dropSpellPos;
            Destroy(gameObject);
        }            
    }
}
