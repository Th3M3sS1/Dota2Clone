using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public LivingEntityManager target;
    public float projectileSpeed;
    public float damage;

    public bool hasTarget;
    public bool isTroop = false;
    public bool isBuilding = false;

    private void Start()
    {
        hasTarget = true;
    }

    private void Update()
    {
        if (!hasTarget)
        {
            Destroy(gameObject);
            return;
        }

        if (target != null)
        {
            //Destroy(gameObject);

            Transform centerOfTarget = target.GetComponentInChildren<MeshRenderer>().gameObject.transform;
            float centerToEdgeDistance = target.GetComponentInChildren<Renderer>().bounds.size.x;
            transform.LookAt(target.transform);
            //transform.localPosition += Vector3.forward * Time.deltaTime * 10f;

            if (Vector3.Distance(transform.position, centerOfTarget.position) > centerToEdgeDistance && target != null)
                transform.position = Vector3.MoveTowards(transform.position, centerOfTarget.position, projectileSpeed);

            else if (Vector3.Distance(transform.position, centerOfTarget.position) <= centerToEdgeDistance)
            {
                if (isTroop)
                {
                    //Debug.Log(damage);
                    target.TakeHit(damage, transform.localEulerAngles, "physical", 0f);
                    //target.TakeHit(damage, transform.position);
                    Destroy(gameObject);
                }

                //else if (isBuilding)
                //{
                //    target.GetComponent<BuildingsManager>().TakeDamage(damage);
                //    Destroy(gameObject);
                //
                //}
            }
        }

        else
            Destroy(gameObject);
    }
}
