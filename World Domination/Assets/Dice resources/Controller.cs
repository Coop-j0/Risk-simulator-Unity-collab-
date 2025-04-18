using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    
    public GameObject prefab;
    public int Counter;
    private int DCount = 0;
    public GameObject Dice;
    public int PlayerNum;
    public int maxPlayers;

    void Start(){
        NewD();
        
    }

    void FixedUpdate()
    {
        
        if (Input.GetKey("i")){
            if (PlayerNum > 0){
                NewD();
            }
        }

    }
    //waits to delete dice
    IEnumerator waiter(){
        yield return new WaitForSeconds(4);
        DCount = DCount - 1;
        NewD();
    }


    void Update(){
        //Deletes dice after thrown
        if (Input.GetKeyDown("w")){
            if(DCount == 1){
                Destroy(Dice, 4);
                StartCoroutine(waiter());
                
            }
        }


    }

    void NewD(){
        //creates instances of dice game object
        //Debug.Log(DCount);
        if(DCount == 0){
                Dice = Instantiate(prefab, new Vector3(-1,1,-4), Quaternion.Euler(0, Random.Range(-90.0f, 90.0f), 0));
                DCount = DCount + 1;
            }
    }
    

    void pTurn(){
        PlayerNum = PlayerNum + 1;
        if (PlayerNum > maxPlayers){
            PlayerNum = 1;
        }
    }
}
