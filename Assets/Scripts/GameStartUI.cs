using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartUI : MonoBehaviour
{
    private TextMeshProUGUI playButton;
    public GameObject PlayUI;

    public void OnEnable()
    {
        // "Score Text (TMP)" 이름을 가진 게임 오브젝트를 찾아 TextMeshProUGUI 컴포넌트를 할당
        playButton = GameObject.Find("Play Button").GetComponent<TextMeshProUGUI>();
        
    }

    public void PlayGame()
    {
        // 씬을 로드하여 게임을 시작합니다.
        SceneManager.LoadScene("Test_MJ");
    }


    public void Play()
    {
        PlayUI.SetActive(true);
    }

}
