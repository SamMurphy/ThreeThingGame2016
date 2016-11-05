using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour, IDamageable
{
    // When creating enemy, be sure to call SetTarget() and pass the gameobject the enemy will attack.

    public LayerMask collisionMask;

    // Ranged attacks
    public bool rangedCharacter;
    public float range;
    public float fireRate;
    public GameObject bullet;
    public float bulletSpeed;
    private float currentFireTime;
    private bool throwing = false;

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
    private Transform[] bodyParts; // 0 = head, 1 = body, 2 = left hand, 3 = right hand
    
	// Use this for initialization
	void Start ()
    {
        Health = maxHealth;
        currentFireTime = 60;
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
            if (throwing)
                rangedAttack();
            else if (Vector3.Distance(transform.position, target.transform.position) < range) // If in range
            {
                RaycastHit hit;
                Ray ray = new Ray(transform.position, (target.transform.position - transform.position));
                if (Physics.Raycast(ray, out hit, range, collisionMask)) // If there is a line of sight
                {
                    if (hit.transform.gameObject == target)
                    {
                        // Attack
                        if (rangedCharacter)
                        {
                            throwing = true;
                            currentFireTime = 0;
                        }
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
                GetComponent<NavMeshAgent>().destination = target.transform.position;
                GetComponent<Animator>().SetBool("melee", false);
                GetComponent<Animator>().SetBool("ranged", false);
            }
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

    private void rangedAttack() // The animation is schedueled to throw projectile at the 60th frame of animation, animation ends at the 120th frame.
    {
        transform.LookAt(target.transform.position);
        GetComponent<Animator>().SetBool("ranged", true);
        ++currentFireTime;
        if (currentFireTime == fireRate/2) // When animation is half way through, fire projectile.
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Projectile p = Instantiate(bullet, bodyParts[3].transform.position, bodyParts[3].transform.rotation) as Projectile;
            p.SetSpeed(bulletSpeed);            
        }
        else if(currentFireTime >= fireRate) // When animation is complete, reset.
        {
            currentFireTime = 0;
            throwing = false;
        }
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
