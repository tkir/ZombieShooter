using UnityEngine;
using System.Collections.Generic;

public class FPC_Equipment : MonoBehaviour
{

    public GameObject weapon;
    public GameObject armor;
    //[HideInInspector]
    public List<GameObject> weaponList;
    //[HideInInspector]
    public List<GameObject> ammunitionList;
    public List<GameObject> bullets;

    private void Awake()
    {
        weaponList = new List<GameObject>();
        weaponList.Add(weapon);

        ammunitionList = new List<GameObject>();
        bullets = new List<GameObject>();

    }

    void FixedUpdate()
    {
        ControllBullets();
    }

    public void AddBullet(GameObject bullet)
    {
        bullets.Add(bullet);
    }

    private void ControllBullets()
    {
        for (int i = 0; i < bullets.Count; ++i)
        {
            if (bullets[i].GetComponent<BulletController2>().isLive == false)
            {
                this.GetComponent<FPC_Properties>().experience += bullets[i].GetComponent<BulletController2>().experience;
                bullets[i].GetComponent<BulletController2>().DestroyBullet();
                bullets.Remove(bullets[i]);
            }
        }
    }
}