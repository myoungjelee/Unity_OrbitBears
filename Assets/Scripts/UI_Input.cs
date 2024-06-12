using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using RankingSytem;

public class UI_Input : MonoBehaviour
{
    public RankingSystem rankingSystem;
    public GameObject gameOverUI;

    private TMP_InputField inputField;
    

    private void Awake()
    {
        inputField = transform.Find("InputField (TMP)").GetComponent<TMP_InputField>();
        inputField.characterLimit = 10;
    }

    public void OnClick_CheckButton()
    {
        rankingSystem.AddHighscoreEntry(ScoreManager.Instance.score, inputField.text);
        gameObject.SetActive(false);
        gameOverUI.SetActive(true);
    }

    public void OnClick_XButton()
    {
        gameObject.SetActive(false);
        gameOverUI.SetActive(true);
    }
}
