using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public interface IDamagable
{
    void recieveDamage(float dis, float maxdis, float effdis, float damage);
}
