using UnityEngine;
using System.Collections;

public class ZombieProperties : MonoBehaviour
{
    [HideInInspector]
    public bool isLive = true;
    public int maxLife = 100;
    public int hp;
    public int outExperience = 1;
    public int damage;
    public float hitTime;

    void Awake()
    {
        hp = maxLife;
    }
}
