using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FierceSoulPassive : MonoBehaviour
{
    public int currentStack = 1;
    int maxStack = 3;

    float duration = 10f;

    float bonusAttackSpeed = 40f;
    float bonusMoveSpeed = 5f;

    bool isBuffApplied = false;

    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ApplyBuff());
    }

    IEnumerator ApplyBuff()
    {
        startTime = 0f;

        while (startTime <= duration * currentStack)
        {
            startTime += Time.deltaTime;

            if (!isBuffApplied)
            {
                //startTime = 0;
                isBuffApplied = true;
                GetComponent<LivingEntityManager>().ChangeAttackSpeed(bonusAttackSpeed);
                GetComponent<LivingEntityManager>().ChangeMoveSpeed(bonusMoveSpeed);
            }

            yield return null;
        }

        isBuffApplied = false;
        GetComponent<LivingEntityManager>().ChangeAttackSpeed(-bonusAttackSpeed * currentStack);

        if (currentStack > 0)
        {
            for (int i = 1; i <= currentStack; i++)
            {
                GetComponent<LivingEntityManager>().ChangeMoveSpeed((-bonusMoveSpeed / (100 + bonusMoveSpeed)) * 100);
            }
        }

        Destroy(this);
    }

    public void StackBuff()
    {
        currentStack++;

        if (currentStack <= maxStack)
        {
            startTime = 0;
            GetComponent<LivingEntityManager>().ChangeAttackSpeed(bonusAttackSpeed);
            GetComponent<LivingEntityManager>().ChangeMoveSpeed(bonusMoveSpeed);
        }

        else if (currentStack > maxStack)
        {
            startTime = 0;
            currentStack = maxStack;
        }
            
    }
}
