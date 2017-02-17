using UnityEngine;
using UnityEngine.UI;

public class menu3 : MonoBehaviour
{
    private GameObject ToolW3;

    void Start()
    {
        ToolW3 = GameObject.Find("ToolWindow3");
        ToolW3.GetComponent<Canvas>().enabled = false;
    }

    void OpenToolWindow3()
    {
        ToolW3.GetComponent<Canvas>().enabled = true;
    }

    void CloseToolWindow3()
    {
        ToolW3.GetComponent<Canvas>().enabled = false;
    }
}
