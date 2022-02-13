using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GooDebuff : MonoBehaviour
{
    public int currentStack = 0;
    int maxStack = 4;

    float duration = 5f;

    float baseArmorReduction = 2f;
    float armorReductionPerStack = 1.4f;

    float baseMoveSpeedSlow = 20f;
    float moveSpeedSlowPerStatck = 3f;

    bool isDuffApplied = false;

    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        //buffDebuffPanle = FindObjectOfType<BUffDebuffPanel>();
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

          //      GameObject debuffImage = Instantiate(gooDebuffImage, buffDebuffPanle.transform) as GameObject;
                GetComponent<LivingEntityManager>().ChangeArmor(-baseArmorReduction);
                GetComponent<LivingEntityManager>().ChangeMoveSpeed(-baseMoveSpeedSlow);
            }

            //buffDebuffPanle.transform.Find("GooDebuffImage").GetComponent<Image>().fillAmount = startTime / duration;

            yield return null;
        }

        //remove debuff
        isDuffApplied = false;
        GetComponent<LivingEntityManager>().ChangeArmor(baseArmorReduction + currentStack * armorReductionPerStack);

        //float defaultMoveSpeed = (baseMoveSpeedSlow + 3 * currentStack) / (100 - (baseArmorReduction + 3 * currentStack));
        //GetComponent<LivingEntityManager>().ChangeMoveSpeed(Mathf.Ceil(defaultMoveSpeed * 100));

        if (currentStack == 0)
            GetComponent<LivingEntityManager>().ChangeMoveSpeed((baseMoveSpeedSlow / (100 - baseMoveSpeedSlow)) * 100);

        else if(currentStack > 0)
        {
            for (int i = 1; i <= currentStack; i++)
            {
                GetComponent<LivingEntityManager>().ChangeMoveSpeed((moveSpeedSlowPerStatck / (100 - moveSpeedSlowPerStatck)) * 100);
            }
            GetComponent<LivingEntityManager>().ChangeMoveSpeed((baseMoveSpeedSlow / (100 - baseMoveSpeedSlow)) * 100);
        }
        //else if(currentStack == 1)
        //{
        //    GetComponent<LivingEntityManager>().ChangeMoveSpeed((moveSpeedSlowPerStatck / (100 - moveSpeedSlowPerStatck)) * 100);
        //    GetComponent<LivingEntityManager>().ChangeMoveSpeed((baseMoveSpeedSlow / (100 - baseMoveSpeedSlow)) * 100);
        //}
        //
        //else if(currentStack == 2)
        //{
        //    GetComponent<LivingEntityManager>().ChangeMoveSpeed((moveSpeedSlowPerStatck / (100 - moveSpeedSlowPerStatck)) * 100);
        //    GetComponent<LivingEntityManager>().ChangeMoveSpeed((moveSpeedSlowPerStatck / (100 - moveSpeedSlowPerStatck)) * 100);
        //    GetComponent<LivingEntityManager>().ChangeMoveSpeed((baseMoveSpeedSlow / (100 - baseMoveSpeedSlow)) * 100);
        //}
        //
        //else if (currentStack == 3)
        //{
        //    GetComponent<LivingEntityManager>().ChangeMoveSpeed((moveSpeedSlowPerStatck / (100 - moveSpeedSlowPerStatck)) * 100);
        //    GetComponent<LivingEntityManager>().ChangeMoveSpeed((moveSpeedSlowPerStatck / (100 - moveSpeedSlowPerStatck)) * 100);
        //    GetComponent<LivingEntityManager>().ChangeMoveSpeed((moveSpeedSlowPerStatck / (100 - moveSpeedSlowPerStatck)) * 100);
        //    GetComponent<LivingEntityManager>().ChangeMoveSpeed((baseMoveSpeedSlow / (100 - baseMoveSpeedSlow)) * 100);
        //}
        //
        //else if (currentStack == 4)
        //{
        //    GetComponent<LivingEntityManager>().ChangeMoveSpeed((moveSpeedSlowPerStatck / (100 - moveSpeedSlowPerStatck)) * 100);
        //    GetComponent<LivingEntityManager>().ChangeMoveSpeed((moveSpeedSlowPerStatck / (100 - moveSpeedSlowPerStatck)) * 100);
        //    GetComponent<LivingEntityManager>().ChangeMoveSpeed((moveSpeedSlowPerStatck / (100 - moveSpeedSlowPerStatck)) * 100);
        //    GetComponent<LivingEntityManager>().ChangeMoveSpeed((moveSpeedSlowPerStatck / (100 - moveSpeedSlowPerStatck)) * 100);
        //    GetComponent<LivingEntityManager>().ChangeMoveSpeed((baseMoveSpeedSlow / (100 - baseMoveSpeedSlow)) * 100);
        //}
        //remove this script from gameobject
        //Destroy(buffDebuffPanle.transform.Find("GooDebuffImage"));
        Destroy(this);        
    }

    public void StackDebuff()
    {
        currentStack++;

        if (isDuffApplied && currentStack <= maxStack)
        {
            startTime = 0;
            GetComponent<LivingEntityManager>().ChangeArmor(-armorReductionPerStack);
            GetComponent<LivingEntityManager>().ChangeMoveSpeed(-moveSpeedSlowPerStatck);
        }

        else if (currentStack > maxStack)
            currentStack = maxStack;
    }
}
