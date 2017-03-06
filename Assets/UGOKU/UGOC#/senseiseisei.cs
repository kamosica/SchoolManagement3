using UnityEngine;
using System.Collections;

public class senseiseisei : MonoBehaviour
{
    int count;
    // 先生の情報
    struct _SenSeiKOUZou
    {
        public int sex;//性別 0=男　1=女
        public int kamoku;//科目
        public string Name;//名前
        public int nenrei;// 年齢
        public int nouryoku;//能力
        public int kyuuryou;//給料
        public int seikaku;//性格
    };
    _SenSeiKOUZou[] skt;
    void Start()
    {
        skt = new _SenSeiKOUZou[1000];

        count = 20;
        
        // 再現性を持たせる為、種を固定しています  
        //System.Random r = new System.Random(1000);
        //' ランダムにする場合  
        System.Random r = new System.Random();

        string[] retNames = new string[count + 1];
        string[] fName1 = {
        "山",
        "川",
        "谷",
        "田",
        "小",
        "石",
        "水",
        "大",
        "橋",
        "野",
        "池",
        "吉",
        "一本",
        "中"
        
    };
        string[] fName2 = {
        "田",
        "本",
        "川",
        "口",
        "野",
        "村",
        "崎",
        "山",
        "島",
        "上",
        "浦",
        "内",
        "松",
        "原"
    };

        string[] lName1 = {
        "順",
        "優",
        "恵",
        "浩",
        "裕",
        "正",
        "昭",
        "真",
        "純",
        "清",
        "博",
        "孝",
        "幸"
    };
        string[] lNameM2 = {
        "",
        "一",
        "二",
        "一郎",
        "義",
        "夫",
        "雄",
        "太郎",
        "彦"
    };
        string[] lNameW2 = {
        "",
        "子",
        "美",
        "実",
        "",
    };

        for (int i = 0; i <= retNames.Length - 1; i++)
        {
            skt[i].sex = r.Next(2);

            skt[i].kamoku = r.Next(6);

            int f1 = r.Next(0, fName1.Length - 1);
            int f2 = r.Next(0, fName2.Length - 1);

            if (skt[i].sex == 0)
            {
                int l1 = r.Next(0, lName1.Length - 1);
                int l2 = r.Next(0, lNameM2.Length - 1);
                skt[i].Name = string.Format("{0}{1}　{2}{3}", fName1[f1], fName2[f2], lName1[l1], lNameM2[l2]);
            }
            else
            {
                int l1 = r.Next(0, lName1.Length - 1);
                int l2 = r.Next(0, lNameW2.Length - 1);
                skt[i].Name = string.Format("{0}{1}　{2}{3}", fName1[f1], fName2[f2], lName1[l1], lNameW2[l2]);
            }
            skt[i].nenrei = r.Next(23, 75);
            skt[i].nouryoku = r.Next(0, 11);
            skt[i].kyuuryou = ((1 + skt[i].nouryoku) * (1 + skt[i].nenrei) + r.Next(10)) * 1000;
            skt[i].seikaku = r.Next(5);
            Debug.Log("性別 " + skt[i].sex + " 名前 " + skt[i].Name + " 科目" + skt[i].kamoku + 
                " 年齢" + skt[i].nenrei + " ステータス " + skt[i].nouryoku + " 性格 " + skt[i].seikaku + " 給料" + skt[i].kyuuryou);
        }
    }
    void Update()
    {

    }
}
