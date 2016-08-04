using UnityEngine;
using System.Collections;

public class WeaponMeleeController : WeaponController
{
    private GameObject bulletLocal;
    private float bulletLifeTime;

    void Start()
    {
        wParam = this.GetComponent<WeaponParameters>();
        GetComponent<Animation>()[wParam.shootAnimationClip.name].speed = wParam.shootAnimationClipSpeed;
        shootAudioSorce = GetComponent<AudioSource>();
        bulletLocal = null;
    }

    void Update()
    {
        if (afterShootTime > 0) afterShootTime -= Time.fixedDeltaTime;

        if (bulletLocal != null)
        {
            if (bulletLifeTime > 0) bulletLifeTime -= Time.deltaTime;
            else
            {
                bulletLocal.GetComponent<BulletController2>().isLive = false;
                bulletLocal = null;
            }
        }
    }

    public override void OnShoot(GameObject FPC)
    {
        //анимация выстрела
        GetComponent<Animation>().Play(wParam.shootAnimationClip.name);
        //звук пули
        shootAudioSorce.PlayOneShot(wParam.shootAudio);

        //ставим задержку перед следующим выстрелом
        afterShootTime = wParam.shootDelay;

        if (Bullet != null)
        {
            //создаем пулю, добавляем повреждение (свойство оружия), добавляем пулю в лист пуль игрока
            GameObject bullet = (GameObject)Instantiate(Bullet, this.transform.position, this.transform.rotation);
            bullet.GetComponent<BulletController2>().damage = wParam.damage;

            //пулю привязываем к оружию, ставим 0 скорость полета, растягиваем по размерам оружия
            bullet.GetComponent<BulletController2>().speed = Vector3.zero;
            bullet.transform.parent = this.transform;            
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x, bullet.transform.localScale.y, this.GetComponent<Renderer>().bounds.extents.z*1.42f/bullet.GetComponent<Renderer>().bounds.extents.z);
            bullet.transform.localPosition = new Vector3(bullet.transform.localPosition.x, bullet.transform.localPosition.y, this.GetComponent<Renderer>().bounds.extents.z*1.2f);


            bulletLocal = bullet;
            bulletLifeTime = wParam.shootAnimationClipSpeed;
            FPC.GetComponent<FPC_Equipment>().AddBullet(bullet);

            //уменьшаем пули в обойме (если не ручное оружие)
            if (wParam.type != "melee") wParam.holderCount--;
        }



        //SparkActiv(true);
    }

    public override void RechargeWeapon(GameObject holder){}
}
