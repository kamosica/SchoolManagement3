using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {

    public GameObject TouchScreen;

    bool flg = false;

    float count = 0.0f;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("Chess");
        }


        if (flg == true)
        {
            Vector3 scale = TouchScreen.transform.localScale;
            scale = new Vector3(scale.x + 0.01f, scale.y + 0.01f, scale.z + 0.01f);
            TouchScreen.transform.localScale = scale;

            count += 0.01f;
        }
        else if(flg == false)
        {
            Vector3 scale = TouchScreen.transform.localScale;
            scale = new Vector3(scale.x - 0.01f, scale.y - 0.01f, scale.z - 0.01f);
            TouchScreen.transform.localScale = scale;

            count -= 0.01f;
        }

        if (count > 0.5f)
        {
            flg = false;
        }
        else if(count < 0.0f)
        {
            flg = true;
        }
	}
}
