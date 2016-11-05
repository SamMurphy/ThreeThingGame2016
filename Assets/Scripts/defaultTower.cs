using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class defaultTower : MonoBehaviour
{

    GameObject currentTarget = null;
    public float range = 10f;
    public GameObject[] enemies;
    public Projectile projectile;
	public float msBetweenShots = 500f;
	float nextShotTime;
	public float shotSpeed;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Do we have a target?
        // If not, try and find one
        if (currentTarget == null)
        {
            float closestDistance = 0;
            GameObject potentialTarget = null;
            // Searches through all enemies and finds closest
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Vector3 distance = transform.position - enemy.transform.position;
				if (distance.magnitude > closestDistance && distance.magnitude < range)
                    potentialTarget = enemy;
            }

            // If a enemy is found, make them our target
			if (potentialTarget != null) 
			{
				currentTarget = potentialTarget;
				nextShotTime = Time.time + msBetweenShots / 1000f;
			}
        }
        else if (currentTarget != null)
        {
            Vector3 distance = transform.position - currentTarget.transform.position;
            if (distance.magnitude > range)
            {
                currentTarget = null;
            }
            else
            {
                transform.LookAt(currentTarget.transform);
				if (Time.time > nextShotTime)
                {
					nextShotTime = Time.time + msBetweenShots / 1000f;
                    Projectile bullet = Instantiate(projectile, transform.forward * 1.1f + transform.position, transform.rotation) as Projectile;
					bullet.SetSpeed(shotSpeed);
                }
            }
        }

    }
}
