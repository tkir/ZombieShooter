using UnityEngine;
using System.Collections;

public class FireSystem : MonoBehaviour {

    public GameObject projectorWall;
    public GameObject projectorEnemy;
    public int maxProjectors = 50;
    private GameObject[] projectorsArray;
    private int tmpCount;
    private GameObject projector;

    void Start()
    {
        projectorsArray = new GameObject[maxProjectors];
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(transform.position);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Quaternion projectorRotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);

                switch (hit.transform.gameObject.layer)
                {
                    case 8: // номер слоя с плоскими объектами
                        projector = projectorWall;
                        break;
                    case 9: // номер слоя с моделями персонажей или рельефных объектов
                        projector = projectorEnemy;
                        break;
                }

                GameObject obj = Instantiate(projector, hit.point + hit.normal * 0.25f, projectorRotation) as GameObject;

                Destroy(projectorsArray[tmpCount]);
                projectorsArray[tmpCount] = obj;

                obj.transform.parent = hit.transform;

                Quaternion randomRotZ = Quaternion.Euler(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, Random.Range(0, 360));
                obj.transform.rotation = randomRotZ;

                if (tmpCount == maxProjectors - 1) tmpCount = 0; else tmpCount++;
            }
        }
    }
}