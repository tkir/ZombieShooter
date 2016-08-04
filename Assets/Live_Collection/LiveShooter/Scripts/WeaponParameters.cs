using UnityEngine;
using System.Collections;

public class WeaponParameters : MonoBehaviour
{
    public string nameWeapon;
    public string type;
    public string ammunition;
    [HideInInspector]
    public bool isAmmunition = false;
    [HideInInspector]
    public int holderCount;
    public GameObject holder;
    public int damage;

    public Vector3 FPS_usePosition;
    public Vector3 FPS_useRotationEuler;
    public Vector3 FPS_useScale;


    public Texture2D Sight;
    public int sightSize;
    [HideInInspector]
    public Rect sightRect;
    private Vector2 screenResolution;

    public GameObject MainCamera;

    public float rechargeDelay;
    public AnimationClip rechargeAnimationClip;
    public int rechargeAnimationSpeed = 2;
    public AudioClip rechargeAudio;

    public float shootDelay;
    public AnimationClip shootAnimationClip;
    public float shootAnimationClipSpeed = 2f;
    public AudioClip shootAudio;


    void Start()
    {
        OnChangeScreenResolution();
        if (holder != null) holderCount = holder.GetComponent<AmmunitionParameters>().ammunitionCapacity;
    }

    void Update()
    {
        if (screenResolution.x != Screen.width || screenResolution.y != Screen.height) OnChangeScreenResolution();
    }

    private void OnChangeScreenResolution()
    {
        screenResolution = new Vector2(Screen.width, Screen.height);
        sightRect = new Rect((screenResolution.x - sightSize) / 2, (screenResolution.y - sightSize) / 2, sightSize, sightSize);
    }
}