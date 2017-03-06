using UnityEngine;
using System.Collections;

public class Coma_Mane : MonoBehaviour
{
    bool komasentaku = false;
    public GameObject clickMAP;
    public GameObject clickCUBE;
    ChessMap chessmap;
	// Use this for initialization
	void Start () {
        chessmap = GetComponent<ChessMap>();
    }

    // Update is called once per frame
    void Update()
    {

        //メインカメラ上のマウスカーソルのある位置からRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (komasentaku == false)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.gameObject.tag == ("Coma"))
                    {
                        clickCUBE = hit.collider.gameObject;
                        komasentaku = true;
                    }
                }
            }

            if (komasentaku == true)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.gameObject.tag == ("Maptile"))
                    {
                        clickMAP = hit.collider.gameObject;
                        komasentaku = false;
                    }
                }
            }
            Debug.Log(clickCUBE);
            Debug.Log(clickMAP);
        }
    }
}
