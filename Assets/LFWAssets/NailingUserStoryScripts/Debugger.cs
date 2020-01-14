using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Debugger : MonoBehaviour
{
    public GameObject debugTextUI;
    public static string debugText = "Nothing.";

    // Update is called once per frame
    void Update()
    {
        debugTextUI.GetComponent<Text>().text = debugText;
    }
}
