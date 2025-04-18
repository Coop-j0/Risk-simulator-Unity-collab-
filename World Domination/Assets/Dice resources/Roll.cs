using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody m_Rigidbody;
    public bool roll;

    public LayerMask whatIsGround;

    public GameObject side1;
    public GameObject side2;
    public GameObject side3;
    public GameObject side4;
    public GameObject side5;
    public GameObject side6;

    private bool grounded;
    private GameObject val; 
    public int fVal;


    void Start()
    {
        
        m_Rigidbody = GetComponent<Rigidbody>();
        //turns off gravity
        m_Rigidbody.useGravity = false;
        roll = true;

        

    }

    void Update(){
        
    }

    
    void FixedUpdate()
    {   
        
        if (Input.GetKey("w"))
        {
            if (roll == true){
                //enables gravity on dice
                m_Rigidbody.useGravity = true;
                //applies random range of random forces to "throw" dice
                m_Rigidbody.AddForce(transform.forward * Random.Range(1.0f, 2.5f), ForceMode.Impulse);
                m_Rigidbody.AddForce(transform.up * Random.Range(-0.5f, -1.0f), ForceMode.Impulse);
                m_Rigidbody.AddTorque(transform.up * Random.Range(-3.0f, 3.0f));
                m_Rigidbody.AddTorque(transform.forward * Random.Range(-3.0f, 3.0f));
                m_Rigidbody.AddTorque(transform.right *10);
                roll = false;
                StartCoroutine(waiter());

                //Debug.Log(fVal);
            }
        
        }
    }

    GameObject nRolled(GameObject side, Collision coll){
        //Debug.Log(side);
        //grounded = Physics.Raycast(tran.position, Vector3.down, Mathf.Infinity, whatIsGround);
        if (side){
            return side;
        }
        else{
            return null;
        }
    }

    IEnumerator waiter(){
        yield return new WaitForSeconds(3);
        //while (grounded == false){
            //val = nRolled(side1);
            //val = nRolled(side2);
            //val = nRolled(side3);
            //val = nRolled(side4);
            //val = nRolled(side5);
            //val = nRolled(side6);
        //}
        
        if(side1.GetComponent<DieCol>().grounded == true){
            //Debug.Log(side1);
            fVal = 1;
        }
        else if(side2.GetComponent<DieCol>().grounded == true){
            //Debug.Log(side2);
            fVal = 2;
        }
        else if(side3.GetComponent<DieCol>().grounded == true){
            //Debug.Log(side3);
            fVal = 3;
        }
        else if(side4.GetComponent<DieCol>().grounded == true){
            //Debug.Log(side4);
            fVal = 4;
        }
        else if(side5.GetComponent<DieCol>().grounded == true){
            //Debug.Log(side5);
            fVal = 5;
        }
        else if(side6.GetComponent<DieCol>().grounded == true){
            //Debug.Log(side6);
            fVal = 6;
        }
        //Debug.Log(fVal);
    }
    
}
