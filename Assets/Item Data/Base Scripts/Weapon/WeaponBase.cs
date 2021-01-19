using UnityEngine;

public class WeaponBase : ScriptableObject
{
    public string officialname;
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
    //shot per minute
    public float firerate;
    public virtual int GetNumShotPerBurst() { return -100; }
    public virtual float GetTimeBeforeNextBurst() { return -100f; }
    public virtual weaponType checkType()
    {
        return weaponType.Generic;
    }

}

public enum weaponType 
{
    Generic,
    Automatic,
    NonAutomatic,
    Burst
}
