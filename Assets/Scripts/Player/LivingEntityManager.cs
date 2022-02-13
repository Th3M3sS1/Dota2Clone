using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class LivingEntityManager : MonoBehaviour,IDamagable
{
    [SerializeField]
    public float startingStrength;
    public float strnght { get; protected set; }

    public float startingHealth;
    public float health { get; protected set; }
    float maxHealth;

    public float startingHealthRegen;
    public float healthRegen { get; protected set; }

    public float startingMegicResistant;
    public float magicResistant { get; protected set; }

    public float startingAigility;
    public float agility { get; protected set; }

    public float startingArmor;
    [SerializeField]
    public float armor { get; protected set; }

    public float startingAttackSpeed;
    public float attackSpeed { get; protected set; }

    public float startingAttackRange;
    public float attackRange { get; protected set; }

    public float startingMovementSpeed;
    [SerializeField]
    public float movementSpeed { get; protected set; }

    public float startingIntelligence;
    public float intelligence { get; protected set; }

    public float startingMana;
    public float mana { get; protected set; }
    float maxMana;

    public float startingManaReegenh;
    public float manaRegen { get; protected set; }

    public float startingSpellDamageAmpli;
    public float spellDamageAmpli { get; protected set; }

    public float startingBaseDamage;
    public float baseDamage { get; protected set; }

    public State currentState;

    public enum State
    {
        Idle,
        Stunned,
        Silenced,
        Rooted,
        Breaked
    }

    //public string attackerString = "none";
    //public int numberOfskills;

    bool isDead;

    public Image healthBar;
    public Image manaBar;

    //public Image stateBar;
    public Text stateText;
    //
    //public Transform canvas;
    //public GameObject damageText;

    public event Action OnDeath;

    protected virtual void Start()
    {
        strnght = startingStrength;
        health = startingHealth + (20 * strnght);
        maxHealth = health;
        healthRegen = startingHealthRegen + (0.1f * strnght);
        //Debug.Log(healthRegen);
        StartCoroutine(HPRegen(health));
        magicResistant = (0.08f * strnght / 100);

        agility = startingAigility;
        armor = startingArmor + (0.16f * agility);
        attackSpeed = 0;
        attackRange = startingAttackRange;
        movementSpeed = startingMovementSpeed + (0.05f * agility / 100);

        intelligence = startingIntelligence;
        mana = startingMana + (12 * intelligence);
        maxMana = mana;
        manaRegen = startingManaReegenh + (0.05f * intelligence);
        StartCoroutine(MANARegen(mana));
        spellDamageAmpli = (0.07f * intelligence / 100);

        baseDamage = startingBaseDamage;

        currentState = State.Idle;
    }

    private void Update()
    {
            
    }

    IEnumerator HPRegen(float tempHealth)
    {
        while(true)
        {
            //Debug.Log("HP Regen Started");

            if (health < tempHealth)
            {
                health += healthRegen;

                healthBar.fillAmount = health / maxHealth;
                //Debug.Log(health);
                yield return new WaitForSeconds(1f);
            }
            else
            {
                health = tempHealth;

                healthBar.fillAmount = health / maxHealth;
                yield return null;
            }                
        }
    }

    IEnumerator MANARegen(float tempMana)
    {
        while (true)
        {
            //Debug.Log("HP Regen Started");

            if (mana < tempMana)
            {
                mana += manaRegen;
                manaBar.fillAmount = mana / maxMana;
                //Debug.Log(mana);
                yield return new WaitForSeconds(1f);
            }
            else
            {
                mana = tempMana;
                manaBar.fillAmount = mana / maxMana;
                yield return null;
            }
        }
    }

    public virtual void TakeHit(float damage, Vector3 attackerDirection, string damageType, float spellAmpli)
    {
        //Debug.Log(damage);
        //Debug.Log(hitDirection);
        //TakeDamage(damage);

        //if(attackerString == "none")
        //{
        //    //Debug.Log(attackerDirection.y);
        //    if (damageType == "physical")
        //        TakePhysicalDamage(damage);
        //
        //    else if (damageType == "magical")
        //        TakeMagicalDamage(damage, spellAmpli);
        //}

        if (gameObject.GetComponent<BristleBackPassive>())
            gameObject.GetComponent<BristleBackPassive>().TakeHit(damage, attackerDirection, damageType, spellAmpli);

        else if (gameObject.GetComponent<CounterHelix>())
        {
            gameObject.GetComponent<CounterHelix>().PerformHelix();

            if (damageType == "physical")
                TakePhysicalDamage(damage);

            else if (damageType == "magical")
                TakeMagicalDamage(damage, spellAmpli);

            else if (damageType == "pure")
                TakePureDamage(damage);
        }

        else
        {
            if (damageType == "physical")
                TakePhysicalDamage(damage);

            else if (damageType == "magical")
                TakeMagicalDamage(damage, spellAmpli);

            else if (damageType == "pure")
                TakePureDamage(damage);
        }

        //if(attackerString == "BristleBack")
        //{
        //    //Debug.Log(attackerDirection.y);
        //    float hitDirection = (180 + attackerDirection.y) - transform.localEulerAngles.y;
        //    float reducedDamage;
        //
        //    if((hitDirection > 70 && hitDirection < 110) || (hitDirection > -110 && hitDirection < -70))
        //    {
        //        if(damageType == "physical")
        //        {
        //            reducedDamage = damage * (1 - (8f / 100));
        //            //Debug.Log("Side Damage" + " " + reducedDamage);
        //            TakePhysicalDamage(reducedDamage);
        //        }
        //
        //        else if(damageType == "magical")
        //        {
        //            reducedDamage = damage * (1 - (8f / 100));
        //            //Debug.Log("Side Damage" + " " + reducedDamage);
        //            TakeMagicalDamage(reducedDamage, spellAmpli);
        //        }                
        //    }
        //    else if((hitDirection > 110 && hitDirection < 200) || (hitDirection > -200 && hitDirection < -110))
        //    {
        //        if(damage > 20)
        //        {
        //            GetComponent<QuillSprayAbility>().SprayQuill(false);
        //        }
        //
        //        if (damageType == "physical")
        //        {
        //            reducedDamage = damage * (1 - (16f / 100));
        //            //Debug.Log("Rear Damage" + " " + reducedDamage);
        //            TakePhysicalDamage(reducedDamage);
        //        }
        //
        //        else if (damageType == "magical")
        //        {
        //            reducedDamage = damage * (1 - (16f / 100));
        //            //Debug.Log("Rear Damage" + " " + reducedDamage);
        //            TakeMagicalDamage(reducedDamage, spellAmpli);
        //        }
        //    }
        //    else
        //    {
        //        if (damageType == "physical")
        //        {
        //            //reducedDamage = damage * (1 - (8 / 100));
        //            //Debug.Log("Front Damage");
        //            TakePhysicalDamage(damage);
        //        }
        //
        //        else if (damageType == "magical")
        //        {
        //            //reducedDamage = damage * (1 - (8 / 100));
        //            //Debug.Log("Front Damage");
        //            TakeMagicalDamage(damage, spellAmpli);
        //        }
        //    }
        //}
    }

    public virtual void TakePhysicalDamage(float damage)
    {
        float damageWithArmor = damage * (1 - ((0.052f * armor) / (0.9f + 0.048f * Mathf.Abs(armor))));
        //GameObject text = Instantiate(damageText, canvas) as GameObject;
        //Canvas canvasMain = this.GetComponentInChildren<Canvas>();
        //canvasMain.transform.GetChild(2).GetComponentInChildren<Text>().text = Mathf.Floor(damageWithArmor).ToString();

        //Debug.Log(damageWithArmor);
        health -= damageWithArmor;

        healthBar.fillAmount = health / maxHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public virtual void TakeMagicalDamage(float damage, float spellAmplification)
    {
        float magicalDamage = damage + (damage * spellAmplification);
        float damageAfterMagicalResistance = magicalDamage * (1 - startingMegicResistant) * (1 - magicResistant); 
        health -= damageAfterMagicalResistance;

        healthBar.fillAmount = health / maxHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public virtual void TakePureDamage(float damage)
    {
        health -= damage;

        healthBar.fillAmount = health / maxHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        isDead = true;
        if (OnDeath != null)
            OnDeath();

        Destroy(gameObject);
    }

    public void ChangeAttackSpeed(float amount)
    {
        attackSpeed += amount;
    }

    public void ChangeArmor(float amount)
    {
        armor += amount;
        //Debug.Log(armor + " " + transform.name);
    }

    public void ChangeAttackDamage(float amount)
    {
        baseDamage += amount;
    }

    public void ChangeMoveSpeed(float speed)
    {
        movementSpeed = (movementSpeed * (1 + (speed / 100)));
        //Debug.Log(movementSpeed + " " + transform.name);
    }

    public void ChangeMana(float manaAmount)
    {
        mana += manaAmount;

        manaBar.fillAmount = mana / maxMana;
    }

    public virtual IEnumerator OnStun(float time)
    {
        if(currentState != State.Stunned)
            currentState = State.Stunned;

        //stateBar.fillAmount = 1;

        Canvas canvas = this.GetComponentInChildren<Canvas>();
        canvas.transform.GetChild(1).GetComponentInChildren<Image>().enabled = true;
        canvas.transform.GetChild(1).GetComponentInChildren<Text>().text = "STUNNED";

        //stateText.text = "STUNNED";

        float startTime = 0;

        while (startTime <= time)
        {
            startTime += Time.deltaTime;
            //Debug.Log(startTime);
            //canvas.transform.GetChild(1).GetComponentInChildren<Image>().fillAmount = startTime / time;
            //freeze player position
            //can't attack
            //can't use active abilities and use passive abilities 
            //can't use items
            //stop channeling
            yield return null;
        }

        canvas.transform.GetChild(1).GetComponentInChildren<Text>().text = null;

        //canvas.transform.GetChild(1).GetComponentInChildren<Image>().enabled = false;
        currentState = State.Idle;
    }

    public virtual IEnumerator OnSilence(float time)
    {
        if (currentState != State.Silenced)
            currentState = State.Silenced;

        float startTime = 0;

        if (startTime <= time)
        {
            startTime += Time.deltaTime;

            //can't use spell
            yield return null;
        }

        currentState = State.Idle;
    }

    public virtual IEnumerator OnBreak(float time)
    {
        if (currentState != State.Breaked)
            currentState = State.Breaked;

        float startTime = 0;

        if (startTime <= time)
        {
            startTime += Time.deltaTime;

            //can't attack
            yield return null;
        }

        currentState = State.Idle;
    }

    public virtual IEnumerator OnRoot(float time)
    {
        if (currentState != State.Rooted)
            currentState = State.Rooted;

        float startTime = 0;

        while (startTime <= time)
        {
            startTime += Time.deltaTime;

            //freeze player position
            //stop channeling
            yield return null;
        }

        currentState = State.Idle;
    }
}
