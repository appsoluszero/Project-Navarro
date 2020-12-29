using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShootingWeapon : MonoBehaviour
{
    [SerializeField] private InputManagerShooting shootingInput;
    private PlayerView view;
    private PlayerInventory inventory;
    private float delayBetweenShot;
    void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
        view = GetComponent<PlayerView>();
    }
    void Start()
    {
        shootingInput.OnShoot += handleShooting;
        shootingInput.OnReloading += reloadWeapon;
        delayBetweenShot = 1.0f/(inventory.playerWeapon.firerate/60.0f);
    }
    void handleShooting(object sender, InputManagerShooting.OnShootArgs e)
    {
        if(inventory.leftinmagazine > 0)
        {
            for(int i = 0;i<e.pellet;++i)
            {
                float inaccurateAngle = Random.Range(((100f - e.accuracy) / 100f)*45f + 3f, -((100f - e.accuracy) / 100f)*45f - 3f);
                Vector3 pelletVector = new Vector3(Mathf.Cos((view.angle+90f+inaccurateAngle)*Mathf.Deg2Rad), Mathf.Sin((view.angle+90f+inaccurateAngle)*Mathf.Deg2Rad), 0f).normalized;
                Debug.DrawLine(transform.position, transform.position + pelletVector * e.effectiverange , Color.red, 10f);
                Debug.DrawLine(transform.position + pelletVector * e.effectiverange, transform.position + pelletVector * e.actualrange, Color.green, 10f);
            }
            inventory.leftinmagazine -= 1;
            inventory.totalammo -= 1;
            Debug.Log(inventory.leftinmagazine.ToString() + "/" + inventory.playerWeapon.magazinesize.ToString() + " Total left = " + inventory.totalammo.ToString());
            shootingInput.OnShoot -= handleShooting;
            StartCoroutine(countDownDelay());
        }
    }
    IEnumerator countDownDelay()
    {
        yield return new WaitForSeconds(delayBetweenShot);
        shootingInput.OnShoot += handleShooting;
    }
    void reloadWeapon(object sender, InputManagerShooting.OnReloadingArgs e)
    {
        if(inventory.leftinmagazine < e.magsize && inventory.totalammo > 0)
        {
            inventory.leftinmagazine = Mathf.Clamp(inventory.totalammo, 0, inventory.playerWeapon.magazinesize);
            Debug.Log("Reloaded!");
        }
    }
}
