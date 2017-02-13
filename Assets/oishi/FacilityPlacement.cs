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

    // Use this for initialization
    void Start () {
        Map_scr = Map_obj.GetComponent<Map>();	
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
                    isSelect = true;

                    Facility_obj.GetComponent<Renderer>().material.color = Color.blue;

                    Vector3 pos = Facility_obj.transform.position;
                    //Debug.Log("Y"+ (int)pos.z + " X"+ (int)pos.x);
                    //Debug.Log(Map_scr.map_array[0, 0]);
                    Map_scr.map_array[(int)pos.z,(int)pos.x] = 0;
                }
                else if(isSelect == true)//施設を選択しているとき
                {
                    Vector3 pos = Facility_obj.transform.position;
                    Map_scr.map_array[(int)pos.z, (int)pos.x] = 1;

                    Facility_obj.GetComponent<Renderer>().material.color = Color.white;
                    Facility_obj = null;
                    isSelect = false;
                }
            }
            else
            {
                //Facility_obj = null;
            }
        }

        //右ボタンを押した時回転させる処理
        if (Input.GetMouseButtonDown(1))
        {
            if (isSelect == true)//施設を選択しているとき
            {
                Facility_obj.transform.Rotate(new Vector3(0f, 90f, 0f));
            }
        }

            //施設を移動させる処理
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

                    Vector3 f_pos = new Vector3(0,0,0);
                    if(Facility_obj.transform.localEulerAngles.y == 0 || Facility_obj.transform.localEulerAngles.y == 180)
                    {
                        Vector3 size = Facility_obj.transform.localScale;
                        f_pos = new Vector3(pos.x + (size.x - 1) * 0.5f, pos.y + Facility_obj.transform.localScale.y / 2, pos.z + (size.z - 1) * 0.5f);
                    }
                    if (Facility_obj.transform.localEulerAngles.y == 90 || Facility_obj.transform.localEulerAngles.y == 270)
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
}
