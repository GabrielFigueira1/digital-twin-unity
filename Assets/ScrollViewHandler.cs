using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScrollViewHandler : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Text textPrefab;
    public Transform content;
    public Scrollbar scrollbar;
    // Start is called before the first frame update
    void Start()
    {
        textPrefab.text = "Logs";
        Text newText = Instantiate<Text>(textPrefab);
        newText.rectTransform.sizeDelta = new Vector2(420f, 20f);
        newText.transform.SetParent(content.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Log(string message)
    {
        bool scrollbarAuto = scrollbar.value <= 0 ? true : false;

        textPrefab.text = message;
        Text newText = Instantiate<Text>(textPrefab);
        newText.transform.SetParent(content.transform);

        StartCoroutine(SetScrollBarToZero(scrollbarAuto));
    }

    IEnumerator SetScrollBarToZero(bool scrollbarAuto)
    {
        yield return new WaitForSeconds(0.02f);
        scrollbar.value = scrollbarAuto ? 0 : scrollbar.value;
    }
}