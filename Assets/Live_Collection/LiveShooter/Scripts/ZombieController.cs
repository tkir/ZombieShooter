using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour
{
    private bool isHitting = false;
    private float hittingTime = 0f;

    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("bullet"))
        {
            Debug.Log("попал!!!");
            Injury(col);

            col.GetComponent<BulletController2>().Hit();
        }
    }

    private void Injury(Collider col)
    {
        if (this.GetComponent<ZombieProperties>().hp > 0)
        {
            this.GetComponent<ZombieProperties>().hp -= col.GetComponent<BulletController2>().damage;     //повреждение
            if (this.GetComponent<ZombieProperties>().hp <= 0)
            {
                this.GetComponent<ZombieProperties>().isLive = false;
                col.GetComponent<BulletController2>().experience = this.GetComponent<ZombieProperties>().outExperience;
            }
        }
    }

    public void Hit(GameObject FPC)
    {
        if (FPC == null)
        {
            isHitting = false;
            return;
        }

        if (!isHitting)
        {
            isHitting = true;
            hittingTime = this.GetComponent<ZombieProperties>().hitTime;
            return;
        }

        hittingTime -= Time.deltaTime;
        if (hittingTime <= 0f)
        {
            hittingTime = 0f;
            isHitting = false;
            FPC.GetComponent<FPC_Controller>().Injury(this.GetComponent<ZombieProperties>().damage);
        }
    }
}
