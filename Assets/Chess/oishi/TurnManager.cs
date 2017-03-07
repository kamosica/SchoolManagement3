using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnManager: MonoBehaviour {

    public GameObject Turn_Text;
    public GameObject Turn2_Text;

    Text turn_t;
    Text turn2_t;

    bool isfirst = true;   //true 先行　false 後攻
    int turn_count = 0;

    public GameObject EndPanel;
    public GameObject EndText;

	// Use this for initialization
	void Start () {
        turn_t = Turn_Text.GetComponent<Text>();
        turn2_t = Turn2_Text.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //テキストの変更
    public void Change_Text()
    {
        if (turn_count%2 == 0)
        {
            turn_count++;
            turn_t.text = "後攻";

            turn2_t.text = turn_count / 2 + 1 + "ターン";
        }
        else if (turn_count % 2 == 1)
        {
            turn_count++;
            turn_t.text = "先攻";

            turn2_t.text = "";

            turn2_t.text = turn_count / 2 + 1 + "ターン";
        }

        Change_Color();
    }

    //ターンが変わったら自分のコマのアルファ値を変える
    void Change_Color()
    {
        GameObject[] tagobjs = GameObject.FindGameObjectsWithTag("Coma");

        for(int i = 0;i < tagobjs.Length;i++)
        {
            Coma coma = tagobjs[i].GetComponent<Coma>();
            
            if(coma.PalyerNo - 1 == turn_count % 2)
            {
                Color coma_color = tagobjs[i].GetComponent<Renderer>().material.color;
                coma_color = new Color(coma_color.r, coma_color.g, coma_color.b, 1.0f);
                tagobjs[i].GetComponent<Renderer>().material.color = coma_color;
            }
            else
            {
                Color coma_color = tagobjs[i].GetComponent<Renderer>().material.color;
                coma_color = new Color(coma_color.r, coma_color.g, coma_color.b, 0.7f);
                tagobjs[i].GetComponent<Renderer>().material.color = coma_color;
            }
        }
    }

    //勝利文字の表示
    public void Create_EndPanel(int PlayerNo)
    {
        EndPanel.SetActive(true);

        Text endtext = EndText.GetComponent<Text>();

        if (PlayerNo == 1) endtext.text = "先攻の勝利";
        else if (PlayerNo == 2) endtext.text = "後攻の勝利";
    }

    public void Push_ContinueButton()
    {
        SceneManager.LoadScene("Chess");
    }
}
