using UnityEngine;
using UnityEngine.UI;

public class menu5 : MonoBehaviour
{
    private GameObject ToolW5;

    void Start()
    {
        ToolW5 = GameObject.Find("ToolWindow5");
        ToolW5.GetComponent<Canvas>().enabled = false;
    }

    void OpenToolWindow5()
    {
        ToolW5.GetComponent<Canvas>().enabled = true;
    }

    void CloseToolWindow5()
    {
        ToolW5.GetComponent<Canvas>().enabled = false;
    }
}
