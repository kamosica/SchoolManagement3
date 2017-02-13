using UnityEngine;
using System.Collections;
using System.Collections.Generic;//リストに必要

public class Seito : MonoBehaviour
{
    NavMeshAgent agent;   
    public bool kokugo, sannsu, rika;   //生徒の情報
    GameObject EVAobj;  //マネージャーの読み込み
    EVA eva;
    bool mokuteki;    //目的フラグ
    int mokutekiNA;  //目的のオブジェクトの番号

    void Start()
    {
        EVAobj = GameObject.Find("EVA");//マネージャーの読み込み
        eva = EVAobj.GetComponent<EVA>();

        agent = GetComponent<NavMeshAgent>();
        mokuteki = false;

    }

    void Update()
    {
        for (int i = 0; i < eva.SenSeiobj.Length; i++)
        {
            if (    (eva.sensei[i].kokugo == kokugo && kokugo == true)
                ||  (eva.sensei[i].sansu  == sannsu && sannsu == true)
                ||  (eva.sensei[i].rika   == rika   && rika   == true))
            {
                mokuteki = true;
                mokutekiNA = i;
            }
        }

        if (mokuteki == true)
        {
            Color senseiIRO = eva.SenSeiobj[mokutekiNA].GetComponent<Renderer>().material.color;
            gameObject.GetComponent<Renderer>().material.color = senseiIRO;
            agent.SetDestination(eva.SenSeiobj[mokutekiNA].transform.position);
        }
    }
}
