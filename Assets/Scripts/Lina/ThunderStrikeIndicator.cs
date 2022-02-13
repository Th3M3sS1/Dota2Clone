using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrikeIndicator : MonoBehaviour
{
    public LayerMask ground;
    public Transform target;

    Vector3 dropSpellPos;

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, ground))
        {
            Vector3 pos = new Vector3(hit.point.x, 0.01f, hit.point.z);

            Vector3 offset = pos - target.position;

            transform.position = target.position + Vector3.ClampMagnitude(offset, 7f);

            dropSpellPos = transform.position;
        }

        if (Input.GetMouseButtonUp(0))
        {
            target.GetComponent<ThunderStrikeAbility>().dropPos = dropSpellPos;
            Destroy(gameObject);
        }            
    }
}
