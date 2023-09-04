using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyType
{
    Zombie,
    Animal
}

[CreateAssetMenu(fileName ="New Enemy",menuName ="Enemy/Create Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public string description;
    public enemyType Type;
    public float damage;
    public float speed;
    public float chaseSpeed;
    public float health;
    public float attackRange;
}
