using UnityEngine;
using System.Collections;

public class GravityBomb : MonoBehaviour {

    public float Radius = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Collider[] collisions = Physics.OverlapSphere(transform.position, Radius);
        if (collisions.Length > 0)
        {
            foreach (Collider col in collisions)
            {
                if (col.gameObject == gameObject) continue;
                EnemyScript enemy = col.GetComponent<EnemyScript>();
                if (enemy == null) enemy = col.GetComponentInParent<EnemyScript>();
                if (enemy != null) enemy.GravityDeath(col.transform.position - transform.position);
            }
        }
    }
}
