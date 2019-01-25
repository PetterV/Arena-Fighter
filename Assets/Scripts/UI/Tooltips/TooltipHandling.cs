using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipHandling : MonoBehaviour {

    public bool isOpen = true;
    //Margins for distances to the edge of the screen
    public float downwardsMargin = 20f;
    public float rightwardsMargin = 0f;
    public GameObject textBox;
    private List<GameObject> newTextBoxes = new List<GameObject>();

    void Start()
    {
        Close();
    }

    public void Open(List<string> messages)
    {
        gameObject.GetComponent<TooltipLocation>().SetLocation();
        CheckEdges();
        gameObject.GetComponent<TooltipLocation>().SetLocation();
        gameObject.SetActive(true);
        isOpen = true;
        foreach (string t in messages)
        {
            GameObject newText = Instantiate(textBox) as GameObject;
            newText.transform.SetParent(textBox.transform.parent);
            newText.transform.localScale = textBox.transform.localScale;
            newText.GetComponent<Text>().text = t;
            newText.SetActive(true);
            newTextBoxes.Add(newText);
        }
    }

    public void Close()
    {
        foreach (GameObject box in newTextBoxes)
        {
            Destroy(box);
        }
        gameObject.GetComponent<TooltipLocation>().invertedHeight = false;
        gameObject.GetComponent<TooltipLocation>().invertedWidth = false;
        gameObject.SetActive(false);
        isOpen = false;
        gameObject.GetComponent<TooltipLocation>().SetLocation();
    }

    void CheckEdges()
    {
        Vector3[] bottomCheckCorners = new Vector3[4];
        GameObject.Find("BottomCheck").GetComponent<RectTransform>().GetWorldCorners(bottomCheckCorners);
        float distanceToCompare = bottomCheckCorners[0].y;
        Vector3[] ttCorners = new Vector3[4];
        gameObject.GetComponent<RectTransform>().GetWorldCorners(ttCorners);

        float distanceCheck = ttCorners[3].y - downwardsMargin;
        if (distanceCheck < distanceToCompare)
        {
            gameObject.GetComponent<TooltipLocation>().invertedHeight = true;
        }

        float rightDistanceCompare = bottomCheckCorners[2].x;
        float rightDistanceCheck = ttCorners[3].x + rightwardsMargin;

        if (rightDistanceCheck > rightDistanceCompare)
        {
            gameObject.GetComponent<TooltipLocation>().invertedWidth = true;
        }
    }
}
