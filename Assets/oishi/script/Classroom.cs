using UnityEngine;
using System.Collections;

public class Classroom : MonoBehaviour {

    public int Facility_Num;
    public Vector3 position = new Vector3(-100, -100, -100);
    public int RotateY;
    public Vector3 size;

    public int list_ID = -1;

    public bool isHit = false;

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
