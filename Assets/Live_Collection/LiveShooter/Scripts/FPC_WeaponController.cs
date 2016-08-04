using UnityEngine;
using System.Collections.Generic;

public class FPC_WeaponController : MonoBehaviour
{

    private List<GameObject> weaponList;
    public GameObject mainCamera;
    [HideInInspector]
    public GameObject activeWeapon;

    private void Start()
    {
        weaponList = this.GetComponent<FPC_Equipment>().weaponList;
        activeWeapon = this.GetComponent<FPC_Equipment>().weapon;
    }


    void Update()
    {
        if (Input.anyKey) HandleKey();
    }

    /// <summary>
    /// Обрабатываем нажатие кнопок мыши и клавиатуры
    /// </summary>
    void HandleKey()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        { ChangeWeapon(); }
        else if (Input.GetKeyDown(KeyCode.R))
        { RechargeWeapon(); }

        if (Input.GetMouseButton(0) && this.activeWeapon.GetComponent<WeaponController>().afterShootTime <= 0)
        {
            if (this.activeWeapon.GetComponent<WeaponParameters>().holderCount == 0)
            { if (!RechargeWeapon()) ChangeWeapon(); }

            this.activeWeapon.GetComponent<WeaponController>().OnShoot(this.gameObject);
        }
    }

    public void AddWeapon(GameObject weapon)
    {
        foreach (GameObject w in weaponList)
        {
            if (w.GetComponent<WeaponParameters>().nameWeapon == weapon.GetComponent<WeaponParameters>().nameWeapon)
            {
                this.GetComponent<FPC_Equipment>().ammunitionList.Add(weapon.GetComponent<WeaponParameters>().holder);
                Destroy(weapon);
                this.GetComponent<FPC_AmmunitionController>().CheckAmmunition();
                return;
            }
        }
        weaponList.Add(weapon);
        weapon.transform.parent = mainCamera.transform;

        weapon.transform.localPosition = weapon.GetComponent<WeaponParameters>().FPS_usePosition;
        weapon.transform.localScale = weapon.GetComponent<WeaponParameters>().FPS_useScale;
        weapon.transform.localRotation = Quaternion.Euler(weapon.GetComponent<WeaponParameters>().FPS_useRotationEuler);

        weapon.GetComponent<BoxCollider>().enabled = false;
        weapon.SetActive(false);
    }

    public void ChangeWeapon()
    {
        this.activeWeapon.SetActive(false);

        int tmp = weaponList.IndexOf(this.activeWeapon);
        for (int i = tmp + 1; i != tmp; i++)  //перебираем лист оружия (циклически)
        {
            i = (i < weaponList.Count) ? i : 0;
            if (weaponList[i].GetComponent<WeaponParameters>().isAmmunition == true ||
                weaponList[i].GetComponent<WeaponParameters>().holderCount > 0)
            {
                //то взять в руку
                this.activeWeapon = weaponList[i];
                this.activeWeapon.SetActive(true);

                return;                                             //выйти из мтода Update()
            }
        }
    }

    public bool RechargeWeapon()
    {
        //если рукопашная, не перезаряжаем
        if (activeWeapon.GetComponent<WeaponParameters>().type == "Melee") return true;

        foreach (GameObject amm in this.GetComponent<FPC_Equipment>().ammunitionList)
        {
            //одинаковые ли названия у аммуниции в листе и в активном оружии
            if (amm.GetComponent<AmmunitionParameters>().nameAmmunition == activeWeapon.GetComponent<WeaponParameters>().holder.GetComponent<AmmunitionParameters>().nameAmmunition)
            {
                //перезаряжаем, звук и анимация перезарядки
                activeWeapon.GetComponent<WeaponController>().RechargeWeapon(amm);
                this.GetComponent<FPC_Equipment>().ammunitionList.Remove(amm);

                this.GetComponent<FPC_AmmunitionController>().CheckAmmunition();
                return true;
            }
        }
        return false;
    }

    //устанавливаем прицел
    //void OnGUI()
    //{
    //    if (activeWeapon.GetComponent<WeaponParameters>().type != "Melee")
    //    {
    //        GUI.DrawTexture(activeWeapon.GetComponent<WeaponParameters>().sightRect, activeWeapon.GetComponent<WeaponParameters>().Sight);
    //    }
    //}
}