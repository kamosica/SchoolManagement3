using UnityEngine;
using UnityEngine.UI;

public class menu2 : MonoBehaviour
{
    private GameObject ToolW2;

    void Start()
    {
        ToolW2 = GameObject.Find("ToolWindow2");
        ToolW2.GetComponent<Canvas>().enabled = false;
    }

    void OpenToolWindow2()
    {
        ToolW2.GetComponent<Canvas>().enabled = true;
    }

    void CloseToolWindow2()
    {
        ToolW2.GetComponent<Canvas>().enabled = false;
    }
}
