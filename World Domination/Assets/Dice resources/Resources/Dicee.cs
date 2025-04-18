using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dicee : MonoBehaviour
{
    
    public int totalVal;
    public GameObject D1;
    public GameObject D2;
    public bool rolled;

    void Start(){
        
    }
    
    void Update()
    {
        int V1 = D1.GetComponent<Roll>().fVal;
        int V2 = D2.GetComponent<Roll>().fVal;

        if (V1 + V2 > 0){
            totalVal = V1 + V2;
            Debug.Log(totalVal);
            rolled = true;
        }
    }
}
