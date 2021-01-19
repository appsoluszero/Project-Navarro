using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public WeaponBase playerWeapon;
    public int leftinmagazine;
    public int totalammo;
    //public GadgetBase playerGadget;
    public ReplenishmentBase playerReplenishment;
    [SerializeField] private int ReplenishmentCharge;
    void Start()
    {
        leftinmagazine = playerWeapon.magazinesize;
        totalammo = playerWeapon.totalammo;
    }
}
