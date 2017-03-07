using UnityEngine;
using System.Collections;

public class Coma_Mane1: MonoBehaviour
{

    public GameObject MapTile;
    public GameObject MapTile2;
    public GameObject MapTile3;
    bool komasentaku = false;
    public GameObject clickMAP;
    public GameObject clickCUBE;
    GameObject PanelUE, PanelSITA, PanelMIGI, PanelHIDARI;
    ChessMap chessmap;
    Vector2 comapos;
    Coma coma;
    float interval;
    // Use this for initialization
    void Start()
    {
        chessmap = GetComponent<ChessMap>();
        interval = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {

        //メインカメラ上のマウスカーソルのある位置からRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (komasentaku == false)//コマを選択してるか
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))//ビーム
                {
                    if (hit.collider.gameObject.tag == ("Coma"))//タグがコマで
                    {
                        clickCUBE = hit.collider.gameObject;//クリックしたオブジェクト
                        coma = clickCUBE.GetComponent<Coma>();
                        comapos = coma.position;
                        //周りのコマチェック。行ける所にオブジェクトが出る
                        if (chessmap.Map_array[(int)comapos.y, (int)comapos.x + 1] == 0 && comapos.x + 1 < 6)//→
                            PanelMIGI = (GameObject)Instantiate(MapTile, new Vector3(((int)comapos.x + 2) * interval, 1, (int)comapos.y * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));

                        if (chessmap.Map_array[(int)comapos.y, (int)comapos.x - 1] == 0 && comapos.x - 1 > 0)//左
                            PanelHIDARI = (GameObject)Instantiate(MapTile, new Vector3(((int)comapos.x - 2) * interval, 1, (int)comapos.y * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));

                        if (chessmap.Map_array[(int)comapos.y + 1, (int)comapos.x] == 0 && comapos.y + 1 < 16)//上
                            PanelUE = (GameObject)Instantiate(MapTile, new Vector3((int)comapos.x * interval, 1, ((int)comapos.y + 2) * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));

                        if (chessmap.Map_array[(int)comapos.y - 1, (int)comapos.x] == 0 && comapos.y - 1 > 0)//下
                            PanelSITA = (GameObject)Instantiate(MapTile, new Vector3((int)comapos.x * interval, 1, ((int)comapos.y - 2) * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));

                        komasentaku = true;//選択した
                    }
                }
            }


            else if (komasentaku == true)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.gameObject.tag == ("SentakuMap"))
                    {
                        clickMAP = hit.collider.gameObject;
                        clickCUBE.transform.position = new Vector3(clickMAP.transform.position.x, 0.5f, clickMAP.transform.position.z);//コマの移動（表面）

                        coma = clickCUBE.GetComponent<Coma>();
                        comapos = coma.position;

                        int coma_number = coma.PalyerNo * 10 + coma.ComaNo;

                        if (clickMAP == PanelMIGI)
                        {
                            Hit_Coma(new Vector2(comapos.x + 2, comapos.y), new Vector2(1, 0));

                            chessmap.Map_array[(int)comapos.y, (int)comapos.x + 2] = coma_number;
                            coma.position = new Vector2(comapos.x + 2, comapos.y);
                        }
                        if (clickMAP == PanelHIDARI)
                        {
                            Hit_Coma(new Vector2(comapos.x - 2, comapos.y), new Vector2(-1, 0));

                            chessmap.Map_array[(int)comapos.y, (int)comapos.x - 2] = coma_number;
                            coma.position = new Vector2(comapos.x - 2, comapos.y);
                        }
                        if (clickMAP == PanelUE)
                        {
                            Hit_Coma(new Vector2(comapos.x, comapos.y + 2), new Vector2(0, 1));

                            chessmap.Map_array[(int)comapos.y + 2, (int)comapos.x] = coma_number;
                            coma.position = new Vector2(comapos.x, comapos.y + 2);
                        }
                        if (clickMAP == PanelSITA)
                        {
                            Hit_Coma(new Vector2(comapos.x, comapos.y - 2), new Vector2(0, -1));

                            chessmap.Map_array[(int)comapos.y - 2, (int)comapos.x] = coma_number;
                            coma.position = new Vector2(comapos.x, comapos.y - 2);
                        }

                        chessmap.Map_array[(int)comapos.y, (int)comapos.x] = 0;

                        Destroy(PanelMIGI);//周りのパネルの削除
                        Destroy(PanelHIDARI);
                        Destroy(PanelSITA);
                        Destroy(PanelUE);

                        komasentaku = false;
                    }

                    chessmap.Array_Log();
                }
            }
        }
    }

    void Hit_Coma(Vector2 pos,Vector2 vec)
    {
        if(isRange(pos.x,pos.y) == true)
        {
            if(chessmap.Map_array[(int)pos.y, (int)pos.x] != 0) //進む方向に駒があるかを調べる
            {
                //壁があるかどうか
                if (chessmap.Map_array[(int)pos.y + (int)vec.y, (int)pos.x + (int)vec.x] == 0)
                {
                    //移動先が範囲外かどうか
                    if(isRange(pos.x + vec.x * 2, pos.y + vec.y * 2) == true)
                    {
                        //後ろに駒があるかどうか
                        if(chessmap.Map_array[(int)pos.y + (int)vec.y * 2, (int)pos.x + (int)vec.x * 2] == 0)
                        {
                            GameObject obj = get_Coma(pos);
                            obj.transform.position = new Vector3(obj.transform.position.x + vec.x * 2 * interval, 0.5f, obj.transform.position.z + vec.y * 2 * interval);//コマの移動（表面）
                            Coma c_scr = obj.GetComponent<Coma>();
                            int coma_number = c_scr.PalyerNo * 10 + c_scr.ComaNo;
                            chessmap.Map_array[(int)pos.y + (int)vec.y * 2, (int)pos.x + (int)vec.x * 2] = coma_number;
                            c_scr.position = new Vector2(pos.x + vec.x * 2, pos.y + vec.y * 2);
                        }
                        else
                        {
                            CrushComa(pos, vec);
                        }
                    }
                    else
                    {
                        CrushComa(pos, vec);
                    }
                }
                else
                {
                    CrushComa(pos, vec);
                }
            }
        }
    }

    void CrushComa(Vector2 pos,Vector2 vec)
    {
        GameObject obj = get_Coma(pos);
        obj.transform.position = new Vector3(obj.transform.position.x + vec.x * 1 * interval, 0.5f, obj.transform.position.z + vec.y * 1 * interval);//コマの移動（表面）
        obj.transform.localScale = new Vector3(0.15f, 0.8f, 0.8f);

        Coma c_scr = obj.GetComponent<Coma>();
        int coma_number = c_scr.PalyerNo * 10 + c_scr.ComaNo;
        chessmap.Map_array[(int)pos.y + (int)vec.y * 1, (int)pos.x + (int)vec.x * 1] = coma_number;
        c_scr.position = new Vector2(pos.x + vec.x * 1, pos.y + vec.y * 1);

        if ((int)c_scr.position.x % 2 == 0 && (int)c_scr.position.y % 2 == 1)
        {
            Debug.Log("回転０");
            obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if ((int)c_scr.position.x % 2 == 1 && (int)c_scr.position.y % 2 == 0)
        {
            Debug.Log("回転９０");
            obj.transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        c_scr.isCrush = true;
    }

    //駒を配列の座標で検索
    GameObject get_Coma(Vector2 pos)
    {
        GameObject[] tagobjs = GameObject.FindGameObjectsWithTag("Coma");

        for (int i = 0; i < tagobjs.Length; i++)
        {
            Coma coma = tagobjs[i].GetComponent<Coma>();

            if(coma.position == pos)
            {
                return tagobjs[i];
            }
        }

        Debug.Log("オブジェクトは入っていないよ");
        return null;
    }

    //配列の範囲外かどうかを調べる処理
    bool isRange(float X, float Y)
    {
        if (X < 0 || X > 6)
        {
            return false;
        }
        if (Y < 0 || Y > 16)
        {
            return false;
        }

        return true;
    }
}