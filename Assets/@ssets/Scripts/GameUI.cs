using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    [SerializeField] private TMP_Text textInfo;
    [SerializeField] private RectTransform gameOverRect;

    public TMP_Text TextInfo
    {
        get
        {
            return textInfo;
        }
    }

    private void OnEnable()
    {
        Instance = this;
    }

    private void Start()
    {
        SetTextInfo("Press Any Key To Start", 60);
        ActivateGameOver(false);
    }

    public void SetTextInfo(string text, float fontSize = -1)
    {
        if (fontSize != -1)
        {
            textInfo.fontSize = fontSize;
        }

        textInfo.text = text;
    }

    public void ActivateGameOver(bool isActive)
    {
        gameOverRect.gameObject.SetActive(isActive);
    }
}
