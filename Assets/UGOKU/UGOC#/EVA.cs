using UnityEngine;
using System.Collections;
using System.Collections.Generic;//リストに必要

public class EVA : MonoBehaviour
{

    // マネージャークラス

    //先生
    int Sninzu;
    public GameObject[] SenSeiobj;
    public List<SenSei> sensei = new List<SenSei>();//リストの生成
    
    void Start()
    {
        SenSeiobj = GameObject.FindGameObjectsWithTag("sensei");//先生を配列に入れる
        Sninzu = SenSeiobj.Length;//先生の数を調べる

        for (int i = 0; i < Sninzu; i++)
        {
            sensei.Add(SenSeiobj[i].GetComponent<SenSei>());//リストの格納
        }

        

    }
    void Update()
    {

    }
}
