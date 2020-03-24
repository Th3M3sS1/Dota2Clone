using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerRange : LivingEntityManager
{
    public LayerMask movementMask;

    public LivingEntityManager curruntFocus;
    //bool move;
    PlayerMotor playerMotor;

    //Vector3 position;

    //Transform target;

    float nextShotTime = 0;

    float projectileSpeed = 0.3f;

    public Transform spawnPoint;
    public GameObject projectile;

    public Text movespeedText;
    public Text attackSpeedText;

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
        movespeedText.text = "Player Movespeed : " + (Mathf.Floor(movementSpeed * 100) / 100).ToString();
        attackSpeedText.text = "Player Attackspeed : " + (Mathf.Floor(attackSpeed * 100) / 100).ToString();

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

                if (interact != null && interact != this)
                {
                    SetFocus(interact);
                }
            }
        }

        if (curruntFocus != null)
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
        if (target != null)
        {
            //time += Time.deltaTime;

            float attackPerSecond = ((100 + agility + attackSpeed) * 0.01f) / 1.7f;
            float attackTime = 1 / attackPerSecond;

            if (Time.time >= nextShotTime)
            {
                nextShotTime = Time.time + attackTime;

                GameObject fireProjectile = Instantiate(projectile, spawnPoint.position, Quaternion.identity) as GameObject;
                //fireProjectile.transform.position = spawnPoint.position;

                fireProjectile.GetComponent<Shoot>().target = target;
                fireProjectile.GetComponent<Shoot>().projectileSpeed = projectileSpeed;
                fireProjectile.GetComponent<Shoot>().damage = baseDamage;
                fireProjectile.GetComponent<Shoot>().isTroop = true;

                //Debug.Log(baseDamage);

                //target.TakePhysicalDamage(baseDamage);

                //time = 0;
            }

            //yield return new WaitForSeconds(attackTime);
        }

        if (target == null)
        {
            if (transform.GetComponentInChildren<Shoot>())
                transform.GetComponentInChildren<Shoot>().hasTarget = false;
        }
        yield return null;
    }
}
