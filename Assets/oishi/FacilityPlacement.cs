//大石健太
//施設を配置するクラス
using UnityEngine;
using System.Collections;

public class FacilityPlacement : MonoBehaviour {

    int CurrentFacility_No = 0; //現在選択している施設の番号
    public GameObject Facility_obj; //現在選択している施設オブジェクト

    // 位置座標
    private Vector3 position;
    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //左ボタンを押した時の処理
        if (Input.GetMouseButton(0))
        {
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                Facility_obj = hit.collider.gameObject;
            }
            else
            {
                Facility_obj = null;
            }
        }

        if (Facility_obj != null && Facility_obj.tag == "Facility")
        {
            float pos_Fy = Facility_obj.transform.position.y; //施設のオブジェクトのY座標を取得
            position = Input.mousePosition;    // Vector3でマウス位置座標を取得する
            position.z = 10.0f;      // Z軸修正
            screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);     // マウス位置座標をスクリーン座標からワールド座標に変換する
            Facility_obj.transform.position = new Vector3(screenToWorldPointPosition.x, pos_Fy, screenToWorldPointPosition.z);    // ワールド座標に変換されたマウス座標を代入
        }

    }
}
