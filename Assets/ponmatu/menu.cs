using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour {
    private GameObject ToolW;

    void Start()
    {
        ToolW = GameObject.Find("ToolWindow");
        ToolW.GetComponent<Canvas>().enabled = false;
    }

    void OpenToolWindow()
    {
        ToolW.GetComponent<Canvas>().enabled = true;
    }

    void CloseToolWindow()
    {
        ToolW.GetComponent<Canvas>().enabled = false;
    }
}
