using UnityEngine;
using System.Collections;

public class Bullet_hole : MonoBehaviour {


	void Update () {

        Destroy(gameObject, 2);
	}
}
