using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileBouncing : Projectile {

    Rigidbody rb;

	// Use this for initialization
	void Start () {
		lifetime = 6;
		damage = 50;
        Destroy(gameObject, lifetime);
        rb = GetComponent<Rigidbody>();
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
		IDamageable damageableObject = c.GetComponentInParent<IDamageable> ();
        if (damageableObject != null)
        {
            damageableObject.TakeHit(damage, hitPoint, transform.forward);
            GameObject.Destroy(gameObject);
        }
    }


}
