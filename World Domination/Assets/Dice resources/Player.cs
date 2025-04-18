using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   

    public bool turn;
    public bool endTurn;
    public bool Deploy;
    public bool Attack;
    public bool Fortify;
    public bool AttackOn;
    private int Timer;
    public int diceScore;
    public int DeployCount;
    public int aDice;
    public int dDice;
    public string currentStage;
    public List<GameObject> cards = new List<GameObject>();
    public List<GameObject> territories = new List<GameObject>();
    public GameObject DicePrefab;
    public GameObject PiecePrefab;
    public GameObject Dice;
    public GameObject TroopPrefab;
    public GameObject selected;
    public GameObject attackFrom;
    public GameObject attackTo;
    public bool Roll1 = false;
    public List<GameObject> Army = new List<GameObject>();
    public Material Colour;
    public Transform box;
    public GameObject cam;
    public GameObject Defender;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");

        roll();
        
        Deploy = false;
        Attack = false;
        Fortify = false;

        DeployCount = 0;
        for(int i = 0; i < 20; i++){
               Army.Add(Instantiate(TroopPrefab, transform.position, transform.rotation, transform));
               Army[i].name = "troop " + (i+1);
               Army[i].GetComponent<Troop>().Owner = gameObject;
            }
        foreach (GameObject i in territories){
            i.GetComponent<Renderer>().material = Colour;
        }
        int trop = 20;
        while(trop >= 0){
            foreach (GameObject i in territories){
                Army[trop].GetComponent<Troop>().occupyingTerritory = i;
                Army[trop].transform.parent = i.transform;
                Army[trop].GetComponent<Territories>().Troops.Add(Army[trop]);
                trop = trop - 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //turn = !endTurn;
        if(turn){
            if(Deploy){
                StartCoroutine(Deploying());
            }
            if(Attack){
                StartCoroutine(Attacking());
            }
            if(Fortify){
                StartCoroutine(Fortifying());
            }
        }
    }

    public void roll(){
        Dice = Instantiate(DicePrefab, box.position + new Vector3(-0.3f,-0.6f,0.2f), box.rotation);
        StartCoroutine(waiter());
    }

    public void nextPhase()
    {
        if(Deploy)
        {
            Debug.Log(currentStage);
            Deploy = false;
            Attack = true;
        }

        else if(Attack)
        {
            Debug.Log(currentStage);
            Attack = false;
            Fortify = true;
        }

        else if(Fortify)
        {
            Fortify = false;
            turn = false;
        }
            
    }

    IEnumerator waiter(){
        yield return new WaitUntil(() => Dice.GetComponent<Dicee>().rolled == true);
        Roll1 = true;
        diceScore = Dice.GetComponent<Dicee>().totalVal;
        Destroy(Dice);
    }


    IEnumerator Countdown(){
        yield return new WaitForSeconds(1);
        Timer = Timer -1;
    }

    IEnumerator Deploying(){
        currentStage = "Deploying";
        if(Input.GetMouseButtonDown(0)){
                yield return new WaitForSeconds(0.1f);
                selected = cam.GetComponent<TerritoryClicker>().selected;
                //Debug.Log(selected);
                if(ownCheck(selected) == true){
                    //Debug.Log("owned");
                    if(find() != null){
                        var found = find();
                        if(found.GetComponent<Troop>().occupyingTerritory != null){
                            found.GetComponent<Troop>().occupyingTerritory.GetComponent<Territories>().Troops.Remove(found);
                        }
                        found.GetComponent<Troop>().occupyingTerritory = selected;
                        found.transform.parent = selected.transform;
                        selected.GetComponent<Territories>().Troops.Add(found);
                    }
                }
                else{
                    Debug.Log("Not yours");
                }
            }
        if(Input.GetKeyDown("y")){
            yield return new WaitForSeconds(0.1f);
            Debug.Log(currentStage);
            Deploy = false;
            Attack = true;
        }
        
        DeployCount = DeployCount+1;
        if(DeployCount < 2){
            for(int i = 0; i < newTroops(); i++){
               Army.Add(Instantiate(TroopPrefab, transform.position, transform.rotation, transform));
               Army[i].name = "troop " + (i+1);
               Army[i].GetComponent<Troop>().Owner = gameObject;
            }
            
        }
        
    }

    IEnumerator Attacking(){
        currentStage = "Attacking";

        if(Input.GetMouseButtonDown(0)){
            var temp = cam.GetComponent<TerritoryClicker>().selected;
            if(AttackOn == false){
            if(ownCheck(temp) && temp.GetComponent<Territories>().NoOfTroops > 1){
                attackFrom = temp;
                aDice = 1;
                Debug.Log("Valid Territory to attack from " + temp.name);
            }
            else if(ownCheck(temp) == false && attackFrom != null){
                if(attackFrom.GetComponent<Territories>().GoTerritory.Contains(temp) == true){
                    attackTo = temp;
                    Defender = attackTo.GetComponent<Territories>().ownedBy;
                    dDice = 1;
                    AttackOn = true;
                    Debug.Log("Valid Territory to attack " + temp.name);
                    Debug.Log("Please select how many dice you would like to roll");
                }
                else{
                    if(attackFrom.GetComponent<Territories>().GoTerritory.Contains(temp) == false){
                        Debug.Log("Invalid area to attack " + temp + "not in connected list " + attackFrom);
                        Debug.Log(attackFrom.GetComponent<Territories>().GoTerritory.Contains(temp));
                    }
                    else{
                        Debug.Log("Invalid area to attack" + temp);
                    }
                }
            }
            }
            
        }

        if(AttackOn == true){
            if(AttackerDice()){
                    
                if(DefenderDice()){

                }
            }

        }
        
        




        if(Input.GetKeyDown("y")){
            yield return new WaitForSeconds(0.1f);
            Attack = false;
            Fortify = true;
        }
    }

    IEnumerator Fortifying(){
        currentStage = "Fortifying";
        if(Input.GetKeyDown("y")){
            yield return new WaitForSeconds(0.1f);
            Fortify = false;
            turn = false;
        }
    }

    int newTroops(){
        var terNum = territories.Count;
        int troopNum = terNum % 3;
        if (troopNum > 3){
            return troopNum;
        }
        else{
            return 3;
        }
    }

    bool ownCheck(GameObject select){
        foreach(GameObject i in territories){
            if(i == select){
                return true;
            }
        }
        return false;
    }
    GameObject find(){
        foreach(GameObject i in Army){
            if(i.GetComponent<Troop>().occupyingTerritory != selected){
                //Debug.Log(i);
                //Debug.Log(selected);
                return i;
            }
        }
        return null;
    }

    bool AttackerDice(){
        if (Input.GetKeyDown("i")){
            Debug.Log(aDice);
        }
        if(aDice < attackFrom.GetComponent<Territories>().NoOfTroops){
            if (Input.GetKeyDown(KeyCode.UpArrow)){
                Debug.Log("Attacker's Dice: " + aDice);
                aDice = aDice + 1;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)){
                Debug.Log("Attacker's Dice: " + aDice);
                aDice = aDice - 1;
            }
        }
        if(Input.GetKeyDown(KeyCode.Return)){
            return true;
        }
        else{
            return false;
        }


        
    }
    
    bool DefenderDice(){
        if (dDice < 3 && dDice <= attackTo.GetComponent<Territories>().NoOfTroops){
            if (Input.GetKeyDown(KeyCode.UpArrow)){
                dDice = dDice + 1;
                Debug.Log("Defender's Dice: " + dDice);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)){
                dDice = dDice - 1;
                Debug.Log("Defender's Dice: " + dDice);
            }
        }

        if(Input.GetKeyDown(KeyCode.Return)){
            return true;
        }
        else{
            return false;
        }
        
    }

    bool pathfinder(GameObject start, GameObject end){
        var startScript = start.GetComponent<Territories>();
        List <GameObject> compiledList = new List<GameObject>();
        foreach(GameObject i in startScript.GoTerritory){
            if(i.GetComponent<Territories>().ownedBy == startScript.ownedBy){
                compiledList = searcher(i);
            }
        }
        return true;
    }

    List<GameObject> searcher(GameObject node){
        var nodeScript = node.GetComponent<Territories>();
        List <GameObject> compiledList = new List<GameObject>();
        foreach(GameObject i in nodeScript.GoTerritory){
            if(i.GetComponent<Territories>().ownedBy == nodeScript.ownedBy){
                //compiledList.Add(i);
                //compiledList.Add(searcher(i));
            }
        }
        return compiledList;
    }


}
