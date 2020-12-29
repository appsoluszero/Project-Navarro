using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDamage : MonoBehaviour, IDamagable
{
    public void DamageEvent(float dis, float maxdis, float effdis, float damage)
    {
        recieveDamage(dis, maxdis, effdis, damage);
    }
    public void recieveDamage(float dis, float maxdis, float effdis, float damage)
    {
        float percent;
        if(dis <= effdis) percent = 100f;
        else percent = (1f-((dis - effdis)/(maxdis - effdis)))*100f;
        float damagePoint = Mathf.FloorToInt((percent/100f)*damage);
        Debug.Log(damagePoint);
    }
}
