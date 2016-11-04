﻿using UnityEngine;
using System.Collections;
public class GunController : MonoBehaviour {

    public Transform weaponHold;
    public Gun[] Guns;
    public int CurrentWeaponIndex = 0;
    Gun equippedGun;

    public void Start()
    {
        if (Guns[0] != null)
            EquipGun(0);
    }

    public void Update()
    {
    }

    public void EquipGun(int weaponIndex)
    {
        if (weaponIndex >= 0 && weaponIndex <= Guns.Length)
        {
            EquipGun(Guns[weaponIndex]);
            CurrentWeaponIndex = weaponIndex;
        }
    }

	public void EquipGun(Gun gunToEquip)
    {
        if (equippedGun != null)
            Destroy(equippedGun.gameObject);

        equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation) as Gun;
        equippedGun.transform.parent = weaponHold;
    }

    public void OnTriggerHold()
    {
        if (equippedGun != null)
        {
            equippedGun.OnTriggerHold();
        }
    }

    public void OnTriggerReleased()
    {
        if (equippedGun != null)
        {
            equippedGun.OnTriggerReleased();
        }
    }

    public void Reload()
    {
        if (equippedGun != null)
        {
            equippedGun.Reload();
        }
    }
}
