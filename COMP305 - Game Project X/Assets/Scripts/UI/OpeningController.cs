using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpeningController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] TextMeshProUGUI continueText;
    string story;
    bool textDone;
    [SerializeField] SceneTransition sceneTransition;

    void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
        story = txt.text;
        txt.text = "";

        // TODO: add optional delay when to start
        StartCoroutine("PlayText");
    }

    IEnumerator PlayText()
    {
        yield return new WaitForSeconds(2f);
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        textDone = true;
        continueText.gameObject.SetActive(true);
    }
    private void Update()
    {
        if(textDone && Input.anyKey)
        {
            sceneTransition.PlayLevel(2);
        }
    }
}
