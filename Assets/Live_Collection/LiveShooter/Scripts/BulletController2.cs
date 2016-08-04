using UnityEngine;
using System.Collections;

public class BulletController2 : MonoBehaviour {
    [HideInInspector]
    public bool isLive = true;
    [HideInInspector]
    public int experience = 0;
    [HideInInspector]
    public int damage;
    private float lifeTime = 0f;
    [HideInInspector]
    public Vector3 speed = new Vector3(0, 0, 1f);
    
    void Update()
    {
        if (isLive)
        {
            transform.Translate(speed);
            lifeTime += Time.deltaTime;
        }
        if (lifeTime >= 2f) isLive = false;         //через 2 секунды FPC_Equipment уничтожит пулю
    }

    public void Hit()
    {
        if (!isLive) return;
        this.GetComponent<BoxCollider>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
        this.isLive = false;
    }

    public void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
