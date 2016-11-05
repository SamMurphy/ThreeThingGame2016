using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GunController))]
public class PlayerScript : MonoBehaviour {

    GunController gunController;
	public GameObject gameOverUI;
	bool gameOver = false;
	public int maxHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public float flashSpeed = 5f;
	public int score = 0;
	public Color damageColor = new Color (1f, 0f, 0f, 0.1f);
	public Text scoreText;
	public Text finalScoreText;
	bool damaged;
	public float deathTime = 3000f;
	float timeTilDeath;

	// Use this for initialization
	void Start () {
        gunController = GetComponent<GunController>();
		currentHealth = maxHealth;
		healthSlider.maxValue = maxHealth;
		healthSlider.value = currentHealth;
	}

	public void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	// Update is called once per frame
	void Update () {
		if (!gameOver) {
			if (damaged) {
				damageImage.color = damageColor;
			} else {
				damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
			}
			damaged = false;

			if (Input.GetMouseButton (0)) {
				gunController.OnTriggerHold ();
			}
			if (Input.GetMouseButtonUp (0)) {
				gunController.OnTriggerReleased ();
			}

			if (Input.GetKeyDown (KeyCode.R)) {
				gunController.Reload ();
			}

			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				gunController.EquipGun (0);
			}
			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				gunController.EquipGun (1);
			}
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				gunController.EquipGun (2);
			}
			if (Input.GetKeyDown (KeyCode.Alpha4)) {
				gunController.EquipGun (3);
			}
		} else {
			if (Time.time > timeTilDeath) {
				SceneManager.LoadScene (0, LoadSceneMode.Single);
			}
		}

	}

	void OnCollisionEnter(Collision collision)
	{
		GameObject collidedObject = collision.collider.gameObject;

		if (collidedObject.tag == "Enemy") {
			Debug.Log ("ENEMY CONTACT");
		}
	}

	public void TakeDamage (int amount)
	{
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;

		// Play hurt sound?

		if (currentHealth <= 0) 
		{
			gameOverUI.SetActive (true);
			finalScoreText.text = "Final Score: " + score;
			gameOver = true;
			timeTilDeath = Time.time + deathTime / 1000f;
		}
	}
}
