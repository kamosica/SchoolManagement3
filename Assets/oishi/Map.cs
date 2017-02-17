//大石健太
using UnityEngine;
using System.Collections;
using System.Collections.Generic;//リストに必要

public class Map : MonoBehaviour {

    public GameObject map_obj;

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

    public List<Facility> v_facility = new List<Facility>();

	void Start () {

        for(int y = 0;y < 10; y++)
        {
            for(int x = 0;x < 10;x++)
            {
                if (map_array[y, x] == 0)
                {
                    Instantiate(map_obj,new Vector3(x,0,y), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                }
            }
        }

        Debug.Log(map_array[0, 0]);
	
	}

	void Update () {

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
}
