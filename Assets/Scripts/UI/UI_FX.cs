using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Xml.Linq;

public class UI_FX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public Vector3 targetScale = Vector3.one;
    public float _time = 1f;
    public Ease ease = Ease.Linear;

    public void BTN_SFX(string sfxName)
    {
        MasterAudio.PlaySound(sfxName);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RectTransform rect = GetComponent<RectTransform>();
        MasterAudio.PlaySound("Button_Hover");
        rect.DOScale(targetScale, _time).SetEase(ease);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.DOScale(Vector3.one, _time).SetEase(ease);
    }
}
