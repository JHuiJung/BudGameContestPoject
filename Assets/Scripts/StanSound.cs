using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkTonic.MasterAudio;
using TMPro;

public class StanSound : MonoBehaviour
{
    public float SoundLength = 10f;
    public string sfxName = "";

    [TextArea]
    public string content = "";

    public GameObject SubTitleObj;
    public TMP_Text txt_Stan;

    [SerializeField]
    bool isCheck = false;

    [SerializeField]
    bool isStartEnable = false;

    private void OnTriggerEnter(Collider other)
    {
        print("2");
        if (other.tag == "Player" && !isCheck)
        {
            isCheck = true;


            StartCoroutine(play());
        }
        
    }

    private void OnEnable()
    {
        if (!isStartEnable)
            return;
        PlayStan();
    }

    public void PlayStan()
    {
        StartCoroutine(play());
    }


    IEnumerator play()
    {

        print("1");

        StanSound[] sounds = FindObjectsOfType<StanSound>();

        foreach (StanSound sound in sounds)
        {
            sound.Stop();
        }

        txt_Stan.text = content;

        SubTitleObj.SetActive(true);
        MasterAudio.PlaySound(sfxName);

        yield return new WaitForSeconds(SoundLength);
        SubTitleObj.SetActive(false);

    }

    public void ResetSound()
    {
        isCheck = false;
    }

    public void Stop()
    {
        MasterAudio.StopBus("Stan");
        SubTitleObj.SetActive(false);
    }
}
