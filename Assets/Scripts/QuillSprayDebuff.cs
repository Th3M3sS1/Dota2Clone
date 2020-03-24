using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuillSprayDebuff : MonoBehaviour
{
    public int currentStack = 0;

    float duration = 14f;

    float startTime;

    bool isDuffApplied = false;

    float stackDamage = 30f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ApplyDebuff());
    }

    IEnumerator ApplyDebuff()
    {
        startTime = 0f;

        while (startTime <= duration)
        {
            startTime += Time.deltaTime;

            if (!isDuffApplied)
            {
                //startTime = 0;
                isDuffApplied = true;
            }

            yield return null;
        }

        isDuffApplied = false;

        Destroy(this);
    }

    public void StackDebuff()
    {
        currentStack++;

        if (isDuffApplied && currentStack * stackDamage <= 150f)
        {
            startTime = 0;
            GetComponent<LivingEntityManager>().TakePhysicalDamage(stackDamage);
        }
    }
}
