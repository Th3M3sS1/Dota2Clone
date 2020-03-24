using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : LivingEntityManager
{
    //public float speed = 10f;

    public LayerMask movementMask;

    public LivingEntityManager curruntFocus;
    //bool move;
    PlayerMotor playerMotor;

    //Vector3 position;

    //Transform target;

    float nextShotTime = 0;

    public List<BristleFuryBuff> lastAbility = new List<BristleFuryBuff>();

    //public Text damageText;
    //public Text speedText;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        playerMotor = GetComponent<PlayerMotor>();
        curruntFocus = null;
        //playerMotor.moveSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //damageText.text = "Player Damage : " + (Mathf.Floor(baseDamage * 100) / 100).ToString();
        //speedText.text = "Player Speed : " + (Mathf.Floor(movementSpeed * 100) / 100).ToString();

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //move = true;
                playerMotor.MoveToPoint(hit.point);

                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                LivingEntityManager interact = hit.collider.GetComponent<LivingEntityManager>();

                if(interact != null && interact != this)
                {
                    SetFocus(interact);
                }
            }
        }

        if(curruntFocus != null)
        {
            if (Vector3.Distance(transform.position, curruntFocus.transform.position) <= attackRange)
            {
                StartCoroutine(AttackTarget(curruntFocus));
            }
        }        
    }

    void SetFocus(LivingEntityManager newFocus)
    {
        curruntFocus = newFocus;
        playerMotor.FollowTarget(newFocus);        
    }

    void RemoveFocus()
    {
        StopCoroutine(AttackTarget(curruntFocus));
        curruntFocus = null;
        playerMotor.StopFollowingTarget();
    }

    IEnumerator AttackTarget(LivingEntityManager target)
    {
        //float time = 0;

        if(target != null)
        {
            //time += Time.deltaTime;
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    //Debug.Log(target.name);
            //    target.CallStun(1f);
            //}

            float attackPerSecond = ((100 + agility + attackSpeed) * 0.01f) / 1.7f;
            float attackTime = 1 / attackPerSecond;

            if(Time.time >= nextShotTime)
            {
                nextShotTime = Time.time + attackTime;

                //Debug.Log(baseDamage);

                target.TakeHit(baseDamage, transform.localEulerAngles, "physical", 0f);

                if (gameObject.GetComponent<CleaveDamage>())
                {
                    gameObject.GetComponent<CleaveDamage>().PerformCleaveAttack();
                    gameObject.GetComponent<CleaveDamage>().damage = baseDamage;
                }
                //time = 0;
            }            

            //yield return new WaitForSeconds(attackTime);
        }
        yield return null;
    }
}
