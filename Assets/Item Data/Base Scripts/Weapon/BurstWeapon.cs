using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Burst Weapon", menuName = "Item/Weapon/Burst")]
public class BurstWeapon : WeaponBase
{
    public int numShotPerBurst;
    public float timeBeforeNextBurst;
    public override weaponType checkType()
    {
        return weaponType.Burst;
    }
    public override int GetNumShotPerBurst()
    {
        return numShotPerBurst;
    }
    public override float GetTimeBeforeNextBurst()
    {
        return timeBeforeNextBurst;
    }
}
