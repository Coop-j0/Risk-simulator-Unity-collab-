using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Board : MonoBehaviour
{
    [Header ("Lists")]
    public List<GameObject> Players = new List<GameObject>();
    public List<GameObject> pOrder = new List<GameObject>();
    public List<Material> Colours = new List<Material>();
    public List<GameObject> Territory = new List<GameObject>();

    [Header ("Player")]
    public int PlayerNum;
    public GameObject PlayerPrefab;
    private int Biggest;
    public GameObject temp;
    public GameObject currentP;
    public Transform box;
    
    [Header ("Display")]
    public TMP_Text indicator1;
    public TMP_Text indicator2;
    public GameObject cam;

    void Start()
    {
        //instantiates players, storing in list
        StartCoroutine(waiter());
        
    }

    void Update()
    {
        //Handles text ui displaying which player's turn and phase of turn
        if (currentP != null){
            indicator1.text = currentP.name;
            //sets phase text by finding which phase bool of the current player is true
            if(currentP.GetComponent<Player>().Deploy == true){
                indicator2.text = "Deploy";
            }
            else if(currentP.GetComponent<Player>().Attack == true){
                indicator2.text = "Attack";
            }
            else if(currentP.GetComponent<Player>().Fortify == true){
                indicator2.text = "Fortify";
            }
        
        }
        else{
            indicator1.text = "";
            indicator2.text = "";
        }
    }
    
    //Firstroll method quickly finds the player that rolled the highest number in the first roll
    List<int> FirstRoll(){
        List<int> Rolls = new List<int>();
        //finds the highest roll
        foreach (GameObject i in Players){
            Rolls.Add(i.GetComponent<Player>().diceScore);
            if(i.GetComponent<Player>().diceScore > temp.GetComponent<Player>().diceScore) {
                //sets global variable temp to player with highest roll
                temp = i;
            }
            
        }
        
        return(Rolls);
    }

    //Waiter method instantiates players, waiting for each player to make their first roll before instantiating next
    IEnumerator waiter(){
        for(int i = 0; i < PlayerNum; i++){
            Players.Add(Instantiate(PlayerPrefab, box.position, transform.rotation));
            //sets player attributes
            Players[i].name = "player" + (i+1);
            Players[i].GetComponent<Player>().Colour = Colours[i];
            //calls allocate method to give player starting troops
            allocate(Players[i]);
            Players[i].GetComponent<Player>().box = box;
            
            
            //waits until player has rolled to instantiate next
            yield return new WaitUntil(() => Players[i].GetComponent<Player>().Roll1 == true);
            
        }
        temp = Players[0];
        Debug.Log(FirstRoll().Max());
        pOrder.Add(temp);
        Debug.Log(temp);
        //calls arrange method to arrange player list by descending first rolls
        arrange();
        
        
    }
    //pTurn method waits until player turn is over to end
    IEnumerator pTurn(GameObject i){
         yield return new WaitUntil(() => i.GetComponent<Player>().turn == false);
    }
    
    //yeah method runs through player list, allowing players to play turn
    IEnumerator yeah(){
        //foreach loop used to start every player's turn
         foreach (GameObject i in Players){
                
                i.GetComponent<Player>().turn = true;
                i.GetComponent<Player>().Deploy = true;
                currentP = i;
                //calls pTurn method to wait until player turn is over before moving to next i
                yield return StartCoroutine(pTurn(i));
        }
        //function calls itself after foreach loop is complete to start loop again from first player
        yeah();
    }

    //arrange method uses insertion sort to organise Players list by dice roll
    void arrange(){
        //for loop used instead of foreach loop in order to have i be an incrementing integer
        for (int i = 0; i < PlayerNum; i++) {
            GameObject val = Players[i];
            int flag = 0;
            for (int j = i - 1; j >= 0 && flag != 1; ) {
                //compares value of current item in list to those beside, swapping if it is greater
                if (val.GetComponent<Player>().diceScore > Players[j].GetComponent<Player>().diceScore) {
                    Players[j + 1] = Players[j];
                    j--;
                    Players[j + 1] = val;
            }
            else flag = 1;
        }
        //calls yeah to start player turns
        StartCoroutine(yeah());
        }
    }

    //allocate method calculates the number of troops to give to each player
    void allocate(GameObject Player){
        if (Territory.Count > 6){
        //adds n random territories to player, removing them from own territory list
        for(int i = 0; i < 8; i++){
            var rand = Random.Range(0, Territory.Count);
            Player.GetComponent<Player>().territories.Add(Territory[rand]);
            Territory[rand].GetComponent<Territories>().ownedBy = Player;
            Territory.Remove(Territory[rand]);
        }
        }
        else{
            for(int i = 0; i < 6; i++){
            var rand = Random.Range(0, Territory.Count);
            Player.GetComponent<Player>().territories.Add(Territory[rand]);
            Territory.Remove(Territory[rand]);
        }
        }
    }

    public void nextPhase()
    {
        currentP.GetComponent<Player>().nextPhase();
    }


}

