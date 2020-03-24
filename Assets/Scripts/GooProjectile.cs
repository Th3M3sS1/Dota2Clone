using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooProjectile : MonoBehaviour
{
    public LivingEntityManager target;
    float projectileSpeed = 0.2f;

    public bool hasTarget;

    // Start is called before the first frame update
    private void Start()
    {
        hasTarget = true;
    }

    // Update is called once per frame
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

            if (Vector3.Distance(transform.position, centerOfTarget.position) > centerToEdgeDistance)
                transform.position = Vector3.MoveTowards(transform.position, centerOfTarget.position, projectileSpeed);

            else if (Vector3.Distance(transform.position, centerOfTarget.position) <= centerToEdgeDistance)
            {
                if (!target.gameObject.GetComponent<GooDebuff>())
                    target.gameObject.AddComponent<GooDebuff>();

                else
                    target.GetComponent<GooDebuff>().StackDebuff();

                Destroy(gameObject);
            }
        }

        else
            Destroy(gameObject);
    }
}
