﻿using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Item/Weapon")]
public class Weapon : ScriptableObject
{
    public string officialname;
    public weaponType type;
    //pure damage
    public float damage;
    //most guns stay at 1, except for shotgun with multiples
    public int pelletnumber;
    //a raycast range for each bullets with full damage
    public float effectiverange;
    //a raycast range for each bullets before disappearing
    public float actualrange;
    //the higher the number, the smaller the sway width
    //0 -> max sway 96 degree 100 -> max sway 6 degree 
    public float accuracy;
    //self-explanatory
    public int magazinesize;
    //in both the gun and the reserve
    public int totalammo;
    [Header("For automatic/non-automatic")]
    //shot per minute
    public float firerate;
    [Header("For burst")]
    public float timeBetweenShot;
    public float timeBeforeNextBurst;
    public int numShotPerBurst;

}

public enum weaponType 
{
    Automatic,
    NonAutomatic,
    Burst
}
