using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_GUN_Long : MonoBehaviour
{
    [SerializeField] private GameObject GunTarget;
    [SerializeField] private GameObject PlayerG;
    [SerializeField] private GameObject CrystalG;
    [SerializeField] private GameObject Gun;
    [SerializeField] private GameObject Gun1;
    [SerializeField] private float MaxDistanse = 1000f;
    [SerializeField] private float Speed = 50f;
    [SerializeField] private int Priority = 0;
    [SerializeField] private GameObject[] AllGunTargets;
    public NavMeshAgent Nav;

    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform PointBul;

    [SerializeField] private Transform PointControllPlayer;
    [SerializeField] private Transform PointControllCrystal;
    [SerializeField] private Transform PointControllShoot;

    private Vector3 t;
    private Vector3 g;

    bool a = true; 
    bool b = true;

    ///private bool GoOldMaxDistanse;///


    // Start is called before the first frame update
    void Start()
    {
        AllGunTargets = GameObject.FindGameObjectsWithTag("TargetGun");
        Nav.speed = 5f;
        //Nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    { 
        if(GunTarget == null)
        {
            MaxDistanse = 1000f;
        }

        PointControllPlayer.transform.LookAt(PlayerG.transform.position);
        PointControllCrystal.transform.LookAt(CrystalG.transform.position);

        Ray ToCrystal = new Ray(PointControllCrystal.transform.position, PointControllCrystal.transform.forward * 100);
        Debug.DrawRay(PointControllCrystal.transform.position, PointControllCrystal.transform.forward * 100, Color.black);
        Ray ToPlayer = new Ray(PointControllPlayer.transform.position, PointControllPlayer.transform.forward * 100);
        Debug.DrawRay(PointControllPlayer.transform.position, PointControllPlayer.transform.forward * 100, Color.blue);

        RaycastHit CrystalTag;

        RaycastHit PlayerTag;

        if (Physics.Raycast(ToCrystal, out CrystalTag))
        {
            if (CrystalTag.collider.name == "Heart")
            {
                GunTarget = CrystalTag.collider.gameObject;
                MaxDistanse = 1000;
            }
        }

        if (Physics.Raycast(ToPlayer, out PlayerTag))
        {
            print(PlayerTag.collider.name);
            if (CrystalTag.collider.name != "Heart")
            {
                if (PlayerTag.collider.name == "FirstPersonController")
                {
                    GunTarget = PlayerTag.collider.gameObject;
                    MaxDistanse = 1000;
                }
            }
        }
        if(CrystalTag.collider.name != "Heart" && PlayerTag.collider.name != "FirstPersonController")
            SelTarget();

        float _distance = Vector3.Distance(transform.position, GunTarget.transform.position);
        if (_distance == 20)
        {
            a = true;
        }

        if (GunTarget != null && _distance > 20)
        {
            Nav.speed = 5f;
            Nav.destination = GunTarget.transform.position;
            
        }
        else if(_distance <= 20)
        {
            
            if(a == true)
            {
                Gun.transform.LookAt(GunTarget.transform.position);
                Gun1.transform.LookAt(GunTarget.transform.position);
                a = false;
            }
            if (a == false)
            {
                Nav.speed = 0;
                Vector3 _tB = new Vector3(GunTarget.transform.position.x, 0, GunTarget.transform.position.z);
                t = Vector3.Lerp(t, _tB, 2f * Time.deltaTime);
                Gun1.transform.LookAt(t);

                Vector3 _tG = new Vector3(GunTarget.transform.position.x, GunTarget.transform.position.y, GunTarget.transform.position.z);
                g = Vector3.Lerp(g, _tG, 2f * Time.deltaTime);
                Gun.transform.LookAt(g);

                /*Vector3 direction = GunTarget.transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                Gun1.transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Speed * Time.deltaTime);*/

                /*Vector3 _tB = new Vector3(GunTarget.transform.position.x, 0, GunTarget.transform.position.z);
                t = Vector3.Lerp(t, _tB, 2f * Time.deltaTime);
                Gun1.transform.rotation = Quaternion.LookRotation(t);
                Vector3 _tG = new Vector3(0, GunTarget.transform.position.y,0);
                g = Vector3.Lerp(g, _tG, 2f * Time.deltaTime);
                Gun.transform.rotation = Quaternion.LookRotation(g);*/

                //Vector3 direction = GunTarget.transform.position ;
                //Quaternion rotation = Quaternion.LookRotation(direction);
                //Gun1.transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Speed * Time.deltaTime);
                //Gun1.transform.rotation = new Quaternion(transform.rotation.x, Mathf.Lerp(transform.rotation.y, rotation.y, Speed * Time.deltaTime), transform.rotation.z, transform.rotation.w);
            }


            Ray ShootController = new Ray(Gun.transform.position, Gun.transform.forward);
            Debug.DrawRay(Gun.transform.position, Gun.transform.forward * 200f, Color.green);

            if(Physics.Raycast(ShootController, out RaycastHit ObjectTag))
            {
                if (ObjectTag.collider.tag == "TargetGun" && ObjectTag.distance < 20f && b == true || ObjectTag.collider.name == "Heart" && ObjectTag.distance < 20f && b == true || ObjectTag.collider.name == "FirstPersonController" && ObjectTag.distance < 20f && b == true)
                {
                    StartCoroutine(Shoot());
                    b = false;
                }
            }
        }
    }

    public void SelTarget()
    {
        AllGunTargets = GameObject.FindGameObjectsWithTag("TargetGun");

        foreach (GameObject target in AllGunTargets)
            {
                float _distance = Vector3.Distance(transform.position, target.transform.position);
                if (_distance <= MaxDistanse)
                {
                    MaxDistanse = _distance;
                    GunTarget = target;
                }
            }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(Bullet, PointBul.position, PointBul.transform.rotation);
        yield return new WaitForSeconds(2f);
        b = true;
        StopCoroutine(Shoot());
    }
}
