using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuganBladeAbility : MonoBehaviour
{
    public Material highlightMat;
    public Material wrongHighlighMat;
    public Material defaultMat;

    public LayerMask heroMask;

    LivingEntityManager currentSelection;

    public GameObject laserPrefab;

    public Transform spawnPos;

    //float currentTime = 0f;
    float coolDown = 0f;
    float manaConsuption = 280f;

    [SerializeField]
    Image coolDownImage;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && coolDown == 0f)
        {
            StartCoroutine(ShowIndication());
        }
    }

    IEnumerator ShowIndication()
    {
        LivingEntityManager hero;

        while (!Input.GetMouseButtonUp(0))
        {
            if (currentSelection != null)
            {
                currentSelection.GetComponentInChildren<MeshRenderer>().material = defaultMat;
                currentSelection = null;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, heroMask))
            {
                //Material temp;

                if (hit.collider.GetComponent<LivingEntityManager>())
                {
                    hero = hit.collider.GetComponent<LivingEntityManager>();
                    //temp = hero.GetComponentInChildren<MeshRenderer>().material;

                    if (hero != this.GetComponent<LivingEntityManager>())
                    {
                        hero.GetComponentInChildren<MeshRenderer>().material = highlightMat;

                        currentSelection = hero;
                    }
                    //if (Input.GetMouseButtonUp(0))
                    //{
                    //    //Debug.Log("click");
                    //    hero.GetComponentInChildren<MeshRenderer>().material = wrongHighlighMat;
                    //
                    //    //Deploy GOO
                    //}
                }
            }
            yield return null;
        }

        if (currentSelection != null)
        {
            currentSelection.GetComponentInChildren<MeshRenderer>().material = defaultMat;

            while (Vector3.Distance(transform.position, currentSelection.transform.position) > 8f)
            {
                yield return null;
            }

            StartCoroutine(CoolDownStart());
            GetComponent<LivingEntityManager>().ChangeMana(-manaConsuption);

            //else
            //    gameObject.GetComponent<BristleFuryPassive>().StackDebuff();


            GameObject laser = Instantiate(laserPrefab, transform) as GameObject;
            //goo.transform.position = gooSpawnPoint.position;

            laser.GetComponent<LaserBeam>().myPos = spawnPos;
            laser.GetComponent<LaserBeam>().target = currentSelection.transform;
            //
            //laser.GetComponent<LineRenderer>().SetPosition(0, spawnPos.position);
            //laser.GetComponent<LineRenderer>().SetPosition(1, currentSelection.gameObject.GetComponentInChildren<MeshRenderer>().transform.position);

            yield return new WaitForSeconds(0.25f);

            currentSelection.TakeHit(450f, transform.localEulerAngles, "magical", GetComponent<LivingEntityManager>().spellDamageAmpli);

            Destroy(laser, 0.5f);
        }

        else if (currentSelection == null)
        {
            //if (transform.GetComponentInChildren<LaserBeam>())
            //    transform.GetComponentInChildren<LaserBeam>().hasTarget = false;
        }
    }

    IEnumerator CoolDownStart()
    {
        coolDownImage.gameObject.SetActive(true);
        coolDown = 70f;

        while (coolDown >= 0f)
        {
            coolDown -= Time.deltaTime;
            coolDownImage.fillAmount = coolDown / 70;

            yield return null;
        }

        //else
        coolDown = 0f;
        coolDownImage.gameObject.SetActive(false);
    }
}
