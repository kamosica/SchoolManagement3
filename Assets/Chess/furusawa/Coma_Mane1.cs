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

    public GameObject TurnManager_obj;
    TurnManager TurnManager_scr;

    GameObject BeforeCUBE;  //前に行動したのコマを保存
    int BeforeMove = 0;     //前に移動した方向の保存 　1.右　2.左  3.上　4.下

    int KoudouCount = 0;    //何回行動したかのカウント

    // Use this for initialization
    void Start()
    {
        chessmap = GetComponent<ChessMap>();
        TurnManager_scr = TurnManager_obj.GetComponent<TurnManager>();
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

                        if (TurnManager_scr.turn_count % 2 == coma.PalyerNo - 1)
                        {
                            Create_SentakuMap();
                        }
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

                        //駒が正方形の時
                        if (coma.isCrush == false)
                        {
                            int coma_number = coma.PalyerNo * 10 + coma.ComaNo;

                            if (clickMAP == PanelMIGI)
                            {
                                Hit_Coma(new Vector2(comapos.x + 2, comapos.y), new Vector2(1, 0));

                                chessmap.Map_array[(int)comapos.y, (int)comapos.x + 2] = coma_number;
                                coma.position = new Vector2(comapos.x + 2, comapos.y);

                                BeforeMove = 1;
                            }
                            if (clickMAP == PanelHIDARI)
                            {
                                Hit_Coma(new Vector2(comapos.x - 2, comapos.y), new Vector2(-1, 0));

                                chessmap.Map_array[(int)comapos.y, (int)comapos.x - 2] = coma_number;
                                coma.position = new Vector2(comapos.x - 2, comapos.y);

                                BeforeMove = 2;
                            }
                            if (clickMAP == PanelUE)
                            {
                                Hit_Coma(new Vector2(comapos.x, comapos.y + 2), new Vector2(0, 1));

                                chessmap.Map_array[(int)comapos.y + 2, (int)comapos.x] = coma_number;
                                coma.position = new Vector2(comapos.x, comapos.y + 2);

                                BeforeMove = 3;
                            }
                            if (clickMAP == PanelSITA)
                            {
                                Hit_Coma(new Vector2(comapos.x, comapos.y - 2), new Vector2(0, -1));

                                chessmap.Map_array[(int)comapos.y - 2, (int)comapos.x] = coma_number;
                                coma.position = new Vector2(comapos.x, comapos.y - 2);

                                BeforeMove = 4;
                            }

                            BeforeCUBE = clickCUBE;

                            chessmap.Map_array[(int)comapos.y, (int)comapos.x] = 0;

                            Destroy_SentakuMap();
                        }

                        //駒が壁の時
                        else if (coma.isCrush == true)
                        {
                            CrushComaMove2(clickMAP.transform.position);
                        }

                        KoudouCount++;
                        if(KoudouCount == 2)
                        {
                            TurnManager_scr.Change_Text();  //ターン切り替えの処理
                            KoudouCount = 0;
                        }
                        komasentaku = false;
                    }
                    else if (hit.collider.gameObject.tag == ("Coma"))//タグがコマで
                    {
                        if (hit.collider.gameObject == clickCUBE)
                        {
                            Destroy_SentakuMap();

                            clickCUBE = null;
                            komasentaku = false;
                        }
                        else
                        {
                            GameObject obj = hit.collider.gameObject;//クリックしたオブジェクト
                            coma = obj.GetComponent<Coma>();

                            if (TurnManager_scr.turn_count % 2 == coma.PalyerNo - 1)
                            {
                                clickCUBE = obj;
                                Create_SentakuMap();
                                komasentaku = false;
                            }
                        }
                    }

                    chessmap.Array_Log();
                }
            }
        }
    }

    void Create_SentakuMap()
    {
        Destroy_SentakuMap();

        coma = clickCUBE.GetComponent<Coma>();
        comapos = coma.position;

        if (BeforeCUBE != clickCUBE) BeforeMove = 0;    //前に行動した駒と今回選択した駒を見て違ったら以前の移動に0を入れる

        //駒が正方形の時
        if (coma.isCrush == false)
        {
            //周りのコマチェック。行ける所にオブジェクトが出る
            if (chessmap.Map_array[(int)comapos.y, (int)comapos.x + 1] == 0 && comapos.x + 1 < 6 && BeforeMove != 1)//右
            {
                PanelMIGI = (GameObject)Instantiate(MapTile, new Vector3(((int)comapos.x + 2) * interval, 1, (int)comapos.y * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));
            }

            if (chessmap.Map_array[(int)comapos.y, (int)comapos.x - 1] == 0 && comapos.x - 1 > 0 && BeforeMove != 2)//左
            {
                PanelHIDARI = (GameObject)Instantiate(MapTile, new Vector3(((int)comapos.x - 2) * interval, 1, (int)comapos.y * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));
            }

            if (chessmap.Map_array[(int)comapos.y + 1, (int)comapos.x] == 0 && comapos.y + 1 < 16 && BeforeMove != 3)//上
            {
                PanelUE = (GameObject)Instantiate(MapTile, new Vector3((int)comapos.x * interval, 1, ((int)comapos.y + 2) * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));
            }

            if (chessmap.Map_array[(int)comapos.y - 1, (int)comapos.x] == 0 && comapos.y - 1 > 0 && BeforeMove != 4)//下
            {
                PanelSITA = (GameObject)Instantiate(MapTile, new Vector3((int)comapos.x * interval, 1, ((int)comapos.y - 2) * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));
            }

            komasentaku = true;//選択した
        }
        //駒が壁の時
        else if (coma.isCrush == true)
        {
            CrushComaMove(comapos, clickCUBE);
            komasentaku = true;//選択した
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

        //元の壁を消す処理
        GameObject kabe = get_Coma(pos + vec);
        if (kabe != null)
        {
            Destroy(kabe);

            Debug.Log("壁を消す");
        }

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

        //勝利判定
        if(c_scr.ComaNo == 3)
        {
            TurnManager_scr.Create_EndPanel(c_scr.PalyerNo);
        }
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

    //駒が壁の時
    void CrushComaMove(Vector2 pos, GameObject coma_obj)
    {
        Vector2[] vec = { new Vector2(1, 1), new Vector2(-1, 1), new Vector2(1, -1), new Vector2(-1, -1) };

        if (coma_obj.transform.localEulerAngles.y == 0 || coma_obj.transform.localEulerAngles.y == 180)
        {
            for(int i = 0;i < 4;i++)
            {
                if (isRange(pos.x + vec[i].x, pos.y + vec[i].y) == true)
                {
                    if (chessmap.Map_array[(int)(pos.y + vec[i].y), (int)(pos.x + vec[i].x)] == 0)
                    {
                        Instantiate(MapTile2, new Vector3(((int)pos.x + vec[i].x) * interval, 1, ((int)pos.y + vec[i].y) * interval), Quaternion.Euler(90.0f, 90.0f, 0.0f));
                    }
                }
            }

            if (isRange(pos.x, pos.y + 2) == true)//上
            {
                if (chessmap.Map_array[(int)pos.y + 2, (int)pos.x] == 0)
                {
                    Instantiate(MapTile2, new Vector3((int)pos.x * interval, 1, ((int)pos.y + 2) * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                }
            }

            if (isRange(pos.x, pos.y - 2) == true)//下
            {
                if (chessmap.Map_array[(int)pos.y - 2, (int)pos.x] == 0)
                {
                    Instantiate(MapTile2, new Vector3((int)pos.x * interval, 1, ((int)pos.y - 2) * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                }
            }
        }
        else if (coma_obj.transform.localEulerAngles.y == 90 || coma_obj.transform.localEulerAngles.y == 270)
        {
            for (int i = 0; i < 4; i++)
            {
                if (isRange(pos.x + vec[i].x, pos.y + vec[i].y) == true)
                {
                    if (chessmap.Map_array[(int)(pos.y + vec[i].y), (int)(pos.x + vec[i].x)] == 0)
                    {
                        Instantiate(MapTile2, new Vector3(((int)pos.x + vec[i].x) * interval, 1, ((int)pos.y + vec[i].y) * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                    }
                }
            }

            if (isRange(pos.x + 2, pos.y) == true)//上
            {
                if (chessmap.Map_array[(int)pos.y, (int)pos.x + 2] == 0)
                {
                    Instantiate(MapTile2, new Vector3(((int)pos.x + 2) * interval, 1, (int)pos.y * interval), Quaternion.Euler(90.0f, 90.0f, 0.0f));
                }
            }

            if (isRange(pos.x - 2, pos.y) == true)//下
            {
                if (chessmap.Map_array[(int)pos.y, (int)pos.x - 2] == 0)
                {
                    Instantiate(MapTile2, new Vector3(((int)pos.x - 2) * interval, 1, (int)pos.y * interval), Quaternion.Euler(90.0f, 90.0f, 0.0f));
                }
            }
        }
    }

    //パネルを選択したとき壁を移動する処理
    void CrushComaMove2(Vector3 tilepos)
    {
        int coma_number = coma.PalyerNo * 10 + coma.ComaNo;

        //Debug.Log("タイルの座標　X" + Mathf.RoundToInt(tilepos.x / 0.7f) + "　Y" + Mathf.RoundToInt(tilepos.z / 0.7f));

        chessmap.Map_array[(int)coma.position.y, (int)coma.position.x] = 0;
        chessmap.Map_array[Mathf.RoundToInt(tilepos.z / 0.7f), Mathf.RoundToInt(tilepos.x / 0.7f)] = coma_number;
        coma.position = new Vector2(Mathf.RoundToInt(tilepos.x / 0.7f), Mathf.RoundToInt(tilepos.z / 0.7f));

        //Debug.Log("ComaPosition X" + coma.position.x + " Y" + coma.position.y);

        Destroy_SentakuMap();

        if ((int)coma.position.x % 2 == 0 && (int)coma.position.y % 2 == 1)
        {
            Debug.Log("回転０");
            clickCUBE.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if ((int)coma.position.x % 2 == 1 && (int)coma.position.y % 2 == 0)
        {
            Debug.Log("回転９０");
            clickCUBE.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    //選択パネルの全削除
    void Destroy_SentakuMap()
    {
        GameObject[] tagobjs = GameObject.FindGameObjectsWithTag("SentakuMap");
        for (int i = 0; i < tagobjs.Length; i++)
        {
            Destroy(tagobjs[i]);
        }
    }
}