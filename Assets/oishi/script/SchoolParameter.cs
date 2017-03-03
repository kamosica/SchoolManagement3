//大石健太
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SchoolParameter : MonoBehaviour {

    //時間で変動するパラメータ
    public float s_gakuryoku_T = 0.0f;   //学力
    public float s_undou_T = 0.0f;       //運動
    public float s_bunka_T = 0.0f;       //文化

    public float m_sutoresu_T = 0.0f;    //ストレス
    public float m_hirou_T = 0.0f;     //疲労

    public float f_tian_T = 0.0f;        //治安
    public float f_seiketudo_T = 0.0f;   //清潔度

    //施設を置くと変動するパラメータ
    public float s_gakuryoku = 0.0f;   //学力
    public float s_undou = 0.0f;       //運動
    public float s_bunka = 0.0f;       //文化

    public float m_sutoresu = 0.0f;    //ストレス
    public float m_hirou = 0.0f;       //疲労

    public float f_tian = 0.0f;        //治安
    public float f_seiketudo = 0.0f;   //清潔度


    public GameObject ParameterText;    //パラメータの表示
    Text parametertext;

    // Use this for initialization
    void Start () {
        parametertext = ParameterText.GetComponent<Text>();
	
	}
	
	// Update is called once per frame
	void Update () {

        parametertext.text =    "学力     : " + s_gakuryoku_T + " : " + s_gakuryoku + "\n" +
                                "運動     : " + s_undou_T     + " : " + s_undou     + "\n" +
                                "文化     : " + s_bunka_T     + " : " + s_bunka     + "\n" +
                                "ストレス : " + m_sutoresu_T  + " : " + m_sutoresu  + "\n" +
                                "疲労     : " + m_hirou_T     + " : " + m_hirou     + "\n" +
                                "治安     : " + f_tian_T      + " : " + f_tian      + "\n" +
                                "清潔度   : " + f_seiketudo_T + " : " + f_seiketudo ;
    }

    //パラメーターの変更 obj:施設か建物のオブジェクト i:プラスかマイナスか
    public void Parameter_Change(GameObject obj,int i)
    {
        if(obj.tag == "Facility")
        {
            Facility facility = obj.GetComponent<Facility>();
            s_gakuryoku += facility.s_gakuryoku * i;
            s_undou     += facility.s_undou * i;
            s_bunka     += facility.s_bunka * i;
            m_sutoresu  += facility.m_sutoresu * i;
            m_hirou     += facility.m_hirou * i;
            f_tian      += facility.f_tian * i;
            f_seiketudo += facility.f_seiketudo * i;
        }
        else if (obj.tag == "Classroom")
        {
            Classroom classroom = obj.GetComponent<Classroom>();
            s_gakuryoku += classroom.s_gakuryoku * i;
            s_undou += classroom.s_undou * i;
            s_bunka += classroom.s_bunka * i;
            m_sutoresu += classroom.m_sutoresu * i;
            m_hirou += classroom.m_hirou * i;
            f_tian += classroom.f_tian * i;
            f_seiketudo += classroom.f_seiketudo * i;
        }
    }
}
