using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public GameObject basicTower_1;
	public GameObject basicTower_2;
	public GameObject sniperTower_1;
	public GameObject sniperTower_2;
	public GameObject autoTower_1;
	public GameObject autoTower_2;
	public GameObject bounceTower_1;
	public GameObject bounceTower_2;

	List<GameObject> towers = new List<GameObject> ();

	public GameObject spawner_1;
	public GameObject spawner_2;
	public GameObject spawner_3;
	public GameObject spawner_4;

	List<GameObject> spawners = new List<GameObject> ();

	public GameObject player;
	PlayerScript playerScript;

	public float nextStageTimer = 15000;

	float nextTowerSpawn;
	float nextZombieWave;

	// Use this for initialization
	void Start () {
		nextTowerSpawn = Time.time + nextStageTimer/ 1000f;
		nextZombieWave = Time.time + nextStageTimer/ 500f;

		basicTower_2.SetActive (false);
		sniperTower_1.SetActive (false);
		sniperTower_2.SetActive (false);
		autoTower_1.SetActive (false);
		autoTower_2.SetActive (false);
		bounceTower_1.SetActive (false);
		bounceTower_2.SetActive (false);

		towers.Add (sniperTower_1);
		towers.Add (autoTower_1);
		towers.Add (autoTower_1);
		towers.Add (bounceTower_1);
		towers.Add (basicTower_2);
		towers.Add (sniperTower_2);
		towers.Add (autoTower_2);
		towers.Add (bounceTower_2);

		spawner_2.SetActive (false);
		spawner_3.SetActive (false);
		spawner_4.SetActive (false);

		spawners.Add (spawner_2);
		spawners.Add (spawner_3);
		spawners.Add (spawner_4);


	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextTowerSpawn && towers.Count > 0) 
		{		
			towers [0].SetActive (true);
			towers.RemoveAt (0);
			nextTowerSpawn = Time.time + nextStageTimer/ 1000f;
		}

		if (Time.time > nextZombieWave && spawners.Count > 0) 
		{		
			spawners [0].SetActive (true);
			spawners.RemoveAt (0);
			nextZombieWave = Time.time + nextStageTimer/ 500f;
		}
			
	}
}
