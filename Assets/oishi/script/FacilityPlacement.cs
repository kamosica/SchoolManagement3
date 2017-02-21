//大石健太
//施設を配置するクラス
using UnityEngine;
using System.Collections;
using System.Collections.Generic;//リストに必要

public class FacilityPlacement : MonoBehaviour {

    int CurrentFacility_No = 0; //現在選択している施設の番号
    public GameObject Facility_obj; //現在選択している施設オブジェクト

    bool isSelect = false;  //施設を選択しているかどうか

    public GameObject Map_obj;
    Map Map_scr;
    public GameObject CsvManager_obj;
    CsvManager CsvManager_scr;

    public GameObject Tile;

    List<GameObject> v_RoomTile = new List<GameObject>();

    // Use this for initialization
    void Start () {
        Map_scr = Map_obj.GetComponent<Map>();
        CsvManager_scr = CsvManager_obj.GetComponent<CsvManager>();	
	}
	
	// Update is called once per frame
	void Update () {

        //左ボタンを押した時の処理
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (isSelect == false)//施設を選択していないとき
                {
                    Facility_obj = hit.collider.gameObject;

                    if (Facility_obj.tag == "Facility")
                    {
                        isSelect = true;

                        Facility_obj.GetComponent<Renderer>().material.color = Color.blue;  //建物の色を青にする

                        Facility facility = Facility_obj.GetComponent<Facility>();

                        Change_Maparray((int)facility.position.x, (int)facility.position.z, (int)facility.size.x, (int)facility.size.z, 0,Facility_obj); //マップリストを更新

                        if(facility.list_ID != -1)ID_Destroy(facility.list_ID);//施設リストから選択した施設を削除
                    }
                    else if(Facility_obj.tag == "Classroom")
                    {
                        isSelect = true;

                        Facility_obj.GetComponent<Renderer>().material.color = Color.blue;  //建物の色を青にする

                        Classroom classroom = Facility_obj.GetComponent<Classroom>();

                        Change_Maparray((int)classroom.position.x, (int)classroom.position.z, (int)classroom.size.x, (int)classroom.size.z, 0, Facility_obj); //マップリストを更新

                        

                        GameObject[] tagobjs = GameObject.FindGameObjectsWithTag("Classroom");  //Classroomのオブジェクトを探す
                        for (int i = 0; i < tagobjs.Length; i++)
                        {
                            if (tagobjs[i] == Facility_obj) continue;

                            Vector3 pos = tagobjs[i].transform.position;
                            Vector3 size = tagobjs[i].transform.localScale;

                            float Y = (pos.y - size.y / 2) + 0.05f;

                            GameObject obj1 = (GameObject)Instantiate(Tile, new Vector3(pos.x + size.x, Y, pos.z), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                            v_RoomTile.Add(obj1);
                            GameObject obj2 = (GameObject)Instantiate(Tile, new Vector3(pos.x - size.x, Y, pos.z), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                            v_RoomTile.Add(obj2);
                            GameObject obj3 = (GameObject)Instantiate(Tile, new Vector3(pos.x, Y, pos.z + size.z), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                            v_RoomTile.Add(obj3);
                            GameObject obj4 = (GameObject)Instantiate(Tile, new Vector3(pos.x, Y, pos.z - size.z), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                            v_RoomTile.Add(obj4);
                            GameObject obj5 = (GameObject)Instantiate(Tile, new Vector3(pos.x, Y + size.y, pos.z), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                            v_RoomTile.Add(obj5);
                        }
                    }
                }
                else if(isSelect == true)//施設を選択しているとき
                {
                    if (Facility_obj.tag == "Facility")
                    {
                        Facility facility = Facility_obj.GetComponent<Facility>();
                        if (Facility_obj.transform.localEulerAngles.y == 0 || Facility_obj.transform.localEulerAngles.y == 180)
                        {
                            facility.position = new Vector3(Facility_obj.transform.position.x - (Facility_obj.transform.localScale.x / 2 - 0.5f),
                                                            Facility_obj.transform.position.y,
                                                            Facility_obj.transform.position.z - (Facility_obj.transform.localScale.z / 2 - 0.5f));
                        }
                        else if (Facility_obj.transform.localEulerAngles.y == 90 || Facility_obj.transform.localEulerAngles.y == 270)
                        {
                            facility.position = new Vector3(Facility_obj.transform.position.x - (Facility_obj.transform.localScale.z / 2 - 0.5f),
                                                            Facility_obj.transform.position.y,
                                                            Facility_obj.transform.position.z - (Facility_obj.transform.localScale.x / 2 - 0.5f));
                        }

                        if (isDeploy_Facility((int)facility.position.x, (int)facility.position.z, (int)facility.size.x, (int)facility.size.z, 1) == false)
                        {
                            return;
                        }
                        Change_Maparray((int)facility.position.x, (int)facility.position.z, (int)facility.size.x, (int)facility.size.z, 1, Facility_obj);

                        Facility_obj.GetComponent<Renderer>().material.color = Color.white; //建物の色を白にする
                        Facility_obj = null;
                        isSelect = false;

                        string[] str = {    facility.Facility_Num.ToString(),   //施設番号
                                            facility.position.x.ToString(),     //X座標
                                            facility.position.y.ToString(),     //Y座標
                                            facility.position.z.ToString(),     //Z座標
                                            facility.RotateY.ToString(),        //Yの回転
                                            facility.size.x.ToString(),         //X方向の大きさ
                                            facility.size.y.ToString(),         //Y方向の大きさ
                                            facility.size.z.ToString(),         //Z方向の大きさ
                                            facility.list_ID.ToString()         //建物ID
                                    };
                        Map_scr.facility_list.Add(str);
                        Map_scr.Array_Log2();
                        CsvManager_scr.CsvWrite("FacilityList", Map_scr.facility_list);
                    }
                    else if (Facility_obj.tag == "Classroom")
                    {
                        Classroom facility = Facility_obj.GetComponent<Classroom>();
                        if (Facility_obj.transform.localEulerAngles.y == 0 || Facility_obj.transform.localEulerAngles.y == 180)
                        {
                            facility.position = new Vector3(Facility_obj.transform.position.x - (Facility_obj.transform.localScale.x / 2 - 0.5f),
                                                            Facility_obj.transform.position.y,
                                                            Facility_obj.transform.position.z - (Facility_obj.transform.localScale.z / 2 - 0.5f));
                        }
                        else if (Facility_obj.transform.localEulerAngles.y == 90 || Facility_obj.transform.localEulerAngles.y == 270)
                        {
                            facility.position = new Vector3(Facility_obj.transform.position.x - (Facility_obj.transform.localScale.z / 2 - 0.5f),
                                                            Facility_obj.transform.position.y,
                                                            Facility_obj.transform.position.z - (Facility_obj.transform.localScale.x / 2 - 0.5f));
                        }

                        //施設が配置できるかどうかの判断
                        if (isDeploy_Facility((int)facility.position.x, (int)facility.position.z, (int)facility.size.x, (int)facility.size.z, 1) == false)
                        {
                            return;
                        }
                        Change_Maparray((int)facility.position.x, (int)facility.position.z, (int)facility.size.x, (int)facility.size.z, 1, Facility_obj);

                        Facility_obj.GetComponent<Renderer>().material.color = Color.white; //建物の色を白にする
                        Facility_obj = null;
                        isSelect = false;

                        //RoomTileをすべて削除
                        for(int i = v_RoomTile.Count - 1;i > -1;i--)
                        {
                            Destroy(v_RoomTile[i]);
                        }
                        v_RoomTile.Clear();
                    }
                }

                Map_scr.Array_Log();
            }
            else
            {
                //Facility_obj = null;
            }
        }

        //右ボタンを押した時回転させる処理
        if (Input.GetMouseButtonDown(1))
        {
            
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.tag == "Facility")
                {
                    if (isSelect == true)//施設を選択しているとき
                    {
                        Facility_obj.transform.Rotate(new Vector3(0f, 90f, 0f));//施設を回転させる
                        Facility facility = Facility_obj.GetComponent<Facility>();
                        facility.RotateY = (int)Facility_obj.transform.localEulerAngles.y;
                    }
                    else if (isSelect == false)//施設を選択していないとき
                    {
                        //施設を消す処理（仮）
                        Facility_Destroy(obj);
                    }
                }
            }
        }

        //施設を移動させる処理
        FacilityMove();
    }

    //建物を配置できるかどうかを判断する関数
    bool isDeploy_Facility(int posx, int posz, int sizex, int sizez, int i)
    {
        for (int x = 0; x < sizex; x++)
        {
            for (int z = 0; z < sizez; z++)
            {
                int mapnum = 0; //マップの番号　0は空き　1は設置済み
                int Z = 0;  //配列のZ座標
                int X = 0;  //配列のX座標
                if (Facility_obj.transform.localEulerAngles.y == 0 || Facility_obj.transform.localEulerAngles.y == 180)
                {
                    Z = posz + z;
                    X = posx + x;
                }
                else if (Facility_obj.transform.localEulerAngles.y == 90 || Facility_obj.transform.localEulerAngles.y == 270)
                {
                    Z = posz + x;
                    X = posx + z;
                }

                if((X < 0 || X > Map_scr.Map_LengthX - 1) || (Z < 0 || Z > Map_scr.Map_LengthY - 1))    //配列の範囲外だったらfalse
                {
                    Debug.Log("範囲外です　置けないよ");
                    return false;
                }

                mapnum = Map_scr.map_array[Z, X];
                if (mapnum == 1) //マップ配列に1があったときはすでにほかの施設が設置させれているのでfalse
                {
                    Debug.Log("他の建物と被っています　置けないよ");
                    return false;
                }
            }
        }

        return true;
    }

    //施設を移動する関数
    void FacilityMove()
    {
        if (isSelect == true && Facility_obj != null && (Facility_obj.tag == "Facility" || Facility_obj.tag == "Classroom"))
        {
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject maptile = hit.collider.gameObject;

                if (maptile.tag == "MapTile")
                {
                    Vector3 pos = maptile.transform.position;
                    //Facility_obj.transform.position = new Vector3(pos.x, pos.y + Facility_obj.transform.localScale.y / 2, pos.z);

                    Vector3 f_pos = new Vector3(0, 0, 0);
                    if (Facility_obj.transform.localEulerAngles.y == 0 || Facility_obj.transform.localEulerAngles.y == 180)
                    {
                        Vector3 size = Facility_obj.transform.localScale;
                        f_pos = new Vector3(pos.x + (size.x - 1) * 0.5f, pos.y + Facility_obj.transform.localScale.y / 2, pos.z + (size.z - 1) * 0.5f);
                    }
                    else if (Facility_obj.transform.localEulerAngles.y == 90 || Facility_obj.transform.localEulerAngles.y == 270)
                    {
                        Vector3 size = Facility_obj.transform.localScale;
                        f_pos = new Vector3(pos.x + (size.z - 1) * 0.5f, pos.y + Facility_obj.transform.localScale.y / 2, pos.z + (size.x - 1) * 0.5f);
                    }

                    Facility_obj.transform.position = f_pos;

                    //Facility_obj.transform.position = new Vector3(pos.x + 1 + 0.5f, pos.y + Facility_obj.transform.localScale.y / 2, pos.z);
                    //Debug.Log(pos.y + Facility_obj.transform.localScale.y / 2);
                }
                else if(maptile.tag == "RoomTile")
                {
                    Vector3 pos = maptile.transform.position;

                    Vector3 f_pos = new Vector3(0, 0, 0);
                    if (Facility_obj.transform.localEulerAngles.y == 0 || Facility_obj.transform.localEulerAngles.y == 180)
                    {
                        Vector3 size = Facility_obj.transform.localScale;
                        f_pos = new Vector3(pos.x + (size.x - 1) * 0.5f, pos.y + Facility_obj.transform.localScale.y / 2 - 0.05f, pos.z + (size.z - 1) * 0.5f);
                    }
                    else if (Facility_obj.transform.localEulerAngles.y == 90 || Facility_obj.transform.localEulerAngles.y == 270)
                    {
                        Vector3 size = Facility_obj.transform.localScale;
                        f_pos = new Vector3(pos.x + (size.z - 1) * 0.5f, pos.y + Facility_obj.transform.localScale.y / 2 - 0.05f, pos.z + (size.x - 1) * 0.5f);
                    }

                    Facility_obj.transform.position = f_pos;
                }
            }
        }
    }

