using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BristleBackPassive : MonoBehaviour
{
    private void Start()
    {
        //GetComponent<LivingEntityManager>().attackerString = "BristleBack";
    }

    public void TakeHit(float damage, Vector3 attackerDirection, string damageType, float spellAmpli)
    {
        float hitDirection = (180 + attackerDirection.y) - transform.localEulerAngles.y;
        float reducedDamage;

        if ((hitDirection > 70 && hitDirection < 110) || (hitDirection > -110 && hitDirection < -70))
        {
            if (damageType == "physical")
            {
                reducedDamage = damage * (1 - (8f / 100));
                //Debug.Log("Side Damage" + " " + reducedDamage);
                GetComponent<LivingEntityManager>().TakePhysicalDamage(reducedDamage);
            }

            else if (damageType == "magical")
            {
                reducedDamage = damage * (1 - (8f / 100));
                //Debug.Log("Side Damage" + " " + reducedDamage);
                GetComponent<LivingEntityManager>().TakeMagicalDamage(reducedDamage, spellAmpli);
            }

            else if(damageType == "pure")
            {
                GetComponent<LivingEntityManager>().TakePureDamage(damage);
            }
        }
        else if ((hitDirection > 110 && hitDirection < 250) || (hitDirection > -250 && hitDirection < -110))
        { 
            if (damage > 20)
            {
                GetComponent<QuillSprayAbility>().SprayQuill(false);
            }

            if (damageType == "physical")
            {
                reducedDamage = damage * (1 - (16f / 100));
                //Debug.Log("Rear Damage" + " " + reducedDamage);
                GetComponent<LivingEntityManager>().TakePhysicalDamage(reducedDamage);
            }

            else if (damageType == "magical")
            {
                reducedDamage = damage * (1 - (16f / 100));
                //Debug.Log("Rear Damage" + " " + reducedDamage);
                GetComponent<LivingEntityManager>().TakeMagicalDamage(reducedDamage, spellAmpli);
            }

            else if (damageType == "pure")
            {
                GetComponent<LivingEntityManager>().TakePureDamage(damage);
            }
        }
        else
        {
            if (damageType == "physical")
            {
                //reducedDamage = damage * (1 - (8 / 100));
                //Debug.Log("Front Damage");
                GetComponent<LivingEntityManager>().TakePhysicalDamage(damage);
            }

            else if (damageType == "magical")
            {
                //reducedDamage = damage * (1 - (8 / 100));
                //Debug.Log("Front Damage");
                GetComponent<LivingEntityManager>().TakeMagicalDamage(damage, spellAmpli);
            }

            else if (damageType == "pure")
            {
                GetComponent<LivingEntityManager>().TakePureDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        float positiveSideAngle = (70f + transform.localEulerAngles.y) * Mathf.Deg2Rad;
        float positiveRearAngle = (110f + transform.localEulerAngles.y) * Mathf.Deg2Rad;
        //float distance = 1f;

        Vector3 myPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 finalPositiveSidePos = new Vector3(5 * Mathf.Sin(positiveSideAngle), 0f, 5 * Mathf.Cos(positiveSideAngle));
        Vector3 posSideDirection = (myPos + finalPositiveSidePos);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(myPos, posSideDirection);

        Vector3 finalPositiveRearPos = new Vector3(5 * Mathf.Sin(positiveRearAngle), 0f, 5 * Mathf.Cos(positiveRearAngle));
        Vector3 posRearDirection = (myPos + finalPositiveRearPos);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(myPos, posRearDirection);

        float negetiveSideAngle = (-70f + transform.localEulerAngles.y) * Mathf.Deg2Rad;
        float negetiveRearAngle = (-110f + transform.localEulerAngles.y) * Mathf.Deg2Rad;
        //float distance = 1f;

        //Vector3 myPos = new Vector3(transform.position.x, 1f, transform.position.z);
        Vector3 finalNegetiveSidePos = new Vector3(5 * Mathf.Sin(negetiveSideAngle), 0f, 5 * Mathf.Cos(negetiveSideAngle));
        Vector3 negSideDirection = (myPos + finalNegetiveSidePos);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(myPos, negSideDirection);

        Vector3 finalNegetiveRearPos = new Vector3(5 * Mathf.Sin(negetiveRearAngle), 0f, 5 * Mathf.Cos(negetiveRearAngle));
        Vector3 negRearDirection = (myPos + finalNegetiveRearPos);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(myPos, negRearDirection);
    }
}
