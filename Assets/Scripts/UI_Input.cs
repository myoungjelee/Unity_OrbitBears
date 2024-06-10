using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using RankingSytem;

public class UI_Input : MonoBehaviour
{
    public RankingSystem rankingSystem;
    public GameObject rankingUI;
    public GameObject gameOverUI;

    private TMP_InputField inputField;

    private void Awake()
    {
        inputField = transform.Find("InputField (TMP)").GetComponent<TMP_InputField>();
    }

    public void OnClick_CheckButton()
    {
        rankingSystem.AddHighscoreEntry(45, inputField.text);
        rankingUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnClick_XButton()
    {
        gameObject.SetActive(false);
        gameOverUI.SetActive(true);
    }
}
