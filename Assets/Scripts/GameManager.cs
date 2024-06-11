using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RankingSytem;
using static RankingSytem.RankingSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;              
            }
            return instance;
        }
    }

    public GameObject quitPanel;      // 종료 안내 UI
    public GameObject gameOverUI;     // 게임오버 UI
    public GameObject inputNameUI;    // 이름입력 UI

    public bool isGameOver { get; private set; }

    private bool isFullRanking;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        SoundManager.Instance.PlayBgmSound();
    }

    private void OnEnable()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        // 게임오버 UI 비활성화
        if(gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        if(quitPanel != null)
        {
            //// 종료 UI 비활성화
            quitPanel.gameObject.SetActive(false);
        }
        

        Time.timeScale = 1.0f;

        // 강조한 후 정보 삭제
        PlayerPrefs.DeleteKey("latestScore");
        PlayerPrefs.DeleteKey("latestName");

        GetRankingListCount();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            ScoreManager.Instance.AddScore(100);
        }

        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            PlayerPrefs.DeleteKey("highscoreTable");
        }
    }

    // 랭킹 리스트 갯수 파악하기
    public bool GetRankingListCount()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        if (!string.IsNullOrEmpty(jsonString))
        {
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
            if (highscores.highscoreEntries.Count >= 5)
            {
                return isFullRanking = true;
            }
            else
            {
                return isFullRanking = false;
            }
        }
        return isFullRanking = false;
    }

    // 꼴등랭킹의 스코어 점수 가져오기
    public int GetLastRankingScore()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        if (!string.IsNullOrEmpty(jsonString))
        {
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
            if (highscores.highscoreEntries.Count > 0)
            {
                return highscores.highscoreEntries[highscores.highscoreEntries.Count - 1].score;
            }
        }
        return 0; // 랭킹 테이블이 비어있는 경우 0 반환
    }

    public void GameOver()
    {
        if (isGameOver) return; // 이미 게임오버 상태라면 실행하지 않음

        // 게임 오버 상태를 참으로 변경
        isGameOver = true;

        // 게임 일시중지
        Time.timeScale = 0f;

        if (isFullRanking)
        {
            if (ScoreManager.Instance.score > GetLastRankingScore())
            {
                inputNameUI.SetActive(true);
            }
            else
            {
                // 게임 오버 UI를 활성화
                gameOverUI.SetActive(true);
            }
        }
        else
        {
            inputNameUI.SetActive(true);
        }
    }
}
