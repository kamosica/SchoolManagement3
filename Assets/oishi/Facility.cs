//大石健太
using UnityEngine;
using System.Collections;

public class Facility : MonoBehaviour {

    public Vector3 position;
    public int RotateY;
    public Vector3 size;
    public int Facility_Num;

	// Use this for initialization
	void Start () {
        position = new Vector3(-100, -100, -100);
        RotateY = 0;
        size = transform.localScale;
        Facility_Num = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
