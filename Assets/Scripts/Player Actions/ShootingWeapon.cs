using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShootingWeapon : MonoBehaviour
{
    [SerializeField] private InputManagerShooting shootingInput;
    [SerializeField] private GameObject muzzleLight;
    [SerializeField] private LayerMask enemyLayer;
    //[SerializeField] private LayerMask physicObjectLayer;
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
        if(inventory.playerWeapon.checkType() == weaponType.Burst)
            delayNextBurst = inventory.playerWeapon.GetTimeBeforeNextBurst();
    }

    #region SHOOTING
    void handleShooting(object sender, EventArgs e)
    {
        weaponType type = inventory.playerWeapon.checkType();
        float damage = inventory.playerWeapon.damage;
        float effectiverange = inventory.playerWeapon.effectiverange;
        float actualrange = inventory.playerWeapon.actualrange;
        float accuracy = inventory.playerWeapon.accuracy;
        int pellet = inventory.playerWeapon.pelletnumber;
        ShootingSequence(type, damage, effectiverange, actualrange, accuracy, pellet, inventory.playerWeapon.GetNumShotPerBurst());
    }
    void ShootingSequence(weaponType type, float damage, float effectiverange, float actualrange, float accuracy, int pellet, int cntShot)
    {
        if(inventory.leftinmagazine > 0)
        {
            for(int i = 0;i<pellet;++i)
            {
                float inaccurateAngle = Random.Range(((100f - accuracy) / 100f)*45f + 3f, -((100f - accuracy) / 100f)*45f - 3f);
                Vector2 currPos = (Vector2)transform.position;
                Vector2 pelletVector = new Vector3(Mathf.Cos((view.angle+90f+inaccurateAngle)*Mathf.Deg2Rad), Mathf.Sin((view.angle+90f+inaccurateAngle)*Mathf.Deg2Rad), 0f).normalized;
                RaycastHit2D enemyHit = Physics2D.Raycast(transform.position, pelletVector, actualrange, enemyLayer);
                //RaycastHit2D physicObjectHit = Physics2D.Raycast(transform.position, pelletVector, actualrange, physicObjectLayer);
                if(enemyHit)
                {
                    if(enemyHit.transform.GetComponent<IDamagable>() != null)
                        enemyHit.transform.GetComponent<IDamagable>().recieveDamage((enemyHit.point - (Vector2)transform.position).magnitude, actualrange, effectiverange, damage);
                    Debug.DrawLine(currPos, enemyHit.point, Color.red, 10f);
                }
                else
                {
                    Debug.DrawLine(currPos, currPos + pelletVector * actualrange, Color.red, 10f);
                }
                /*if(physicObjectHit)
                {
                    physicObjectHit.transform.GetComponent<IPhysicsObject>().recieveForce(physicObjectHit.point - (Vector2)transform.position, physicObjectHit.point);
                }*/
                //Debug.DrawLine(currPos, currPos + pelletVector * effectiverange , Color.red, 10f);
                //Debug.DrawLine(currPos + pelletVector * effectiverange, currPos + pelletVector * actualrange, Color.green, 10f);
            }
            inventory.leftinmagazine -= 1;
            inventory.totalammo -= 1;
            //MAKE SURE THE AMMO DOESN'T REACH ZERO
            inventory.leftinmagazine = Mathf.Clamp(inventory.leftinmagazine, 0, inventory.leftinmagazine);
            shootingInput.OnReloading -= reloadWeapon;
            shootingInput.OnShoot -= handleShooting;
            MuzzleLightHandle();
            if(type == weaponType.Burst)
                StartCoroutine(countDownDelayBurst(type, damage, effectiverange, actualrange, accuracy, pellet, cntShot));
            else
                StartCoroutine(countDownDelay());
            
        }
    }
    IEnumerator countDownDelayBurst(weaponType type, float damage, float effectiverange, float actualrange, float accuracy, int pellet, int cntShot)
    {
        if(cntShot > 1)
        {
            yield return new WaitForSeconds(delayBetweenShot);
            cntShot--;
            ShootingSequence(type, damage, effectiverange, actualrange, accuracy, pellet, cntShot);
        }
        else
        {
            yield return new WaitForSeconds(delayNextBurst);
            shootingInput.OnReloading += reloadWeapon;
            shootingInput.OnShoot += handleShooting;
        }
    }
    IEnumerator countDownDelay()
    {
        yield return new WaitForSeconds(delayBetweenShot);
        shootingInput.OnReloading += reloadWeapon;
        shootingInput.OnShoot += handleShooting;
    }
    #endregion

    #region RELOADING
    void reloadWeapon(object sender, EventArgs e)
    {
        if(inventory.leftinmagazine < inventory.playerWeapon.magazinesize && inventory.totalammo > 0)
        {
            inventory.leftinmagazine = Mathf.Clamp(inventory.totalammo, 0, inventory.playerWeapon.magazinesize);
            Debug.Log("Reloaded!");
        }
    }
    #endregion

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
