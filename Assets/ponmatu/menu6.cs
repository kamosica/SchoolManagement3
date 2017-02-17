using UnityEngine;
using UnityEngine.UI;

public class menu6 : MonoBehaviour
{
    private GameObject ToolW6;

    void Start()
    {
        ToolW6 = GameObject.Find("ToolWindow6");
        ToolW6.GetComponent<Canvas>().enabled = false;
    }

    void OpenToolWindow6()
    {
        ToolW6.GetComponent<Canvas>().enabled = true;
    }

    void CloseToolWindow6()
    {
        ToolW6.GetComponent<Canvas>().enabled = false;
    }
}
