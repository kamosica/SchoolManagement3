using UnityEngine;
using System.Collections;

public class Coma_Mane : MonoBehaviour
{

    public GameObject MapTile;
    public GameObject MapTile2;
    public GameObject MapTile3;
    bool komasentaku = false;
    public GameObject clickMAP;
    public GameObject clickCUBE;
    ChessMap chessmap;
    Vector2 comapos;
    Coma coma;
    float interval;
	// Use this for initialization
	void Start () {
        chessmap = GetComponent<ChessMap>();
        interval=0.7f;
    }

    // Update is called once per frame
    void Update()
    {

        //メインカメラ上のマウスカーソルのある位置からRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        //{
        //    if (komasentaku == false)
        //    {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                if (hit.collider.gameObject.tag == ("Coma"))
                {
                    clickCUBE = hit.collider.gameObject;
                    coma = clickCUBE.GetComponent<Coma>();
                    comapos = coma.position;
                    //周りのコマチェック
                    if (chessmap.Map_array[(int)comapos.y, (int)comapos.x + 1] == 0 && comapos.x+1 < 6)//→
                        Instantiate(MapTile, new Vector3(((int)comapos.x + 2 )* interval, 1, (int)comapos.y * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));

                    if (chessmap.Map_array[(int)comapos.y, (int)comapos.x - 1] == 0 && comapos.x-1 > 0)//左
                       Instantiate(MapTile, new Vector3(((int)comapos.x - 2) * interval, 1, (int)comapos.y * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));

                    if (chessmap.Map_array[(int)comapos.y + 1, (int)comapos.x] == 0 && comapos.y+1 < 16)//上
                        Instantiate(MapTile, new Vector3((int)comapos.x * interval, 1, ((int)comapos.y + 2) * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));

                    if (chessmap.Map_array[(int)comapos.y - 1, (int)comapos.x] == 0 && comapos.y-1 > 0)//下
                        Instantiate(MapTile, new Vector3((int)comapos.x * interval, 1, ((int)comapos.y - 2) * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));

                    komasentaku = true;
                }
                //}
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
