using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterHelix : MonoBehaviour
{
    float radius = 3.25f;
    float helixDamage = 60f;
    int chance = 50;

    public Animator anim;

    public void PerformHelix()
    {
        int randomNumber = Random.Range(1, 100);

        if(randomNumber <= chance)
        {
            anim.Play("CounterHelix");

            foreach(LivingEntityManager heros in FindObjectsOfType<LivingEntityManager>())
            {
                if(Vector3.Distance(transform.position, heros.transform.position) <= radius && heros != gameObject.GetComponent<LivingEntityManager>())
                {
                    heros.TakeHit(helixDamage, transform.localEulerAngles, "pure", 0f);
                }
            }
        }

        anim.Play("Idle");
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, radius);
    //}
}
