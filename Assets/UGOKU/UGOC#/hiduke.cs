using UnityEngine;
using System.Collections;

public class hiduke : MonoBehaviour
{
    private RectTransform hoge;
    float idou = 27.84f;
    float tuki = 173;
    float time;
    float speedlol;
    // Use this for initialization
    void Start()
    {
        speedlol = 0.5f;
        hoge = gameObject.GetComponent<RectTransform>();
        hoge.localPosition = new Vector3(0, tuki, 0);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        hoge.localPosition = new Vector3(0, tuki+time*speedlol, 0);

        if (Input.GetKeyDown(KeyCode.A))
        {
            speedlol += 5;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            speedlol -= 5;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            tuki += idou;
            hoge.localPosition = new Vector3(0, tuki, 0);
        }
    }
}
