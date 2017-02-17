//大石健太
//施設を配置するクラス
using UnityEngine;
using System.Collections;

public class FacilityPlacement : MonoBehaviour {

    int CurrentFacility_No = 0; //現在選択している施設の番号
    public GameObject Facility_obj; //現在選択している施設オブジェクト

    bool isSelect = false;  //施設を選択しているかどうか

    public GameObject Map_obj;
    Map Map_scr;
    public GameObject CsvManager_obj;
    CsvManager CsvManager_scr;

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

                        if(facility.list_num != -1)Map_scr.facility_list.RemoveAt(facility.list_num);//施設リストから選択した施設を削除
                        facility.list_num = -1;
                    }
                }
                else if(isSelect == true)//施設を選択しているとき
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
                    Change_Maparray((int)facility.position.x, (int)facility.position.z, (int)facility.size.x, (int)facility.size.z, 1,Facility_obj);
                    
                    Facility_obj.GetComponent<Renderer>().material.color = Color.white; //建物の色を白にする
                    Facility_obj = null;
                    isSelect = false;

                    facility.list_num = Map_scr.facility_list.Count;
                    //Map_scr.v_facility.Add(facility);

                    string[] str = {    facility.Facility_Num.ToString(),   //施設番号
                                        facility.position.x.ToString(),     //X座標
                                        facility.position.y.ToString(),     //Y座標
                                        facility.position.z.ToString(),     //Z座標
                                        facility.RotateY.ToString(),        //Yの回転
                                        facility.size.x.ToString(),         //X方向の大きさ
                                        facility.size.y.ToString(),         //Y方向の大きさ
                                        facility.size.z.ToString(),         //Z方向の大きさ
                                    };
                    Map_scr.facility_list.Add(str);
                    Map_scr.Array_Log2();
                    CsvManager_scr.CsvWrite("FacilityList", Map_scr.facility_list);
                }

                //Map_scr.Array_Log();
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

                if((X < 0 || X > 9) || (Z < 0 || Z > 9))    //配列の範囲外だったらfalse
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
        if (isSelect == true && Facility_obj != null && Facility_obj.tag == "Facility")
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
            }
        }
    }

    //マップ配列を変更する関数
    void Change_Maparray(int posx,int posz,int sizex,int sizez,int i,GameObject obj)
    {
        if (posx == -100 && posz == -100) return;

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

        Debug.Log("list_num" + facility.list_num);
        if (facility.list_num != -1) Map_scr.facility_list.RemoveAt(facility.list_num);//施設リストから選択した施設を削除
        CsvManager_scr.CsvWrite("FacilityList", Map_scr.facility_list);
        Destroy(obj);
    }
}
