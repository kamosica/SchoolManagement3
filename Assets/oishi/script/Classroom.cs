using UnityEngine;
using System.Collections;

public class Classroom : MonoBehaviour {

    public int Facility_Num;
    public Vector3 position = new Vector3(-100, -100, -100);
    public int RotateY;
    public Vector3 size;

    public int list_ID = -1;
    public bool isHit = false;

    public float s_gakuryoku = 2.0f;   //学力
    public float s_undou = 1.0f;   //運動
    public float s_bunka = 3.0f;   //文化

    public float m_sutoresu = 5.0f;   //ストレス
    public float m_hirou = 2.0f;   //疲労

    public float f_tian = 12.0f;   //治安
    public float f_seiketudo = 1.0f;   //清潔度

    // Use this for initialization
    void Start () {
        size = transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //オブジェクトが衝突した時
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RoomTile" || other.tag == "MapTile") return;
        isHit = true;
        Debug.Log("Classroom Enter");
    }
    //オブジェクトが離れたとき
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "RoomTile" || other.tag == "MapTile") return;
        isHit = false;
        Debug.Log("Classroom Exit");
    }
}
