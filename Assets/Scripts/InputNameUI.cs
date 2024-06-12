using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using RankingSytem;

public class InputNameUI : MonoBehaviour
{
    public RankingSystem rankingSystem;
    public GameObject gameOverUI;

    private TMP_InputField inputField;
    private int maxKoreanCharLimit = 5; // 원하는 한글 글자 수 제한

    private void Awake()
    {
        inputField = transform.Find("InputField (TMP)").GetComponent<TMP_InputField>();
        inputField.characterLimit = 10; // 충분히 큰 값으로 설정
        inputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    private void OnInputValueChanged(string text)
    {
        if (GetKoreanCharacterCount(text) > maxKoreanCharLimit)
        {
            inputField.text = RemoveExcessKoreanCharacters(text);
        }
    }

    private int GetKoreanCharacterCount(string text)
    {
        int koreanCharCount = 0;
        foreach (char c in text)
        {
            if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
            {
                koreanCharCount++;
            }
        }
        return koreanCharCount;
    }

    private string RemoveExcessKoreanCharacters(string text)
    {
        int koreanCharCount = 0;
        List<char> validChars = new List<char>();

        foreach (char c in text)
        {
            if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
            {
                if (koreanCharCount >= maxKoreanCharLimit)
                {
                    continue;
                }
                koreanCharCount++;
            }
            validChars.Add(c);
        }

        return new string(validChars.ToArray());
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
