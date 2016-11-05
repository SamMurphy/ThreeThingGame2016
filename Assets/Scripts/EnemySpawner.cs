using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public EnemyScript enemyInstance;
	public GameObject defaultTarget;
	public float msBetweenSpawns = 4000f;
	float nextSpawnTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawnTime) 
		{
			nextSpawnTime = Time.time + msBetweenSpawns / 1000f;
			EnemyScript enemy = Instantiate (enemyInstance, transform.position, transform.rotation) as EnemyScript;
			enemy.SetTarget (defaultTarget);
		} 
	}
}
