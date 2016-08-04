using UnityEngine;
using System.Collections;

public class GUI_Skils : MonoBehaviour
{
    private Vector2 screenResolution;               //параметры экрана
    public GameObject FPC;                          //плеер

    public Vector2 hpPosition;                      //
    public Vector2 hpSize;
    public Texture hpBackground;
    public Texture hpForegroundLeft;
    public Texture hpForegroundCenter;
    public Texture hpForegroundRight;

    public Vector2 ammunPosition;
    public Vector2 ammunSize;

    public GUISkin guiSkin;

    private int maxHP;
    private int ammunCount;

    private GameObject activeWeapon;

    void Start()
    {
        maxHP = FPC.GetComponent<FPC_Properties>().maxHP;
    }

    /// <summary>
    /// Проверка изменений разрешения экрана
    /// </summary>
    /// <param name="isNeedUpdate">Обновить без изменения разрешения экрана</param>
    private void OnChangeScreenResolution(bool isNeedUpdate = false)
    {
        if (screenResolution.x != Screen.width || screenResolution.y != Screen.height || isNeedUpdate)
        {
            screenResolution = new Vector2(Screen.width, Screen.height);
            activeWeapon.GetComponent<WeaponParameters>().sightRect =
                new Rect((screenResolution.x - activeWeapon.GetComponent<WeaponParameters>().sightSize) / 2,
                         (screenResolution.y - activeWeapon.GetComponent<WeaponParameters>().sightSize) / 2,
                         activeWeapon.GetComponent<WeaponParameters>().sightSize,
                         activeWeapon.GetComponent<WeaponParameters>().sightSize);
        }
    }

    void OnGUI()
    {
        //получаем текущее оружие
        if (activeWeapon != FPC.GetComponent<FPC_WeaponController>().activeWeapon)
        {
            activeWeapon = FPC.GetComponent<FPC_WeaponController>().activeWeapon;
            OnChangeScreenResolution(true);
        }

        //Прорисовываем элементы GUI
        if (activeWeapon != null)
        {
            OnChangeScreenResolution();

            DrowHP();
            DrawAmmunitionCount();
            DrawSight();
        }
    }

    private void DrowHP()
    {
        int hp = FPC.GetComponent<FPC_Properties>().hp;
        GUI.skin = guiSkin;
        guiSkin.box.alignment = TextAnchor.MiddleLeft;

        GUI.Box(new Rect(hpPosition.x, hpPosition.y, hpSize.x, hpSize.y), hpBackground);
        GUI.Box(new Rect(hpPosition.x, hpPosition.y, hpSize.x, hpSize.y), hpForegroundLeft);

        float deviation = hpForegroundLeft.width / 4;
        for (int i = 0; i < hp * 2; ++i)
        {
            GUI.Box(new Rect(hpPosition.x + deviation, hpPosition.y, hpSize.x, hpSize.y), hpForegroundCenter);
            deviation += hpForegroundCenter.width / 4;
        }

        GUI.Box(new Rect(hpPosition.x + deviation, hpPosition.y, hpSize.x, hpSize.y), hpForegroundRight);

        guiSkin.box.alignment = TextAnchor.MiddleCenter;
        GUI.Box(new Rect(hpPosition.x, hpPosition.y, hpSize.x, hpSize.y), hp + "/" + maxHP);
    }

    private void DrawSight()
    {
        if (activeWeapon.GetComponent<WeaponParameters>().type != "Melee")
        {
            GUI.DrawTexture(activeWeapon.GetComponent<WeaponParameters>().sightRect, activeWeapon.GetComponent<WeaponParameters>().Sight);
        }
    }

    private void DrawAmmunitionCount()
    {
        if (activeWeapon.GetComponent<WeaponParameters>().type != "Melee")
        {
            GUI.TextArea(new Rect(ammunPosition.x, ammunPosition.y, ammunSize.x, ammunSize.y),
                         activeWeapon.GetComponent<WeaponParameters>().holderCount.ToString());
        }
    }
}
