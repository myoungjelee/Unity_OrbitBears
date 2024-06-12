using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using RankingSytem;
using UnityEngine.UI;


public class InputNameUI : MonoBehaviour
{
    public RankingSystem rankingSystem;
    public GameObject gameOverUI;
    private Image backGround;

    private TMP_InputField inputField;
    private int maxKoreanCharLimit = 5; // 원하는 한글 글자 수 제한

    private void Awake()
    {
        inputField = transform.Find("InputField (TMP)").GetComponent<TMP_InputField>();
        backGround = transform.Find("BackGround").GetComponent<Image>();
        inputField.characterLimit = 10; // 영어 글자수 입력제한
        inputField.onValueChanged.AddListener(OnInputValueChanged); // 입력값이 변경될 때 호출될 메서드
    }

    // 입력값이 변경될 때 호출되는 메서드
    private void OnInputValueChanged(string text)
    {
        if (GetKoreanCharacterCount(text) > maxKoreanCharLimit)
        {
            inputField.text = RemoveExcessKoreanCharacters(text);
        }
    }

    // 문자열에서 한글 글자 수를 세는 메서드
    private int GetKoreanCharacterCount(string text)
    {
        int koreanCharCount = 0;
        foreach (char c in text)
        {
            // 문자가 한글인지 확인
            if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
            {
                koreanCharCount++;
            }
        }
        return koreanCharCount;
    }

    // 초과된 한글 글자를 제거하는 메서드
    private string RemoveExcessKoreanCharacters(string text)
    {
        int koreanCharCount = 0;
        List<char> validChars = new List<char>();

        foreach (char c in text)
        {
            // 문자가 한글인지 확인
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
        if(!string.IsNullOrEmpty(inputField.text))
        {
            rankingSystem.AddHighscoreEntry(ScoreManager.Instance.score, inputField.text);
            gameObject.SetActive(false);
            gameOverUI.SetActive(true);
        }
        else
        {
            StartCoroutine(ChangeColor());
        }
    }

    IEnumerator ChangeColor()
    {
        backGround.color = HexColor("#B3120F");

        yield return new WaitForSecondsRealtime(0.1f);

        backGround.color = Color.white;

        yield return new WaitForSecondsRealtime(0.1f);

        backGround.color = HexColor("#B3120F");

        yield return new WaitForSecondsRealtime(0.1f);

        backGround.color = Color.white;
    }

    public void OnClick_XButton()
    {
        gameObject.SetActive(false);
        gameOverUI.SetActive(true);
    }

    // 헥사값 컬러 반환( 코드 순서 : RGBA )
    public static Color HexColor(string hexCode)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hexCode, out color))
        {
            return color;
        }

        Debug.LogError("[UnityExtension::HexColor]invalid hex code - " + hexCode);
        return Color.white;
    }
}
