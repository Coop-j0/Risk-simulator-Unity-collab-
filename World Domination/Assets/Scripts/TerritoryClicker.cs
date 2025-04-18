using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TerritoryClicker : MonoBehaviour
{
    
    Ray ray;
	RaycastHit hit;
    public Material mat;
    private Material mat2;
    private GameObject current;
    public GameObject selected;

	void Start(){
    }
	void Update()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit))
		{  
			//if(Input.GetMouseButtonDown(0)){
				//Debug.Log(hit.collider.name);
                
                
                if(current == null){
                current = hit.collider.gameObject;
                mat2 = hit.collider.gameObject.GetComponent<Renderer>().material;
                hit.collider.gameObject.GetComponent<Renderer>().material = mat;
                }
                else if(current != hit.collider){
                    //Debug.Log(current);
                    current.GetComponent<Renderer>().material = mat2;
                    current = hit.collider.gameObject;
                    mat2 = hit.collider.gameObject.GetComponent<Renderer>().material;
                    //Debug.Log(current);
                    current.GetComponent<Renderer>().material = mat;
                }
            //}
            if(Input.GetMouseButtonDown(0)){
                selected = hit.collider.gameObject;
            }
		}
	}

    
}