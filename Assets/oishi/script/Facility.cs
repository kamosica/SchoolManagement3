//大石健太
using UnityEngine;
using System.Collections;

public class Facility : MonoBehaviour {

    public int Facility_Num;
    public Vector3 position = new Vector3(-100,-100,-100);
    public int RotateY;
    public Vector3 size;

    public int list_ID = -1;

	// Use this for initialization
	void Start () {
        //position = new Vector3(-100, -100, -100);
        //RotateY = 0;
        size = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
