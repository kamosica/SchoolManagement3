using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class sisann : MonoBehaviour {

    public int S_sisan  = 1000000;
    public int kyuuryou = 200000;
    public int seihuku  = 10000;
    public int sisetu   = 200000;
    public int gyougi = 45000;


	// Use this for initialization
	void Start ()
    {
        
        //初期値60を表示
        //String型に変換して表示
        GetComponent<Text>().text = (S_sisan).ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("hike");
            S_sisan -= kyuuryou;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            S_sisan -= seihuku;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            S_sisan -= sisetu;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            S_sisan=S_sisan - gyougi;
            
        }
        GetComponent<Text>().text = (S_sisan).ToString();
    }
}
