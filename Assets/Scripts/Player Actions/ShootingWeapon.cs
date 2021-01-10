using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShootingWeapon : MonoBehaviour
{
    [SerializeField] private InputManagerShooting shootingInput;
    [SerializeField] private GameObject muzzleLight;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask physicObjectLayer;
    private PlayerView view;
    private PlayerInventory inventory;
    private float delayBetweenShot;
    private float delayNextBurst;
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
        if(inventory.playerWeapon.type == weaponType.Burst)
            delayNextBurst = inventory.playerWeapon.timeBeforeNextBurst;
    }
    void ShootingStuff(weaponType type, float damage, float effectiverange, float actualrange, float accuracy, int pellet, int cntShot)
    {
        if(inventory.leftinmagazine > 0)
        {
            for(int i = 0;i<pellet;++i)
            {
                float inaccurateAngle = Random.Range(((100f - accuracy) / 100f)*45f + 3f, -((100f - accuracy) / 100f)*45f - 3f);
                Vector2 currPos = (Vector2)transform.position;
                Vector2 pelletVector = new Vector3(Mathf.Cos((view.angle+90f+inaccurateAngle)*Mathf.Deg2Rad), Mathf.Sin((view.angle+90f+inaccurateAngle)*Mathf.Deg2Rad), 0f).normalized;
                RaycastHit2D enemyHit = Physics2D.Raycast(transform.position, pelletVector, actualrange, enemyLayer);
                RaycastHit2D physicObjectHit = Physics2D.Raycast(transform.position, pelletVector, actualrange, physicObjectLayer);
                if(enemyHit)
                {
                    enemyHit.transform.GetComponent<IDamagable>().recieveDamage((enemyHit.point - (Vector2)transform.position).magnitude, actualrange, effectiverange, damage);
                }
                if(physicObjectHit)
                {
                    physicObjectHit.transform.GetComponent<IPhysicsObject>().recieveForce(physicObjectHit.point - (Vector2)transform.position, physicObjectHit.point);
                }
                Debug.DrawLine(currPos, currPos + pelletVector * effectiverange , Color.red, 10f);
                Debug.DrawLine(currPos + pelletVector * effectiverange, currPos + pelletVector * actualrange, Color.green, 10f);
            }
            inventory.leftinmagazine -= 1;
            inventory.totalammo -= 1;
            //MAKE SURE THE AMMO DOESN'T REACH ZERO
            inventory.leftinmagazine = Mathf.Clamp(inventory.leftinmagazine, 0, inventory.leftinmagazine);
            shootingInput.OnReloading -= reloadWeapon;
            shootingInput.OnShoot -= handleShooting;
            MuzzleLightHandle();
            if(type == weaponType.Burst)
            {
                StartCoroutine(countDownDelayBrust(type, damage, effectiverange, actualrange, accuracy, pellet, cntShot));
            }
            else
                StartCoroutine(countDownDelay());
        }
    }
    void handleShooting(object sender, InputManagerShooting.OnShootArgs e)
    {
        weaponType type = e.type;
        float damage = e.damage;
        float effectiverange = e.effectiverange;
        float actualrange = e.actualrange;
        float accuracy = e.accuracy;
        int pellet = e.pellet;
        ShootingStuff(type, damage, effectiverange, actualrange, accuracy, pellet, 1);
    }
    IEnumerator countDownDelay()
    {
        yield return new WaitForSeconds(delayBetweenShot);
        shootingInput.OnReloading += reloadWeapon;
        shootingInput.OnShoot += handleShooting;
    }
    IEnumerator countDownDelayBrust(weaponType type, float damage, float effectiverange, float actualrange, float accuracy, int pellet, int cntShot)
    {
        if(cntShot < inventory.playerWeapon.numShotPerBurst)
        {
            yield return new WaitForSeconds(delayBetweenShot);
            cntShot++;
            ShootingStuff(type, damage, effectiverange, actualrange, accuracy, pellet, cntShot);
        }
        else
        {
            yield return new WaitForSeconds(delayNextBurst);
            shootingInput.OnReloading += reloadWeapon;
            shootingInput.OnShoot += handleShooting;
        }
    }
    void reloadWeapon(object sender, InputManagerShooting.OnReloadingArgs e)
    {
        if(inventory.leftinmagazine < e.magsize && inventory.totalammo > 0)
        {
            inventory.leftinmagazine = Mathf.Clamp(inventory.totalammo, 0, e.magsize);
            Debug.Log("Reloaded!");
        }
    }
    void MuzzleLightHandle()
    {
        muzzleLight.SetActive(true);
        StartCoroutine(countDownMuzzle());
    }
    IEnumerator countDownMuzzle()
    {
        yield return new WaitForSeconds(0.05f);
        muzzleLight.SetActive(false);
    }
}
