using UnityEngine;
using System.Collections;

public class Coma : MonoBehaviour {

    public int PalyerNo;   //どちらのコマか
    public int ComaNo;  //コマの種類

    public Vector2 position;
    public bool isCrush; //つぶれているかどうか

    Color coma_color;

    float alfa = 1.0f;
    bool alfaflg = true;

    // Use this for initialization
    void Start () {

        coma_color = this.GetComponent<Renderer>().material.color;
    }
	
	// Update is called once per frame
	void Update () {

        //if (alfa == 0.0f) alfaflg = false;
        //else if (alfa == 1.0f) alfaflg = true;

        //if (alfaflg == false) alfa += 0.05f;
        //else if (alfaflg == true) alfa -= 0.05f;

        //coma_color = new Color(coma_color.r, coma_color.g, coma_color.b, alfa);

        //this.GetComponent<Renderer>().material.color = coma_color;

    }
}
