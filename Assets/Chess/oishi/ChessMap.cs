using UnityEngine;
using System.Collections;

public class ChessMap : MonoBehaviour {

    public int[,] Map_array = { 
                                { 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 22, 0, 23, 0, 22, 0 },
                                { 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 21, 0, 21, 0, 21, 0 },
                                { 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 11, 0, 11, 0, 11, 0 },
                                { 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 12, 0, 13, 0, 12, 0 },
                                { 0, 0, 0, 0, 0, 0, 0 }
                              }; //マップ配列

    int Map_width = 3 * 2 + 1;
    int Map_height = 8 * 2 + 1;

    public GameObject MapTile;
    public GameObject MapTile2;
    public GameObject MapTile3;

    float map_interval = 0.7f;

    public GameObject Coma_1;
    public GameObject Coma_2;
    public GameObject Coma_King;

    // Use this for initialization
    void Start () {
        //Map_array = new int[Map_width, Map_height];

        float width = 0;
        float height = 0;

        for (int i = 0;i < Map_height; i++)
        {
            for(int j = 0;j < Map_width;j++)
            {
                if (j % 2 == 0 && i % 2 == 1)
                {
                    Instantiate(MapTile3, new Vector3(width, 0, height), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                }
                else if (i % 2 == 0 && j % 2 == 1)
                {
                    Instantiate(MapTile2, new Vector3(width, 0, height), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                }
                else if(i % 2 == 1&& j % 2 == 1)
                {
                    Instantiate(MapTile, new Vector3(width, 0, height), Quaternion.Euler(90.0f, 0.0f, 0.0f));
                }
                else
                {
                }

                //Map_array[i,j] = 0;

                if (Map_array[i,j] % 10 == 1)
                {
                    GameObject obj = (GameObject)Instantiate(Coma_1, new Vector3(width, 0.5f, height), Quaternion.Euler(0.0f, 0.0f, 0.0f));
                    Coma coma = obj.GetComponent<Coma>();
                    coma.PalyerNo = Map_array[i, j] / 10;
                    coma.ComaNo = Map_array[i, j] % 10;
                    coma.position = new Vector2(j, i);
                    coma.isCrush = false;
                }
                else if (Map_array[i, j] % 10 == 2)
                {
                    GameObject obj = (GameObject)Instantiate(Coma_2, new Vector3(width, 0.5f, height), Quaternion.Euler(0.0f, 0.0f, 0.0f));
                    Coma coma = obj.GetComponent<Coma>();
                    coma.PalyerNo = Map_array[i, j] / 10;
                    coma.ComaNo = Map_array[i, j] % 10;
                    coma.position = new Vector2(j, i);
                    coma.isCrush = false;
                }
                else if (Map_array[i, j] % 10 == 3)
                {
                    GameObject obj = (GameObject)Instantiate(Coma_King, new Vector3(width, 0.5f, height), Quaternion.Euler(0.0f, 0.0f, 0.0f));
                    Coma coma = obj.GetComponent<Coma>();
                    coma.PalyerNo = Map_array[i, j] / 10;
                    coma.ComaNo = Map_array[i, j] % 10;
                    coma.position = new Vector2(j, i);
                    coma.isCrush = false;
                }

                width += map_interval;

            }

            width = 0;

            height += map_interval;
        }

        Array_Log();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void ComaGenerator(int i)
    {

    }

    public void Array_Log()
    {
        string print_array = "";
        for (int i = 0; i < Map_array.GetLength(0); i++)
        {
            for (int j = 0; j < Map_array.GetLength(1); j++)
            {
                print_array += Map_array[i, j].ToString() + ":";
            }
            print_array += "\n";
        }

        Debug.Log(print_array);
    }

}
