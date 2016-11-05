using UnityEngine;
using System.Collections;

public class BounceBombProjectile : Projectile {

	public ProjectileBouncing shrapnel;
	public int amountOfShrapnel = 5;

	void Start()
	{
		Destroy(gameObject, lifetime);

		Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
		if (initialCollisions.Length > 0)
		{
			OnHitObject(initialCollisions[0], transform.position);
		}

		GetComponent<TrailRenderer>().material.SetColor("_Color", trailColour);
	}

	void Update()
	{
		float moveDistance = speed * Time.deltaTime;
		CheckCollisions(moveDistance);
		transform.Translate(Vector3.forward * moveDistance);
	}

	protected void CheckCollisions(float moveDistance)
	{
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide))
		{
			OnHitObject(hit.collider, hit.point);
		}
	}

	virtual protected void OnHitObject(Collider c, Vector3 hitPoint)
	{
		IDamageable damageableObject = c.GetComponent<IDamageable>();
		if (damageableObject != null)
		{
			damageableObject.TakeHit(damage, hitPoint, transform.forward);
		}
		GameObject.Destroy(gameObject);
	}


	void OnDestroy()
	{
		for (int i = 0; i < amountOfShrapnel; i++) 
		{
			ProjectileBouncing bouncy = Instantiate (shrapnel, transform.position, Random.rotation) as ProjectileBouncing;
			bouncy.SetSpeed (10f);

		}
	}
}
