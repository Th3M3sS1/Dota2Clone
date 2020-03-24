using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaveDamage : MonoBehaviour
{
    float distance = 8f;
    float attackAngle = 90f;
    public float damage;

    public void PerformCleaveAttack()
    {
        foreach(LivingEntityManager heros in FindObjectsOfType<LivingEntityManager>())
        {
            if(Vector3.Distance(transform.position, heros.transform.position) <= distance && heros != gameObject.GetComponent<LivingEntityManager>())
            {
                Vector3 directionToHeros = (heros.transform.position - transform.position).normalized;
                float angleBTWPlayerAndHeroes = Vector3.Angle(transform.forward, directionToHeros);

                if(angleBTWPlayerAndHeroes < attackAngle / 2f)
                {
                    float reducedDamage = 30f * damage / 100;
                    heros.TakeHit(reducedDamage, transform.localEulerAngles, "physical", 0f);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        float positiveAngle = (45f + transform.localEulerAngles.y) * Mathf.Deg2Rad;
        float negetiveAngle = (-45f + transform.localEulerAngles.y) * Mathf.Deg2Rad;

        Vector3 myPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 finalPositivePos = new Vector3(8 * Mathf.Sin(positiveAngle), 0f, 8 * Mathf.Cos(positiveAngle));
        Vector3 posDirection = (myPos + finalPositivePos);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(myPos, posDirection);

        Vector3 finalNegetivePos = new Vector3(8 * Mathf.Sin(negetiveAngle), 0f, 8 * Mathf.Cos(negetiveAngle));
        Vector3 negDirection = (myPos + finalNegetivePos);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(myPos, negDirection);
    }
}
