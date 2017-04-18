using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {

    public GameObject TouchScreen;

    bool flg = false;

    float count = 0.0f;

    public GameObject BG1;
    public GameObject Logo1;

    public GameObject BG2;
    public GameObject Logo2;

	// Use this for initialization
	void Start () {
        Screen.SetResolution(720, 1280, Screen.fullScreen);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("Chess");
        }

        if (Input.GetKeyDown("up"))
        {
            BG1.SetActive(true);
            Logo1.SetActive(true);
            BG2.SetActive(false);
            Logo2.SetActive(false);
        }
        if (Input.GetKey("down"))
        {
            BG2.SetActive(true);
            Logo2.SetActive(true);
            BG1.SetActive(false);
            Logo1.SetActive(false);
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
