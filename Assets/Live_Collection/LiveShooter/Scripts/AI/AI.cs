using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{

    Transform enemy;
    public Transform FPC_point;
    public GameObject FPC;
    public Transform enemyPoint;
    public int lookDistance = 80;
    private int atackDistance;
    NavMeshAgent NavMesh;
    Rigidbody rb;


    public float distanceEnemyToFPC;
    public float distanceEnemyToMainPosition;

    public float speed = 3;

    Vector3 distanceEnemyToFPCXZ;
    Vector3 distanceFPCToFEnemyXZ;

    private Animator myAnimator;

    void Start()
    {
        enemy = this.gameObject.transform;
        atackDistance = lookDistance - 30;
        myAnimator = this.GetComponentInChildren<Animator>();
        NavMesh = this.GetComponent<NavMeshAgent>();
        NavMesh.enabled = false;
        rb = this.GetComponent<Rigidbody>();

    }

    void Update()
    {

        distanceEnemyToFPC = Vector3.Distance(FPC_point.transform.position, enemy.transform.position);
        distanceEnemyToMainPosition = Vector3.Distance(enemyPoint.transform.position, enemy.transform.position);

        if (this.GetComponent<ZombieProperties>().isLive == true)
        {
            if (distanceEnemyToFPC <= lookDistance || this.GetComponent<ZombieProperties>().hp < this.GetComponent<ZombieProperties>().maxLife) Atack();

            if (distanceEnemyToFPC > lookDistance && this.GetComponent<ZombieProperties>().hp == this.GetComponent<ZombieProperties>().maxLife) ReturnBack(); 
        }
        else                         Die(); 
    }

    void Atack()
    {
        distanceEnemyToFPCXZ = FPC_point.position - enemy.position;
        distanceEnemyToFPCXZ.y = 0; // заморозить поворот по Y

        distanceFPCToFEnemyXZ = enemy.position - FPC_point.position;
        distanceFPCToFEnemyXZ.y = 0;

        enemy.transform.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(distanceEnemyToFPCXZ), speed * Time.deltaTime);// enemy поворачивается по направлению к игроку со скоростью 3*

        if (distanceEnemyToFPC <= atackDistance || this.GetComponent<ZombieProperties>().hp < this.GetComponent<ZombieProperties>().maxLife)
        {
            if (distanceEnemyToFPC <= 2.5f)
            {
                NavMesh.enabled = false;
                myAnimator.SetBool("Walk", false);
                myAnimator.SetBool("Attack", true);
                this.GetComponent<ZombieController>().Hit(FPC);
            }

            if (distanceEnemyToFPC >= 3f)
            {
                NavMesh.enabled = true;              // enemy приближается к FPC
                NavMesh.SetDestination(FPC_point.position);
                //enemy.position += enemy.forward * speed * Time.deltaTime; // enemy приближается к FPC со скоростью 3*

                myAnimator.SetBool("Walk", true);
                myAnimator.SetBool("Attack", false);
                this.GetComponent<ZombieController>().Hit(null);
            }
        }

        if (distanceEnemyToFPC > atackDistance && this.GetComponent<ZombieProperties>().hp == this.GetComponent<ZombieProperties>().maxLife)
        {
            myAnimator.SetBool("Walk", false);
            NavMesh.enabled = false;
        }
    }

    void ReturnBack()
    {

        if (distanceEnemyToMainPosition > 0.5f)
        {
            //enemy.transform.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(enemyPoint.position - enemy.position), speed * Time.deltaTime); // enemy поворачивается по направлению к enemyPoint со скоростью 3*
            //enemy.position += enemy.forward * speed * Time.deltaTime;
            NavMesh.enabled = true;
            NavMesh.SetDestination(enemyPoint.position);
            myAnimator.SetBool("Walk", true);
        }
        if (distanceEnemyToMainPosition <= 0.5f)
        {
            myAnimator.SetBool("Walk", false);
            NavMesh.enabled = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("bullet"))
        {
            myAnimator.SetTrigger("Get_hit");
        }
    }

    void Die()
    {
        myAnimator.SetTrigger("Die");
        rb.isKinematic = true;
        NavMesh.enabled = false;
        this.GetComponent<CapsuleCollider>().isTrigger = true;
        transform.Translate(-Vector3.up * 0.5f * Time.deltaTime);   //тонет
        Destroy(gameObject, 5);
    }
}

                
                