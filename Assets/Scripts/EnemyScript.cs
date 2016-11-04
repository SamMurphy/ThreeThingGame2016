using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    public LayerMask collisionMask;

    public bool rangedCharacter;
    public float range;
    public float fireRate;
    private float currentFireTime;

    public float maxHealth;
    private float health;

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
        health = maxHealth;
        bodyParts = GetComponentsInChildren<Transform>();
        GetComponent<NavMeshAgent>().destination = target.transform.position;
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
            if (Vector3.Distance(transform.position, target.transform.position) < range)
            {
                RaycastHit hit;
                Ray ray = new Ray(transform.position, (target.transform.position - transform.position));
                if (Physics.Raycast(ray, out hit, range, collisionMask))
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
                    }
                }
            }

            if (currentFireTime > 0)
                --currentFireTime;
        }
    }

    public void SetTarget(GameObject obj)
    {
        target = obj;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Death();
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
    }

    private void rangedAttack()
    {

    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
