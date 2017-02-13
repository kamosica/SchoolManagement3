using UnityEngine;
using System.Collections;
using System.Collections.Generic;//リストに必要

public class Seito : MonoBehaviour
{
    NavMeshAgent agent;
    //生徒の情報
    public bool kokugo, sannsu, rika;
    GameObject EVAobj;
    EVA eva;
    bool mokuteki;
    int mokutekiNA;
    public List<SenSei> senseiS = new List<SenSei>();
    void Start()
    {
        EVAobj = GameObject.Find("EVA");
        eva = EVAobj.GetComponent<EVA>();

        agent = GetComponent<NavMeshAgent>();
        mokuteki = false;

        //senseiS.Add(eva.sensei[0]);

        Debug.Log(eva.sensei[0].kokugo);

        //for (int i = 0; i < 3; i++)
        //{

        //    if (eva.sensei[i] == kokugo)
        //    {
        //        //    Debug.Log("OK");
        //        mokuteki = true;
        //        mokutekiNA = i;
        //    }
        //}
    }

    void Update()
    {
        if (mokuteki == true)
           agent.SetDestination(eva.SenSeiobj[mokutekiNA].transform.position);
    }
}
