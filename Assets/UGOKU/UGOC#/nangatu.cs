using UnityEngine;
using System.Collections;
using UnityEngine.UI;//この宣言が必要

public class nangatu : MonoBehaviour {
    public Text TukiText;
    public Text GakkiText;
    public GameObject[] tuki;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        for(int i = 0; i < 12; i++)
        {
            if (tuki[i].name == collision.name)
            {
                if (i < 9)
                {
                    TukiText.text = "0" + (i + 1) + "月になりました。";
                }
                else
                    TukiText.text = (i + 1) + "月になりました。";

                if (8 > i && i > 2)
                {
                    GakkiText.text = "1学期";
                }
                else if (i > 7 && i < 13)
                {
                    GakkiText.text = "2学期";
                }
                else if (i < 4)
                {
                    GakkiText.text = "3学期";
                }
            }
        }
    }
    }
