using UnityEngine;
using System.Collections;

public class ChessMap : MonoBehaviour {

    public int[,] Map_array; //マップ配列

    int Map_width = 3 * 2 + 1;
    int Map_height = 8 * 2 + 1;

    public GameObject MapTile;
    public GameObject MapTile2;
    public GameObject MapTile3;

    float map_interval = 0.7f;

    // Use this for initialization
    void Start () {
        Map_array = new int[Map_width, Map_height];

        float width = 0;
        float height = 0;

        for (int i = 0;i < Map_width;i++)
        {
            for(int j = 0;j < Map_height;j++)
            {
                if (i % 2 == 0 && j % 2 == 1)
                {
                    Instantiate(MapTile3, new Vector3(width, 0, height), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                    height += map_interval;
                }
                else if (j % 2 == 0 && i % 2 == 1)
                {
                    Instantiate(MapTile2, new Vector3(width, 0, height), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                    height += map_interval;
                }
                else if(i % 2 == 1&& j % 2 == 1)
                {
                    Instantiate(MapTile, new Vector3(width, 0, height), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                    height += map_interval;
                }
                else
                {
                    height += map_interval;
                }

                Map_array[i,j] = 0;
            }

            height = 0;

            width += map_interval;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
