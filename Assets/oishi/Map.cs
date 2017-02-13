using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

    public GameObject map_obj;

    int[,] map_array = {    { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0}  };

	// Use this for initialization
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
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
