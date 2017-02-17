using UnityEngine;
using UnityEngine.UI;

public class menu1 : MonoBehaviour
{
    private GameObject ToolW1;

    void Start()
    {
        ToolW1 = GameObject.Find("ToolWindow1");
        ToolW1.GetComponent<Canvas>().enabled = false;
    }

    void OpenToolWindow1()
    {
        ToolW1.GetComponent<Canvas>().enabled = true;
    }

    void CloseToolWindow1()
    {
        ToolW1.GetComponent<Canvas>().enabled = false;
    }
}
