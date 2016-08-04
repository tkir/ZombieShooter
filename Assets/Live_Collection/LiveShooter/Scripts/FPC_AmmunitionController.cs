using UnityEngine;
using System.Collections.Generic;

public class FPC_AmmunitionController : MonoBehaviour {
    private List<GameObject> ammunitionList;
    public GameObject mainCamera;

    void Start () {
        ammunitionList = this.GetComponent<FPC_Equipment>().ammunitionList;
        CheckAmmunition();
    }

    public void AddAmmunition(GameObject ammunition)
    {
        ammunitionList.Add(ammunition);

        ammunition.transform.parent = mainCamera.transform;
        ammunition.GetComponent<BoxCollider>().enabled = false;
        ammunition.SetActive(false);

        CheckAmmunition();
    }
    /// <summary>
    /// Проверка, на какое оружие есть аммуниция
    /// </summary>
    public void CheckAmmunition()
    {
        foreach (GameObject weapon in this.GetComponent<FPC_Equipment>().weaponList)
        {
            foreach (GameObject ammunition in this.GetComponent<FPC_Equipment>().ammunitionList)
            {
                if (weapon.GetComponent<WeaponParameters>().ammunition == ammunition.GetComponent<AmmunitionParameters>().nameAmmunition)
                {
                    weapon.GetComponent<WeaponParameters>().isAmmunition = true;
                    break;
                }
                weapon.GetComponent<WeaponParameters>().isAmmunition = false;
            }
        }
    }

}
