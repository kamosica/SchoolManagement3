//大石健太
using UnityEngine;
using System.Collections;
using System.Collections.Generic;//リストに必要

public class Map : MonoBehaviour {

    public GameObject map_obj;     //マップタイルのプレハブ

    public GameObject CsvManager_obj;
    CsvManager CsvManager_scr;

    public int[,] map_array = {     { 0,0,0,0,0,0,0,0,0,0},
                                    { 0,0,0,0,0,0,0,0,0,0},
                                    { 0,0,0,0,0,0,0,0,0,0},
                                    { 0,0,0,0,0,0,0,0,0,0},
                                    { 0,0,0,0,0,0,0,0,0,0},
                                    { 0,0,0,0,0,0,0,0,0,0},
                                    { 0,0,0,0,0,0,0,0,0,0},
                                    { 0,0,0,0,0,0,0,0,0,0},
                                    { 0,0,0,0,0,0,0,0,0,0},
                                    { 0,0,0,0,0,0,0,0,0,0}  };

    //public List<Facility> v_facility = new List<Facility>();
    public List<string[]> facility_list = new List<string[]>();
    public int facilityID = 0;

    public GameObject[] Facility_obj;   //建物のオブジェクト

	void Start () {

        CsvManager_scr = CsvManager_obj.GetComponent<CsvManager>();

        //タイルの作成
        for (int y = 0;y < 10; y++)
        {
            for(int x = 0;x < 10;x++)
            {
                if (map_array[y, x] == 0)
                {
                    Instantiate(map_obj,new Vector3(x,0,y), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                }
            }
        }

        //マップの読み込み
        string[,] m_list = CsvManager_scr.CsvRead("MapList");
        for (int i = 0; i < m_list.GetLength(0); i++)
        {
            for (int j = 0; j < m_list.GetLength(1); j++)
            {
                map_array[i, j] = int.Parse(m_list[i, j]);
            }
        }

        Array_Log();

        //建物の読み込み
        string[,] f_list = CsvManager_scr.CsvRead("FacilityList");

        for (int i = 0; i < f_list.GetLength(0); i++)
        {
            string[] str = new string[9];

            for (int j = 0; j < f_list.GetLength(1); j++)
            {
                if (j == f_list.GetLength(1) - 1) str[j] = i.ToString();    //listの最後の番号はFacilityIDなので読み込みの時に0からふり直す
                else str[j] = f_list[i, j];
            }

            facility_list.Add(str);
        }

        for (int i = 0; i < facility_list.Count; i++)
        {
            string[] f_scr = facility_list[i];

            int Facility_Num = int.Parse(f_scr[0]);
            Vector3 position = new Vector3(float.Parse(f_scr[1]), float.Parse(f_scr[2]), float.Parse(f_scr[3]));
            int RotateY      = int.Parse(f_scr[4]);
            Vector3 size     = new Vector3(float.Parse(f_scr[5]), float.Parse(f_scr[6]), float.Parse(f_scr[7]));

            GameObject obj = null;
            if (RotateY == 0 || RotateY == 180)
            {
                obj = (GameObject)Instantiate
                        (   Facility_obj[Facility_Num - 1],
                            new Vector3(position.x + (size.x / 2 - 0.5f), position.y, position.z + (size.z / 2 - 0.5f)),
                            Quaternion.Euler(0.0f, RotateY, 0.0f)
                        );
            }
            else if (RotateY == 90 || RotateY == 270)
            {
                obj = (GameObject)Instantiate
                        (   Facility_obj[Facility_Num - 1],
                            new Vector3(position.x + (size.z / 2 - 0.5f), position.y, position.z + (size.x / 2 - 0.5f)),
                            Quaternion.Euler(0.0f, RotateY, 0.0f)
                        );
            }
            Facility facility_scr = obj.GetComponent<Facility>();
            facility_scr.Facility_Num = Facility_Num;
            facility_scr.position = position;
            facility_scr.RotateY = RotateY;
            facility_scr.size = size;
            facility_scr.list_ID = facilityID;

            facilityID++;
        }

        Array_Log2();

    }

	void Update () {

        //デバック用　MapListとFacilityListの初期化
        if(Input.GetKey(KeyCode.C))
        {
            for(int i = 0;i < 10;i++)
            {
                for(int j = 0;j < 10;j++)
                {
                    map_array[i, j] = 0;
                }
            }

           facility_list.Clear();

            CsvManager_scr.CsvWrite("MapList", map_array);
            CsvManager_scr.CsvWrite("FacilityList", facility_list);
        }

    }

    //マップの配列をデバックログで表示する関数
    public void Array_Log(){
        string print_array = "";
        for (int i = 0; i < map_array.GetLength(0); i++)
        {
            for (int j = 0; j < map_array.GetLength(1); j++)
            {
                print_array += map_array[i, j].ToString() + ":";
            }
            print_array += "\n";
        }

        Debug.Log(print_array);
    }

    public void Array_Log2()
    {
        string print_array = "";
        for (int i = 0; i < facility_list.Count; i++)
        {
            string[] str = facility_list[i];

            for (int j = 0; j < str.Length; j++)
            {
                print_array += str[j].ToString() + ":";
            }
            print_array += "\n";
        }

        Debug.Log(print_array);
    }
}
