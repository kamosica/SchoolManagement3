using UnityEngine;
using System.Collections;

public class iTweenTest : MonoBehaviour {

    public GameObject Coma;

	// Use this for initialization
	void Start () {
        MoveComa();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void MoveComa()
    {
        iTween.MoveTo(Coma, new Vector3(3.5f,0.5f,6.3f), 3);
    }
}
