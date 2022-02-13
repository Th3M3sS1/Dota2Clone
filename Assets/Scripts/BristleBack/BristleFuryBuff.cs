using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BristleFuryBuff : MonoBehaviour
{
    float bonusDamagePerStack = 22f;
    float bonusMoveSpeedPerStack = 3f;

    float duration = 14f;

    float startTime;

    bool isDuffApplied = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<PlayerController>().lastAbility.Add(this);
        StartCoroutine(ApplyBuff());
    }

    IEnumerator ApplyBuff()
    {
        startTime = 0f;

        while (startTime <= duration)
        {
            startTime += Time.deltaTime;

            if (!isDuffApplied)
            {
                //startTime = 0;
                isDuffApplied = true;
                GetComponent<LivingEntityManager>().ChangeAttackDamage(bonusDamagePerStack);
                GetComponent<LivingEntityManager>().ChangeMoveSpeed(bonusMoveSpeedPerStack);
            }
            yield return null;
        }
        
        isDuffApplied = false;

        GetComponent<LivingEntityManager>().ChangeAttackDamage(-bonusDamagePerStack);
        GetComponent<LivingEntityManager>().ChangeMoveSpeed((-bonusMoveSpeedPerStack) / (100 + bonusMoveSpeedPerStack) * 100f);

        gameObject.GetComponent<PlayerController>().lastAbility.Remove(this);
        Destroy(this);
    }
}
