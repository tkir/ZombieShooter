using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{

    public GameObject Bullet_Hole;
    private RaycastHit RayH;
        


	void Start () {

	
	}


    public void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }

	void Update () {

        transform.Translate(0,0,1f);


        if (Physics.Raycast(transform.position, transform.forward, out RayH, 100.00f))
        {
            if (RayH.transform.tag == "Enemy")
            {
                Instantiate(Bullet_Hole, RayH.point, Quaternion.identity);
            }

        }


        
        Destroy(gameObject, 1f);
	}
}
