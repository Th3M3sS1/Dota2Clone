using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GooAbility : MonoBehaviour
{
    public Material highlightMat;
    public Material wrongHighlighMat;
    public Material defaultMat;

    public LayerMask heroMask;

    LivingEntityManager currentSelection;

    public GameObject gooPrefab;

    public Transform gooSpawnPoint;

    bool isRunning = false;

    //float currentTime = 0f;
    float coolDown = 0f;
    float manaConsuption = 12f;

    [SerializeField]
    Image coolDownImage;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1) && coolDown == 0f)
        {
            StartShowIndicationCoroutine();
        }    
    }

    public void StartShowIndicationCoroutine()
    {
        if(!isRunning)
            StartCoroutine(ShowIndication());
    }

    IEnumerator ShowIndication()
    {
        isRunning = true;
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

                    if(hero != this.GetComponent<LivingEntityManager>())
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
            isRunning = false;
        }

        if(currentSelection != null)
        {
            currentSelection.GetComponentInChildren<MeshRenderer>().material = defaultMat;

            while(Vector3.Distance(transform.position, currentSelection.transform.position) > 8f)
            {
                yield return null;
            }

            StartCoroutine(CoolDownStart());
            GetComponent<LivingEntityManager>().ChangeMana(-manaConsuption);

            if (gameObject.GetComponent<PlayerController>().lastAbility.Count < 5)
            {
                gameObject.AddComponent<BristleFuryBuff>();
            }
                

            //else
            //    gameObject.GetComponent<BristleFuryPassive>().StackDebuff();


            GameObject goo = Instantiate(gooPrefab, gooSpawnPoint.position, Quaternion.identity) as GameObject;
            //goo.transform.position = gooSpawnPoint.position;

            goo.GetComponent<GooProjectile>().target = currentSelection;
        }      
        
        else if(currentSelection == null)
        {
            if (transform.GetComponentInChildren<GooProjectile>())
                transform.GetComponentInChildren<GooProjectile>().hasTarget = false;
        }        
    }

    IEnumerator CoolDownStart()
    {
        coolDownImage.gameObject.SetActive(true);
        coolDown = 1.5f;

        while(coolDown >= 0f)
        {
            coolDown -= Time.deltaTime;
            coolDownImage.fillAmount = coolDown / 1.5f;

            yield return null;
        }

        //else
            coolDown = 0f;
        coolDownImage.gameObject.SetActive(false);
    }
}
