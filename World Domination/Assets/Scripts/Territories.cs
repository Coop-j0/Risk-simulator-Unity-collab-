using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Territories : MonoBehaviour
{
    public enum Continent{
        NorthAmerica,
        SouthAmerica,
        Europe,
        Asia,
        Africa,
        Australia
    }

    public enum Territory{
        Greenland,
        Alaska,
        North_west,
        Ontario,
        Central_America,
        Alberta,
        Western_US,
        Eastern_US,
        Quebec,
        Argentina,
        Brazil,
        Venezuela,
        Peru,
        Scandinavia,
        Ukraine,
        Southern_Europe,
        Western_Europe,
        Northern_Europe,
        Great_Britain,
        Iceland,
        Siam,
        India,
        Middle_East,
        Afghanastan,
        Ural,
        Siberia,
        Yakutsk,
        Kamchatka,
        Mongolia,
        Irkutsk,
        China,
        Japan,
        South_Africa,
        Congo,
        East_Africa,
        Egypt,
        North_Africa,
        Madagascar,
        Western_Australia,
        Eastern_Australia,
        Indonesia,
        New_Guinea
    }


    public Continent continent;
    public Territory territory;

    public List<Territory> connectedTo = new List<Territory>();
    public List<GameObject> GoTerritory = new List<GameObject>();
    public GameObject ownedBy;
    public int NoOfTroops;
    public List<GameObject> Troops = new List<GameObject>();
    public Canvas tex;
    private TMP_Text textor;
    private Canvas tex1;

    // Start is called before the first frame update
    void Start()
    {
        tex1 = Instantiate(tex, transform.position, transform.rotation, transform);
        tex1.GetComponent<RectTransform>().localPosition = gameObject.GetComponent<MeshFilter>().mesh.bounds.center + new Vector3(0,0.03f,0);
        textor = tex1.gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        GoLister();
        //tex1.GetComponent<TMP_Text>().text = "" + NoOfTroops;
        //Debug.Log(gameObject.GetComponent<MeshFilter>().mesh.bounds.center + "" + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        textor.text = "" +NoOfTroops;
        NoOfTroops = Troops.Count;
    }

    void GoLister(){
        foreach(Territory i in connectedTo){
            var temp = i.ToString();
            GoTerritory.Add(GameObject.Find(temp));
        }
    }
}
