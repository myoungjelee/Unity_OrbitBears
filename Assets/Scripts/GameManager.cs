using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RankingSytem;
using static RankingSytem.RankingSystem;
using System.IO;

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

    public string filePath;
    string buildPath = Directory.GetParent(Application.dataPath).FullName;

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

        // 랭킹파일 저장 경로
        #if UNITY_EDITOR
        filePath = Path.Combine(Application.dataPath + "/Editor", "Ranking.json");
        #else
        filePath = Path.Combine(buildPath + "/Rank Data", "Ranking.json");
        #endif

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

        //// 강조한 후 정보 삭제
        //PlayerPrefs.DeleteKey("latestScore");
        //PlayerPrefs.DeleteKey("latestName");   
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

            // 최근 점수 및 이름 초기화
            highscores.latestScore = 0;
            highscores.latestName = string.Empty;

            // 업데이트된 JSON 파일 저장
            string updatedJson = JsonUtility.ToJson(highscores);
            File.WriteAllText(filePath, updatedJson);
        }

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
        //string jsonString = PlayerPrefs.GetString("highscoreTable");
        //if (!string.IsNullOrEmpty(jsonString))
        //{
        //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        //    if (highscores.highscoreEntries.Count >= 5)
        //    {
        //        return isFullRanking = true;
        //    }
        //    else
        //    {
        //        return isFullRanking = false;
        //    }
        //}
        //return isFullRanking = false;

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(jsonString))
            {
                Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
                return highscores.highscoreEntries.Count >= 5;
            }
        }
        return false;
    }

    // 꼴등랭킹의 스코어 점수 가져오기
    public int GetLastRankingScore()
    {
        //string jsonString = PlayerPrefs.GetString("highscoreTable");
        //if (!string.IsNullOrEmpty(jsonString))
        //{
        //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        //    if (highscores.highscoreEntries.Count > 0)
        //    {
        //        return highscores.highscoreEntries[highscores.highscoreEntries.Count - 1].score;
        //    }
        //}
        //return 0; // 랭킹 테이블이 비어있는 경우 0 반환

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(jsonString))
            {
                Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
                if (highscores.highscoreEntries.Count > 0)
                {
                    return highscores.highscoreEntries[highscores.highscoreEntries.Count - 1].score;
                }
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
