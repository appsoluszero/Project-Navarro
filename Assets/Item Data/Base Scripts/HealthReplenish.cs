using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "Item/Replenishment/Health")]
public class HealthReplenish : ReplenishmentBase
{
    public override void Replenish()
    {
        Debug.Log("HEALTH REPLENISH!");
    }
}
