using UnityEngine;
using System.Collections;

public class Machin_gun_Shoot : MonoBehaviour
{
    public GameObject FPC;
    public GameObject Main_camera;

    public GameObject Machin_gun_1;
    public GameObject Machin_gun_2;
    public bool g1 = false;
    public bool g2 = false;

    public AnimationClip shoot_1;
    public AnimationClip Anim_recharge;
    private int Anim_shoot_speed = 2;
    private float Time_1;
    private bool a1 = false;
    private float Recharge_shoot_time = 0.1f;

    
    private float Recharge_time = 0;
    private int bullets = 30;
    private int a2 = 0;
    private bool a3 = false;

    public Texture2D Sight;
    
    public Transform Bullet_pos;
    public GameObject Bullet;

    public AudioClip shoot;
    public AudioClip recharge;


    public AnimationClip Anim_sniper_off;
    public bool Sniper_trigger = false;
    public Texture2D text_sniper;
    public GameObject optica;

    public GameObject particles;




    void Start()
    {

        GetComponent<Animation>()[shoot_1.name].speed = Anim_shoot_speed;
        Time_1 = Recharge_shoot_time;

        particles.SetActive(false);
        
    }


    void Update()
          {
              

              if (g1 == false && g2 == false)
              {
                  Machin_gun_1.SetActive(false);
                  Machin_gun_2.SetActive(false);
              }

        if (g1 == true)
        {
            Machin_gun_1.SetActive(true);
            Machin_gun_2.SetActive(false);
            g1 = true;
            g2 = false;
        }
        if (g2 == true)
        {
            Machin_gun_2.SetActive(true);
            Machin_gun_1.SetActive(false);
            g2 = true;
            g1 = false;
        }


        if (Machin_gun_1 == true || Machin_gun_2 == true)
        {
            if (Input.GetMouseButton(0) && Time_1 >= Recharge_shoot_time && a3 == false)
            {
                if (a2 < bullets)
                {
                    GetComponent<Animation>().Play(shoot_1.name);

                Instantiate(Bullet, Bullet_pos.transform.position, Bullet_pos.rotation);

                GetComponent<AudioSource>().PlayOneShot(shoot);
                
                Time_1 = 0;
                a1 = true;
                a2 = a2 + 1;

                particles.SetActive(true);

                }
                else 
                {
                    a1 = true;
                    a2 = a2 + 1;
                }
            }

            if (Input.GetMouseButtonUp(0) || a3 == true)
            {
                particles.SetActive(false);
            }
            
            if (a1 == true)
            {
                Time_1 += Time.deltaTime;
                if (Time_1 >= Recharge_shoot_time)
                {
                    a1 = false;
                }
            }

            if (a2 > bullets)
            {
                GetComponent<Animation>().Play(Anim_recharge.name);
                a2 = 0;
                a3 = true;
                GetComponent<AudioSource>().PlayOneShot(recharge);

               
               Sniper_trigger = false;
            }

            if (a3 == true)
            {
                Recharge_time += Time.deltaTime;
            }

            if (Recharge_time >= 1)
            {
                Recharge_time = 0;
                a3 = false;

            }

        }

        if (Input.GetMouseButtonDown(1) && g2 == true && a3 == false)
        {
            Sniper_trigger = !Sniper_trigger;

            if (Sniper_trigger == true)
            {
                Main_camera.GetComponent<Camera>().fieldOfView = 18;
                FPC.GetComponent<MouseLook>().sensitivityX = 2;
                Main_camera.GetComponent<MouseLook>().sensitivityY = 2;
                optica.SetActive(false);


            }
            if (Sniper_trigger == false)
            {
                GetComponent<Animation>().Play(Anim_sniper_off.name);
                
            }
        }

        if (bullets ==0)
        {
            Sniper_trigger = false;
        }

        if (Sniper_trigger == false)
        {
            Main_camera.GetComponent<Camera>().fieldOfView = 60;
            FPC.GetComponent<MouseLook>().sensitivityX = 15;
            Main_camera.GetComponent<MouseLook>().sensitivityY = 10;
            optica.SetActive(true);
        }
            
    }



    void OnGUI()
    {

        if (Sniper_trigger == false)
        {
            GUI.DrawTexture(new Rect((Screen.width - 50) / 2, (Screen.height - 50) / 2, 50, 50), Sight);
        }

        if (Sniper_trigger == true)
        {
            GUI.DrawTexture(new Rect((Screen.width - 1920) / 2, (Screen.height - 1080) / 2, 1920, 1080), text_sniper);
        }
    }
}

