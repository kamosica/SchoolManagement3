using UnityEngine;
using System.Collections;
using UnityEngine.UI;//この宣言が必要

public class hiduke : MonoBehaviour
{
    private RectTransform hoge;
    float idou = 27.84f;
    float tuki = 173;
    int Sosita;
    int Hosita;
    int Oosita;
    public Button stopbttn;
    public float basetime;
    public Text nanbai;
    // Use this for initialization
    void Start()
    {
        var newblock = stopbttn.colors;
        Sosita = 0;
        Hosita = 0;
        Oosita = 0;
        basetime = 1;
        hoge = gameObject.GetComponent<RectTransform>();
        hoge.localPosition = new Vector3(0, tuki, 0);


    }

    // Update is called once per frame
    void Update()
    {

        hoge.localPosition = new Vector3(0, tuki += basetime * Time.deltaTime, 0);
        nanbai.text = basetime + "倍速"; 

        if (Input.GetKeyDown(KeyCode.A))
        {
            basetime = 0.6f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            basetime = 0f;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            basetime = 10f;
        }
    }
   public void Stop()
    {
        if (Sosita ==0)
        {
            basetime = 0f;
            Sosita++;

            var newblock = stopbttn.colors;
            newblock.normalColor = new Color32(200, 200, 200, 225);
            newblock.highlightedColor = new Color32(200, 200, 200, 225);
            stopbttn.colors = newblock;
        }
        else
        {
            basetime = 1f;
            Sosita=0;

            var newblock = stopbttn.colors;
            newblock.normalColor = new Color32(255, 255, 255, 255);
            newblock.highlightedColor = new Color32(255, 255, 255, 255);
            stopbttn.colors = newblock;
        }
        Hosita = 0;
        Oosita = 0;
    }
    public void Hayaku()
    {
        if (Hosita <= 2)
        {
            if (basetime == 0)
                basetime = 1;
            basetime += basetime;
            Hosita++;

            var newblock = stopbttn.colors;
            newblock.normalColor = new Color32(255, 255, 255, 255);
            newblock.highlightedColor = new Color32(255, 255, 255, 255);
            stopbttn.colors = newblock;
        }
        else
        {
            basetime = 1f;
            Hosita = 0;
        }
        Oosita = 0;
        Sosita = 0;
    }

    public void Osoku()
    {
        if (Oosita <= 2)
        {
            if (basetime == 0||basetime>1)
            {
                basetime = 1;
            }
            basetime -= 0.2f;
            Oosita++;

            var newblock = stopbttn.colors;
            newblock.normalColor = new Color32(255, 255, 255, 255);
            newblock.highlightedColor = new Color32(255, 255, 255, 255);
            stopbttn.colors = newblock;
        }
        else
        {
            basetime = 1f;
            Oosita = 0;
        }
        Sosita = 0;
        Hosita = 0;
    }
}
