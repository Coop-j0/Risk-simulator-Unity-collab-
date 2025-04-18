using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCol : MonoBehaviour
{

    public bool grounded;
    public LayerMask whatIsGround;

    void Start(){
        //Debug.Log(gameObject.name);
    }

    void Update(){
        grounded = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), Mathf.Infinity, whatIsGround);

        if(grounded == true){
            //Debug.Log("hoopa" + gameObject.name);
        }
    }
    
    
    
}
