using UnityEngine;
using UnityEngine.UI;

public class menu4 : MonoBehaviour
{
    private GameObject ToolW4;

    void Start()
    {
        ToolW4 = GameObject.Find("ToolWindow4");
        ToolW4.GetComponent<Canvas>().enabled = false;
    }

    void OpenToolWindow4()
    {
        ToolW4.GetComponent<Canvas>().enabled = true;
    }

    void CloseToolWindow4()
    {
        ToolW4.GetComponent<Canvas>().enabled = false;
    }
}
