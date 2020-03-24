using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntityManager
{
    float nextShotTime;
    float projectileSpeed = 0.3f;
    NavMeshAgent pathfinder;
    Transform target;

    public Transform spawnPoint;
    public GameObject projectile;

    //public Text armorText;
    //public Text speedText;

    protected override void Start()
    {
        base.Start();
        pathfinder = GetComponent<NavMeshAgent>();
        pathfinder.speed = movementSpeed;
        pathfinder.stoppingDistance = attackRange;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        //StartCoroutine(UpdatePath());
    }

    void Update()
    {
        //armorText.text = "Enemy Armor : " + (Mathf.Floor(armor * 100) / 100).ToString();
        //speedText.text = "Enemy Speed : " + (Mathf.Floor(movementSpeed * 100) / 100).ToString();

        if (target != null && (currentState != State.Stunned || currentState != State.Rooted))
        {
            //Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            pathfinder.SetDestination(target.position);
            FaceTarget();

            if (Vector3.Distance(transform.position, target.position) <= attackRange)
            {
                //Debug.Log("Attack" + target.transform.name);
                StartCoroutine(AttackTarget(target.GetComponent<LivingEntityManager>()));
                //target.GetComponent<LivingEntityManager>().ReduceArmor(15);
            }
        }
    }

    //IEnumerator UpdatePath()
    //{
    //    float refreshRate = .25f;
    //
    //    while (target != null)
    //    {
    //        Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
    //        
    //        yield return new WaitForSeconds(refreshRate);
    //    }
    //}

    IEnumerator AttackTarget(LivingEntityManager target)
    {
        if (target != null && (currentState != State.Stunned || currentState != State.Breaked))
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

        if(target == null)
        {
            if (transform.GetComponentInChildren<Shoot>())
                transform.GetComponentInChildren<Shoot>().hasTarget = false;
        }
        yield return null;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookROtation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookROtation, Time.deltaTime * 5);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, attackRange);
    }
}