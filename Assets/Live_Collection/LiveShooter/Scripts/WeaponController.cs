using UnityEngine;
using System.Threading;

public class WeaponController : MonoBehaviour
{
    protected WeaponParameters wParam;
    protected AudioSource shootAudioSorce;

    public GameObject Bullet;

    private bool isRecharging = false;
    private float afterRechargeTime = 0;
    [HideInInspector]
    public float afterShootTime = 0;

    void Start()
    {
        wParam = this.GetComponent<WeaponParameters>();
        GetComponent<Animation>()[wParam.shootAnimationClip.name].speed = wParam.shootAnimationClipSpeed;
        shootAudioSorce = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (afterShootTime > 0) afterShootTime -= Time.fixedDeltaTime;
        //else SparkActiv(false);

        if (afterRechargeTime > 0) afterRechargeTime -= Time.fixedDeltaTime;
        else isRecharging = false;
    }

    /// <summary>
    /// Выстрл
    /// </summary>
    /// <param name="FPC">кто стреляет</param>
    public virtual void OnShoot(GameObject FPC)
    {
        if (isRecharging) return;

        //если нет патронов return
        if (wParam.holderCount <= 0) return;
        //анимация выстрела
        GetComponent<Animation>().Play(wParam.shootAnimationClip.name);

        if (Bullet != null)
        {
            //создаем пулю, добавляем повреждение (свойство оружия), добавляем пулю в лист пуль игрока
            GameObject bullet = (GameObject)Instantiate(Bullet, wParam.MainCamera.transform.position, wParam.MainCamera.transform.rotation);
            bullet.GetComponent<BulletController2>().damage = wParam.damage;
            FPC.GetComponent<FPC_Equipment>().AddBullet(bullet);

            //уменьшаем пули в обойме (если не ручное оружие)
            if (wParam.type != "melee")
                wParam.holderCount--;
        }
        //звук пули
        shootAudioSorce.PlayOneShot(wParam.shootAudio);

        //ставим задержку перед следующим выстрелом
        afterShootTime = wParam.shootDelay;
        //SparkActiv(true);
    }

    public virtual void RechargeWeapon(GameObject holder)
    {
        isRecharging = true;
        afterRechargeTime = wParam.rechargeDelay;
        wParam.holderCount = holder.GetComponent<AmmunitionParameters>().ammunitionCapacity;
        GetComponent<Animation>()[wParam.rechargeAnimationClip.name].speed = wParam.rechargeAnimationSpeed;
        this.GetComponent<AudioSource>().PlayOneShot(wParam.rechargeAudio);
        this.GetComponent<Animation>().Play(wParam.rechargeAnimationClip.name);
    }

    //если к оружию привязана спарка, делаем ее активной
    //private void SparkActiv(bool isActive)
    //{
    //    if (spark != null) emitter.emit = isActive;
    //}
}