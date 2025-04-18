using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

public class Script : MonoBehaviour
{

    //public TextMeshProUGUI counterText;
    public float counter;
    public GameObject valu;

    void Start(){
        counter = 1;
    }
    void Update()
    {
        //increases displayed counter when roll key is pressed
        //counterText.text = ("player" + counter.ToString() + " Roll: " + valu.GetComponent<Controller>().Dice.GetComponent<Dicee>().totalVal);

        if (Input.GetKeyDown("w")){
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter(){
        //waits until previous dice destroyed to increase counter
        yield return new WaitForSeconds(4);
        counter = counter + 1;
    }
}
