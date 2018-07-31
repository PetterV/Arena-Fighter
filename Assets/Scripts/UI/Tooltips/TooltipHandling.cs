using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipHandling : MonoBehaviour {

    public bool isOpen = true;
    public GameObject textBox;

    void Start()
    {
        Close();
    }

    public void Open() {
        gameObject.SetActive(true);
        isOpen = true;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        isOpen = false;
    }
}
