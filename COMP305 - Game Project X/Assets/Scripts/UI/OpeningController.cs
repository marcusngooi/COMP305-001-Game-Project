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
    bool textDone, skipText;
    [SerializeField] SceneTransition sceneTransition;
    public AudioSource audioSource;
    public AudioClip typeAudio;

    void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
        story = txt.text;
        txt.text = "";

        // TODO: add optional delay when to start
        StartCoroutine(PlayText());
        StartCoroutine(PlayTypeSound());
    }

    IEnumerator PlayText()
    {
        yield return new WaitForSeconds(2f);
        audioSource.Play();
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.02f);
            if (skipText == true)
            {
                txt.text = story;
                break;
            }
        }
        audioSource.Stop();
        textDone = true;
        continueText.gameObject.SetActive(true);
    }
    IEnumerator PlayTypeSound()
    {
        while(textDone == false)
        {
            
            yield return new WaitForSeconds(0.12f);
        }
        yield return null;
    }
    private void Update()
    {
        if(textDone && Input.anyKeyDown)
        {
            sceneTransition.PlayLevel("Level 1");
        }
        if(textDone == false && Input.anyKeyDown && skipText == false)
        {
            skipText = true;
        }
    }
}
