using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class sisan : MonoBehaviour {
    public int S_sisan  = 100000000;
    public int kyuuryou = 250000;
    public int sisetu   = 20000000;
    public int seihuku  = 20000;
    public int gyouzi   = 30000;

    // Use this for initialization
    void Start ()
    {
        //初期値60を表示
        //float型からint型へCastし、String型に変換して表示
        GetComponent<Text>().text = S_sisan.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            S_sisan -= kyuuryou;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            S_sisan -= seihuku;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            S_sisan -= gyouzi;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            S_sisan -= sisetu;
        }
        GetComponent<Text>().text = S_sisan.ToString();
    }
}
