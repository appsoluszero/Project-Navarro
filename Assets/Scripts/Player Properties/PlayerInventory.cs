using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Weapon playerWeapon;
    public int leftinmagazine;
    public int totalammo;
    void Start()
    {
        leftinmagazine = playerWeapon.magazinesize;
        totalammo = playerWeapon.totalammo;
    }
}
