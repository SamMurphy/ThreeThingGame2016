using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileBouncing : Projectile {

    Rigidbody rb;

	// Use this for initialization
	void Start () {
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
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
    }

    protected override void OnHitObject(Collider c, Vector3 hitPoint)
    {
        
    }


}
