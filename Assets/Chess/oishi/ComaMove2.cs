using UnityEngine;
using System.Collections;

public class ComaMove2 : MonoBehaviour
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

                    //駒が正方形の時
                    if (coma.isCrush == false)
                    {
                        //周りのコマチェック
                        if (chessmap.Map_array[(int)comapos.y, (int)comapos.x + 1] == 0 && comapos.x + 1 < 6)//→
                            Instantiate(MapTile, new Vector3(((int)comapos.x + 2) * interval, 1, (int)comapos.y * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));

                        if (chessmap.Map_array[(int)comapos.y, (int)comapos.x - 1] == 0 && comapos.x - 1 > 0)//左
                            Instantiate(MapTile, new Vector3(((int)comapos.x - 2) * interval, 1, (int)comapos.y * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));

                        if (chessmap.Map_array[(int)comapos.y + 1, (int)comapos.x] == 0 && comapos.y + 1 < 16)//上
                            Instantiate(MapTile, new Vector3((int)comapos.x * interval, 1, ((int)comapos.y + 2) * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));

                        if (chessmap.Map_array[(int)comapos.y - 1, (int)comapos.x] == 0 && comapos.y - 1 > 0)//下
                            Instantiate(MapTile, new Vector3((int)comapos.x * interval, 1, ((int)comapos.y - 2) * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));

                        komasentaku = true;
                    }
                    //駒が壁の時
                    else if(coma.isCrush == true)
                    {
                        CrushComaMove(comapos, clickCUBE);
                    }
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

    void CrushComaMove(Vector2 pos,GameObject coma_obj)
    {
        if (coma_obj.transform.localEulerAngles.y == 0 || coma_obj.transform.localEulerAngles.y == 180)
        {
            if(isRange(pos.x + 1, pos.y + 1) == true)
            {
                if (chessmap.Map_array[(int)pos.y + 1, (int)pos.x + 1] == 0)
                {
                    Instantiate(MapTile2, new Vector3(((int)pos.x + 1) * interval, 1, ((int)pos.y + 1) * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                }
            }
            if (isRange(pos.x - 1, pos.y + 1) == true)
            {
                if (chessmap.Map_array[(int)pos.y + 1, (int)pos.x -1] == 0)
                {
                    Instantiate(MapTile2, new Vector3(((int)pos.x - 1) * interval, 1, ((int)pos.y + 1) * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                }
            }
            if (isRange(pos.x + 1, pos.y - 1) == true)
            {
                if (chessmap.Map_array[(int)pos.y - 1, (int)pos.x + 1] == 0)
                {
                    Instantiate(MapTile2, new Vector3(((int)pos.x + 1) * interval, 1, ((int)pos.y - 1) * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                }
            }
            if (isRange(pos.x - 1, pos.y - 1) == true)
            {
                if (chessmap.Map_array[(int)pos.y - 1, (int)pos.x - 1] == 0)
                {
                    Instantiate(MapTile2, new Vector3(((int)pos.x - 1) * interval, 1, ((int)pos.y - 1) * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                }
            }

            if (isRange(pos.x,pos.y + 2) == true)//上
            {
                if (chessmap.Map_array[(int)pos.y + 2, (int)pos.x] == 0)
                {
                    Instantiate(MapTile2, new Vector3((int)pos.x * interval, 1, ((int)pos.y + 2) * interval), Quaternion.Euler(90.0f, 90.0f, 0.0f));
                }
            }

            if (isRange(pos.x,pos.y - 2) == true)//下
            {
                if (chessmap.Map_array[(int)pos.y - 2, (int)pos.x] == 0)
                {
                    Instantiate(MapTile2, new Vector3((int)pos.x * interval, 1, ((int)pos.y - 2) * interval), Quaternion.Euler(90.0f, 90.0f, 0.0f));
                }
            }
        }
        else if(coma_obj.transform.localEulerAngles.y == 90 || coma_obj.transform.localEulerAngles.y == 270)
        {
            if (isRange(pos.x + 1, pos.y + 1) == true)
            {
                if (chessmap.Map_array[(int)pos.y + 1, (int)pos.x + 1] == 0)
                {
                    Instantiate(MapTile2, new Vector3(((int)pos.x + 1) * interval, 1, ((int)pos.y + 1) * interval), Quaternion.Euler(90.0f, 90.0f, 0.0f));
                }
            }
            if (isRange(pos.x - 1, pos.y + 1) == true)
            {
                if (chessmap.Map_array[(int)pos.y + 1, (int)pos.x - 1] == 0)
                {
                    Instantiate(MapTile2, new Vector3(((int)pos.x - 1) * interval, 1, ((int)pos.y + 1) * interval), Quaternion.Euler(90.0f, 90.0f, 0.0f));
                }
            }
            if (isRange(pos.x + 1, pos.y - 1) == true)
            {
                if (chessmap.Map_array[(int)pos.y - 1, (int)pos.x + 1] == 0)
                {
                    Instantiate(MapTile2, new Vector3(((int)pos.x + 1) * interval, 1, ((int)pos.y - 1) * interval), Quaternion.Euler(90.0f, 90.0f, 0.0f));
                }
            }
            if (isRange(pos.x - 1, pos.y - 1) == true)
            {
                if (chessmap.Map_array[(int)pos.y - 1, (int)pos.x - 1] == 0)
                {
                    Instantiate(MapTile2, new Vector3(((int)pos.x - 1) * interval, 1, ((int)pos.y - 1) * interval), Quaternion.Euler(90.0f, 90.0f, 0.0f));
                }
            }

            if (isRange(pos.x, pos.x + 2) == true)//上
            {
                if (chessmap.Map_array[(int)pos.y, (int)pos.x + 2] == 0)
                {
                    Instantiate(MapTile2, new Vector3(((int)pos.x + 2) * interval, 1, (int)pos.y * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                }
            }

            if (isRange(pos.x, pos.x - 2) == true)//下
            {
                if (chessmap.Map_array[(int)pos.y, (int)pos.x - 2] == 0)
                {
                    Instantiate(MapTile2, new Vector3(((int)pos.x - 2) * interval, 1, (int)pos.y * interval), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                }
            }
        }
    }

    //配列の範囲外かどうかを調べる処理
    bool isRange(float X,float Y)
    {
        if(X < 0 || X > 6)
        {
            return false;
        }
        if(Y < 0 || Y > 16)
        {
            return false;
        }

        return true;
    }
}

