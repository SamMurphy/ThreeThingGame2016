using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileGravityBomb : Projectile {

    public GravityBomb gravityBomb;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        damage = 0;
	}

    public override void SetSpeed(float newSpeed)
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * newSpeed);
        base.SetSpeed(newSpeed);
    }

    // Update is called once per frame
    void Update () {
        transform.forward = rb.velocity;
        float moveDistance = rb.velocity.magnitude * Time.deltaTime;
        CheckCollisions(moveDistance);
    }

    protected override void OnHitObject(Collider c, Vector3 hitPoint)
    {

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Vector3.Distance(hitPoint, transform.position), collisionMask);

        GravityBomb gravBomb = Instantiate(gravityBomb, hitPoint, gravityBomb.transform.rotation) as GravityBomb;
        gravBomb.transform.up = hit.normal;

        IDamageable damageableObject = c.GetComponent<IDamageable>();
		if (damageableObject == null)
			damageableObject = c.GetComponentInParent<IDamageable> ();
        if (damageableObject != null)
        { 
            gravBomb.transform.parent = c.transform;
            damageableObject.TakeHit(damage, hitPoint, transform.forward);
        }

        Destroy(gameObject);
    }


}
