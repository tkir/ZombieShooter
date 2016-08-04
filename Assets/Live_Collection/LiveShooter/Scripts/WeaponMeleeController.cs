using UnityEngine;
using System.Collections;

public class WeaponMeleeController : WeaponController
{
    void Start()
    {
        wParam = this.GetComponent<WeaponParameters>();
        GetComponent<Animation>()[wParam.shootAnimationClip.name].speed = wParam.shootAnimationClipSpeed;
        shootAudioSorce = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (afterShootTime > 0) afterShootTime -= Time.fixedDeltaTime;
    }

    public override void OnShoot(GameObject FPC)
    {
        //анимация выстрела
        GetComponent<Animation>().Play(wParam.shootAnimationClip.name);
        //звук пули
        shootAudioSorce.PlayOneShot(wParam.shootAudio);

        //ставим задержку перед следующим выстрелом
        afterShootTime = wParam.shootDelay;
        //SparkActiv(true);
    }

    public override void RechargeWeapon(GameObject holder)
    {

    }
}
