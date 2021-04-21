using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{

    public float attackRate;
    public float attackDelay;
    public GameObject weaponObject;
    public float damage;
    public AudioClip initiateAttackSound, attackHitSound;

}
