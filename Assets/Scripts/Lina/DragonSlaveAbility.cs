using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonSlaveAbility : MonoBehaviour
{
    float coolDown = 0f;
    float manaConsuption = 100f;

    public Transform spawnPoint;

    public LayerMask heroMask;
    public LayerMask ground;

    public GameObject flamePrefab;

    public GameObject highLightArea;

    public Vector3 dropPos;

    [SerializeField]
    Image coolDownImage;

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1) && coolDown == 0f)
        {
            StartCoroutine(ShowIndication());
        }
    }

    IEnumerator ShowIndication()
    {
        bool instantiated = false;
        //Debug.Log("Ability used");

        while (!Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit rayHit;            

            if (Physics.Raycast(ray, out rayHit, 100, ground))
            {
                if(instantiated == false)
                {
                    instantiated = true;
                    GameObject spellIndicator = Instantiate(highLightArea, transform) as GameObject;
                    spellIndicator.transform.position = spawnPoint.position;
                    spellIndicator.GetComponent<FireIndicator>().target = transform;
                }
            }
            yield return null;
        }
        //Debug.Log("Mouse button up");
        transform.LookAt(dropPos);

        StartCoroutine(CoolDownStart());
        GetComponent<LivingEntityManager>().ChangeMana(-manaConsuption);

        if (!gameObject.GetComponent<FierceSoulPassive>())
            gameObject.AddComponent<FierceSoulPassive>();

        else
            gameObject.GetComponent<FierceSoulPassive>().StackBuff();

        GameObject fireFlame = Instantiate(flamePrefab, transform) as GameObject;
        fireFlame.transform.position = spawnPoint.position;
        Destroy(fireFlame, 1f);

        Vector3 p1 = spawnPoint.position;
        Vector3 p2 = p1 + (transform.forward * 10);
        float radius = 3.25f;

        Collider[] collider = Physics.OverlapCapsule(p1, p2, radius, heroMask);

        foreach (Collider insideCollider in collider)
        {
            //foreach(LivingEntityManager heros in insideCollider)
            insideCollider.gameObject.GetComponent<LivingEntityManager>().TakeHit(85f, transform.localEulerAngles, "magical", gameObject.GetComponent<LivingEntityManager>().spellDamageAmpli);                           
        }
    }

    IEnumerator CoolDownStart()
    {
        coolDownImage.gameObject.SetActive(true);

        coolDown = 8f;

        while (coolDown >= 0f)
        {
            coolDown -= Time.deltaTime;
            coolDownImage.fillAmount = coolDown /8;
            yield return null;
        }

        //else
        coolDown = 0f;
        coolDownImage.gameObject.SetActive(false);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //
    //    Gizmos.DrawLine(spawnPoint.position, spawnPoint.position + (transform.forward * 10f));
    //
    //    //Gizmos.DrawWireSphere(transform.position, 2.5f);
    //}
}
