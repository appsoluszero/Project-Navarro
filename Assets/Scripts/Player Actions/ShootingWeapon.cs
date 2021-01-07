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
    void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
        view = GetComponent<PlayerView>();
    }
    void Start()
    {
        shootingInput.OnShoot += handleShooting;
        shootingInput.OnShoot += MuzzleLightHandle;
        shootingInput.OnReloading += reloadWeapon;
        delayBetweenShot = 1.0f/(inventory.playerWeapon.firerate/60.0f);
    }
    void handleShooting(object sender, InputManagerShooting.OnShootArgs e)
    {
        for(int i = 0;i<e.pellet;++i)
        {
            float inaccurateAngle = Random.Range(((100f - e.accuracy) / 100f)*45f + 3f, -((100f - e.accuracy) / 100f)*45f - 3f);
            Vector2 currPos = (Vector2)transform.position;
            Vector2 pelletVector = new Vector3(Mathf.Cos((view.angle+90f+inaccurateAngle)*Mathf.Deg2Rad), Mathf.Sin((view.angle+90f+inaccurateAngle)*Mathf.Deg2Rad), 0f).normalized;
            RaycastHit2D enemyHit = Physics2D.Raycast(transform.position, pelletVector, e.actualrange, enemyLayer);
            RaycastHit2D physicObjectHit = Physics2D.Raycast(transform.position, pelletVector, e.actualrange, physicObjectLayer);
            if(enemyHit)
            {
                enemyHit.transform.GetComponent<IDamagable>().recieveDamage((enemyHit.point - (Vector2)transform.position).magnitude, e.actualrange, e.effectiverange, e.damage);
            }
            if(physicObjectHit)
            {
                physicObjectHit.transform.GetComponent<IPhysicsObject>().recieveForce(physicObjectHit.point - (Vector2)transform.position, physicObjectHit.point);
            }
            Debug.DrawLine(currPos, currPos + pelletVector * e.effectiverange , Color.red, 10f);
            Debug.DrawLine(currPos + pelletVector * e.effectiverange, currPos + pelletVector * e.actualrange, Color.green, 10f);
        }
        inventory.leftinmagazine -= 1;
        inventory.totalammo -= 1;
        //Debug.Log(inventory.leftinmagazine.ToString() + "/" + inventory.playerWeapon.magazinesize.ToString() + " Total left = " + inventory.totalammo.ToString());
        shootingInput.OnReloading -= reloadWeapon;
        shootingInput.OnShoot -= handleShooting;
        shootingInput.OnShoot -= MuzzleLightHandle;
        StartCoroutine(countDownDelay());
    }
    IEnumerator countDownDelay()
    {
        yield return new WaitForSeconds(delayBetweenShot);
        shootingInput.OnReloading += reloadWeapon;
        shootingInput.OnShoot += handleShooting;
        shootingInput.OnShoot += MuzzleLightHandle;
    }
    void reloadWeapon(object sender, InputManagerShooting.OnReloadingArgs e)
    {
        inventory.leftinmagazine = Mathf.Clamp(inventory.totalammo, 0, e.magsize);
        Debug.Log("Reloaded!");
    }
    void MuzzleLightHandle(object sender, EventArgs e)
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
