//大石健太
using UnityEngine;
using System.Collections;

public class ButtonFacility : MonoBehaviour {

    // 位置座標
    private Vector3 position;
    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;

    public GameObject Facility_obj; //施設のオブジェクト

    public GameObject Map_obj;
    Map Map_scr;

    void Start () {
        Map_scr = Map_obj.GetComponent<Map>();
    }

	void Update () {
	
	}

    //施設を新しく配置する
    public void Create_Facility(){

        // Vector3でマウス位置座標を取得する
        position = Input.mousePosition;
        // Z軸修正
        position.z = 10f;
        // マウス位置座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
        GameObject obj = (GameObject)Instantiate(Facility_obj, screenToWorldPointPosition, transform.rotation);

        if (obj.tag == "Facility")
        {
            Facility facility = obj.GetComponent<Facility>();
            facility.list_ID = Map_scr.facilityID;
            Map_scr.facilityID++;
        }
        else if (obj.tag == "Classroom")
        {
            Classroom classroom = obj.GetComponent<Classroom>();
            classroom.list_ID = Map_scr.classroomID;
            Map_scr.classroomID++;
        }
    }
}
