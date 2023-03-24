using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InteractionScript : MonoBehaviour
{
    [Header("Текущий объект(для дебага, трогать не надо)")]
    public GameObject itemInteract;
    [Header("пустышка для руки")]
    [SerializeField] private GameObject hand;
    [Header("Прицел")]
   [SerializeField]private GameObject pricel;
    [Header("Камера игрока")]
    [SerializeField] private GameObject camer;
    private int vzyal = 0;
    RaycastHit hit;
    private Rigidbody rb;
    [Header("Сила взятия")]
    [SerializeField] private float grabPower = 11.01f;
    private int once = 0;
   
    private bool trou;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    private void Update()
    {
        
            if (Input.GetKeyDown(KeyCode.F))
        {

            RaycastHit hit;
            if (Physics.Raycast(camer.transform.position, camer.transform.forward, out hit, 3))
          {

                if(trou == false)
                {
                    if (vzyal == 0)
                    {
                        
                        if (hit.collider.tag == "Crystal" )  
                        {

                            itemInteract = hit.collider.gameObject;
                            vzyal = 1;

                        }
                        else if ( hit.collider.tag == "Button")
                            {
                                
                                if(hit.transform.parent != null)
                            {
                                hit.transform.parent.GetComponent<Animator>().SetBool("Start", true);
                            }

                            }
                    }
                }
                
               

                    


            }
        }
        if (vzyal == 1)
        {
           
            if (Input.GetMouseButtonDown(0))
            {
                ThrowO();
                
            }
            // itemInteract.transform.parent.gameObject.transform.parent = Camera.main.transform; 


        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            if(itemInteract != null)
            {
                vzyal = 0;
                rb.useGravity = true;
                rb.freezeRotation = false;
                //ротация включить обязательнл
                // itemInteract.transform.parent.transform.parent = null;
                itemInteract = null;
            }
            
           

        }
        
    }
   
    private void FixedUpdate()
    {
        
        if(itemInteract != null)
        {
            if (Vector3.Distance(hand.transform.position, itemInteract.transform.position) >2.5f)
            {
                Debug.Log(Vector3.Distance(hand.transform.position, itemInteract.transform.position));
                vzyal = 0;
                itemInteract = null;
            }
        }
        
        if (vzyal == 0)
        {
            if(rb != null)
            {
                pricel.SetActive(true);
                vzyal = 0;
                rb.useGravity = true;

                rb.freezeRotation = false;

                
                itemInteract = null;
            }
            else
            {
                pricel.SetActive(true);
            }
            

        }
        if (vzyal == 1)
        {

            if (itemInteract == null)
            {

                pricel.SetActive(true);

            }
            else
            {
                if (itemInteract.GetComponent<Rigidbody>())
                {
                    pricel.SetActive(false);

                    rb = itemInteract.GetComponent<Rigidbody>();
                }
                else if (itemInteract.GetComponentInParent<Rigidbody>())
                {
                    pricel.SetActive(false);
                    rb = itemInteract.GetComponentInParent<Rigidbody>();
                }
                else
                {
                    pricel.SetActive(true);
                }
            }
            
           
            
            
            if (rb.centerOfMass.x - 0.7f > hand.transform.position.x || rb.centerOfMass.x + 0.7f < hand.transform.position.x || rb.centerOfMass.y - 0.7f > hand.transform.position.y || rb.centerOfMass.y + 0.7f < hand.transform.position.y || rb.centerOfMass.z - 0.7f > hand.transform.position.z || rb.centerOfMass.z + 0.7f < hand.transform.position.z)
            {
                rb.velocity = (hand.transform.position - (itemInteract.transform.transform.position + rb.centerOfMass)) * grabPower;

            }

            if (!itemInteract.name.StartsWith("Rope"))
            {
                rb.freezeRotation = true;
            }
            
            
            
                
            rb.useGravity = false;


            

        }
        
    }
    private void ThrowO()
    {
        
        vzyal = 0;
        rb.useGravity = true;
        rb.freezeRotation = false;
        
        // itemInteract.transform.parent.transform.parent = null;
        trou = true;
        rb.velocity = Camera.main.transform.forward * 10f;
        itemInteract = null;
        StartCoroutine(otp());
    }
    IEnumerator otp()
    {
        yield return new WaitForSeconds(0.5f);
        trou = false;
    }
    public void ItemInt()
    {
        
    }
}
