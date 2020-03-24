using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThunderStrikeAbility : MonoBehaviour
{
    float coolDown = 0f;
    float manaConsuption = 100f;

    //public Transform spawnPoint;

    public LayerMask heroMask;
    public LayerMask ground;

    public GameObject thunderStrikePrefab;

    public GameObject highLightArea;

    public Vector3 dropPos;

    [SerializeField]
    Image coolDownImage;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha2) && coolDown == 0f)
        {
            StartCoroutine(ShowIndication());
        }
    }

    IEnumerator ShowIndication()
    {
        bool instantiated = false;

        while (!Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit rayHit;

            if (Physics.Raycast(ray, out rayHit, 100, ground))
            {
                if (instantiated == false && Vector3.Distance(transform.position, rayHit.point) <= 7f)
                {
                    instantiated = true;
                    Vector3 pos = new Vector3(rayHit.point.x, 0.01f, rayHit.point.z);
                    GameObject spellIndicator = Instantiate(highLightArea, pos, Quaternion.identity) as GameObject;
                    //spellIndicator.transform.position = new Vector3(rayHit.point.x, 0.01f, rayHit.point.z);
                    spellIndicator.GetComponent<ThunderStrikeIndicator>().target = transform;
                }
            }
            yield return null;
        }

        //Vector3 spawnPos = Vector3.zero;

        //RaycastHit hit;
        //Ray rays = Camera.main.ScreenPointToRay(Input.mousePosition);
        //
        //if (Physics.Raycast(rays, out hit, 100, ground))
        //    spawnPos = new Vector3(hit.point.x, 0f, hit.point.z);

        transform.LookAt(dropPos);

        StartCoroutine(CoolDownStart());
        GetComponent<LivingEntityManager>().ChangeMana(-manaConsuption);

        if (!gameObject.GetComponent<FierceSoulPassive>())
            gameObject.AddComponent<FierceSoulPassive>();

        else
            gameObject.GetComponent<FierceSoulPassive>().StackBuff();

        GameObject thunderStrike = Instantiate(thunderStrikePrefab, transform) as GameObject;
        thunderStrike.transform.position = dropPos;
        Destroy(thunderStrike, 1f);

        yield return new WaitForSeconds(0.5f);

        Collider[] collider = Physics.OverlapSphere(dropPos, 2.5f, heroMask);

        foreach (Collider insideCollider in collider)
        {
            insideCollider.GetComponent<LivingEntityManager>().TakeHit(80f, transform.localEulerAngles, "magical", gameObject.GetComponent<LivingEntityManager>().spellDamageAmpli);
            StartCoroutine(insideCollider.GetComponent<LivingEntityManager>().OnStun(1.6f));
        }
    }

    IEnumerator CoolDownStart()
    {
        coolDownImage.gameObject.SetActive(true);

        coolDown = 7f;

        while (coolDown >= 0f)
        {
            coolDown -= Time.deltaTime;
            coolDownImage.fillAmount = coolDown / 7;

            yield return null;
        }

        //else
        coolDown = 0f;
        coolDownImage.gameObject.SetActive(false);
    }
}
