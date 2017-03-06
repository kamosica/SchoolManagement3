using UnityEngine;
using System.Collections;

public class ChessMap : MonoBehaviour {

    public int[,] Map_array;

    int Map_width = 3 * 2 + 1;
    int Map_height = 8 * 2 + 1;

    public GameObject MapTile;
    public GameObject MapTile2;
    public GameObject MapTile3;

    // Use this for initialization
    void Start () {
        Map_array = new int[Map_width, Map_height];
        
        for(int i = 0;i < Map_width;i++)
        {
            for(int j = 0;j < Map_height;j++)
            {
                if (i % 2 == 0 && j % 2 == 1)
                {
                    Instantiate(MapTile3, new Vector3(i, 0, j), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                }
                else if (j % 2 == 0 && i % 2 == 1)
                {
                    Instantiate(MapTile2, new Vector3(i, 0, j), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                }
                else if(i % 2 == 1&& j % 2 == 1)
                {
                    Instantiate(MapTile, new Vector3(i, 0, j), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                }
            }
        }	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