    //マップ配列を変更する関数
    void Change_Maparray(int posx,int posz,int sizex,int sizez,int i,GameObject obj)
    {
        if (posx == -100 && posz == -100) return;

        Debug.Log("pX" + posx  + " pZ" + posz );
        Debug.Log("sX" + sizex + " sZ" + sizez);

        for (int x = 0; x < sizex; x++)
        {
            for(int z = 0;z < sizez;z++)
            {
                if (obj.transform.localEulerAngles.y == 0 || obj.transform.localEulerAngles.y == 180)
                {
                    Map_scr.map_array[posz + z, posx + x] = i;
                }
                else if (obj.transform.localEulerAngles.y == 90 || obj.transform.localEulerAngles.y == 270)
                {
                    Map_scr.map_array[posz + x, posx + z] = i;
                }
            }
        }

        CsvManager_scr.CsvWrite("MapList", Map_scr.map_array);  //マップの情報をCSVに書き込む
    }

    //施設を消す処理
    void Facility_Destroy(GameObject obj)
    {
        Facility facility = obj.GetComponent<Facility>();
        Change_Maparray((int)facility.position.x, (int)facility.position.z, (int)facility.size.x, (int)facility.size.z, 0,obj); //マップリストを更新

        Debug.Log("list_ID" + facility.list_ID);

        if (facility.list_ID != -1) ID_Destroy(facility.list_ID);
        //if (facility.list_ID != -1) Map_scr.facility_list.RemoveAt(facility.list_ID);//施設リストから選択した施設を削除
        //CsvManager_scr.CsvWrite("FacilityList", Map_scr.facility_list);
        Destroy(obj);
    }

    //建物リストから削除する処理
    void ID_Destroy(int id)
    {
        for(int i = Map_scr.facility_list.Count - 1; i > -1;i--)
        {
            if(int.Parse(Map_scr.facility_list[i][8]) == id)
            {
                Debug.Log("ID" + id);
                Map_scr.facility_list.RemoveAt(i);
                CsvManager_scr.CsvWrite("FacilityList", Map_scr.facility_list);
            }
        }
    }
}
