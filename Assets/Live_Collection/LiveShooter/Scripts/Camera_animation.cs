using UnityEngine;
using System.Collections;

public class Camera_animation : MonoBehaviour {

    public AnimationClip walk;



	void Update () {
	
        if (Input.GetButton ("Vertical") || Input.GetButton ("Horizontal"))
        {
            GetComponent<Animation>().Play(walk.name);
        }


	}
}
