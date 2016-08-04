using UnityEngine;
using System.Collections;

public class FPC_Controller : MonoBehaviour
{

    /// <summary>
    /// Наносим повреждение игроку
    /// </summary>
    /// <param name="injury">на сколько повреждение</param>
    public void Injury(int injury)
    {
        //пока считаем самым простым способом, потом можно будет учесть защиту, добавить спец. повреждения
        this.GetComponent<FPC_Properties>().hp -= injury;
    }
}
