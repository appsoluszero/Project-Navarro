using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public interface IDamagable
{
    void DamageEvent(float dis, float maxdis, float effdis, float damage);
    void recieveDamage(float dis, float maxdis, float effdis, float damage);
}
