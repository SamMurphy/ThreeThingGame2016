using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GunController))]
public class PlayerScript : MonoBehaviour {

    GunController gunController;

	// Use this for initialization
	void Start () {
        gunController = GetComponent<GunController>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetMouseButton(0))
        {
            gunController.OnTriggerHold();
        }
        if (Input.GetMouseButtonUp(0))
        {
            gunController.OnTriggerReleased();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gunController.Reload();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunController.EquipGun(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunController.EquipGun(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gunController.EquipGun(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            gunController.EquipGun(3);
        }
    }
}
