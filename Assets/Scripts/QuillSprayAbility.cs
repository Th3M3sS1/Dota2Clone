using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuillSprayAbility : MonoBehaviour
{
    public GameObject sparayPrefab;
    
    float coolDown = 0f;
    float manaConsuption = 35f;

    [SerializeField]
    Image coolDownImage;
    //Transform spraySpawnPoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha2) && coolDown == 0f)
        {
            SprayQuill(true);
        }
    }

    public void SprayQuill(bool isAbility)
    {
        if (isAbility)
        {
            StartCoroutine(CoolDownStart());
            GetComponent<LivingEntityManager>().ChangeMana(-manaConsuption);

            if (gameObject.GetComponent<PlayerController>().lastAbility.Count < 5)
            {
                gameObject.AddComponent<BristleFuryBuff>();
            }
                

            //else
            //    gameObject.GetComponent<BristleFuryPassive>().StackDebuff();                
        }
        //StartCoroutine(CoolDownStart());
        //GetComponent<LivingEntityManager>().ChangeMana(-manaConsuption);

        GameObject spray = Instantiate(sparayPrefab, transform.position, Quaternion.identity) as GameObject;

        foreach(LivingEntityManager heros in FindObjectsOfType<LivingEntityManager>())
        {
            if(Vector3.Distance(transform.position, heros.transform.position) <= 9f && heros != this.GetComponent<LivingEntityManager>())
            {
                heros.TakeHit(20f, transform.localEulerAngles, "physical", 0f);

                if (!heros.gameObject.GetComponent<QuillSprayDebuff>())
                    heros.gameObject.AddComponent<QuillSprayDebuff>();

                else
                    heros.GetComponent<QuillSprayDebuff>().StackDebuff();
            }
        }
    }

    IEnumerator CoolDownStart()
    {
        coolDownImage.gameObject.SetActive(true);
        coolDown = 3f;

        while (coolDown >= 0f)
        {
            coolDown -= Time.deltaTime;
            coolDownImage.fillAmount = coolDown / 3;

            yield return null;
        }

        //else
        coolDown = 0f;
        coolDownImage.gameObject.SetActive(false);
    }
}
