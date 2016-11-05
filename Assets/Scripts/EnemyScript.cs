using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour, IDamageable
{
    public LayerMask collisionMask;

    public bool rangedCharacter;
    public float range;
    public float fireRate;
    private float currentFireTime;

    public float maxHealth;
    private float Health;

    private bool playerIsTarget;
    public GameObject target;

    // Gravity Death
    private bool gravityDeath = false;
    private Vector3 gravityDeathVector;
    private float gravityCount = 0;
    public float gravityCountMax;

    // Disintegration Death
    private bool disintegrationDeath = false;
    private Transform[] bodyParts;
    
	// Use this for initialization
	void Start ()
    {
        Health = maxHealth;
        bodyParts = GetComponentsInChildren<Transform>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gravityDeath)
        {
            transform.position += gravityDeathVector * Time.deltaTime;
            transform.Rotate(gravityDeathVector);
            gravityCount++;
            if (gravityCount >= gravityCountMax)
                Death();
        }
        else if(disintegrationDeath)
        {
            TakeDamage(1.0f);
        }
        else
        {
            GetComponent<NavMeshAgent>().destination = target.transform.position;
            if (Vector3.Distance(transform.position, target.transform.position) < range) // If in range
            {
                RaycastHit hit;
                Ray ray = new Ray(transform.position, (target.transform.position - transform.position));
                if (Physics.Raycast(ray, out hit, range, collisionMask)) // If there is a line of sight
                {
                    if (hit.transform.gameObject == target)
                    {
                        // Attack
                        if (rangedCharacter)
                            rangedAttack();
                        else
                            meleeAttack();
                    }
                    else
                    {
                        GetComponent<Animator>().SetBool("melee", false);
                        GetComponent<Animator>().SetBool("ranged", false);
                    }
                }
            }
            else
            {
                GetComponent<Animator>().SetBool("melee", false);
                GetComponent<Animator>().SetBool("ranged", false);
            }

            if (currentFireTime > 0)
                --currentFireTime;
        }
    }

    public void SetTarget(GameObject obj)
    {
        target = obj;
        GetComponent<NavMeshAgent>().destination = target.transform.position;
    }

    public void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        //if (damage >= Health)
        //    Destroy(Instantiate(deathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, deathEffect.startLifetime);
        GravityDeath(hitDirection);
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        //if (Health <= 0) DisintegrationDeath();
    }

    public void GravityDeath(Vector3 projVec)
    {
        gravityDeath = true;
        gravityDeathVector = projVec.normalized;
        GetComponent<NavMeshAgent>().enabled = false;
    }

    public void DisintegrationDeath()
    {
        disintegrationDeath = true;
        GetComponent<NavMeshAgent>().enabled = false;
    }

    private void meleeAttack()
    {
        GetComponent<Animator>().SetBool("melee", true);
        // TODO call takeDamage() on player.
    }

    private void rangedAttack()
    {
        GetComponent<Animator>().SetBool("ranged", true);
        // TODO create projectile aimed at the player.
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
