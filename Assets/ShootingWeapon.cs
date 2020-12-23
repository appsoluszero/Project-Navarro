using System;
using System.Collections;
using UnityEngine;

public class ShootingWeapon : MonoBehaviour
{
    [SerializeField] private InputManagerShooting shootingInput;
    private PlayerInventory inventory;
    private float delayBetweenShot;
    void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
    }
    void Start()
    {
        shootingInput.OnShoot += shootWeapon;
        shootingInput.OnReloading += reloadWeapon;
        delayBetweenShot = 1.0f/(inventory.playerWeapon.firerate/60.0f);
    }
    void shootWeapon(object sender, EventArgs e)
    {
        if(inventory.leftinmagazine > 0)
        {
            inventory.leftinmagazine -= 1;
            inventory.totalammo -= 1;
            Debug.Log(inventory.leftinmagazine.ToString() + "/" + inventory.playerWeapon.magazinesize.ToString() + " Total left = " + inventory.totalammo.ToString());
            shootingInput.OnShoot -= shootWeapon;
            StartCoroutine(countDownDelay());
        }
    }
    IEnumerator countDownDelay()
    {
        yield return new WaitForSeconds(delayBetweenShot);
        shootingInput.OnShoot += shootWeapon;
    }
    void reloadWeapon(object sender, EventArgs e)
    {
        if(inventory.leftinmagazine < inventory.playerWeapon.magazinesize && inventory.totalammo > 0)
        {
            inventory.leftinmagazine = Mathf.Clamp(inventory.totalammo, 0, inventory.playerWeapon.magazinesize);
            Debug.Log("Reloaded!");
        }
    }
}
