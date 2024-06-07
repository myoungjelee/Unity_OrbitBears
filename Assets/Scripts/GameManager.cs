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
    public static GameManager instance;

    public GameObject quitPanel;      // 종료 안내 UI
    public GameObject gameOverUI;     // 게임오버 UI
    public GameObject inputNameUI;    // 이름입력 UI
    public Image nextPlanetImage;

    public bool isGameOver { get; private set; }
    private int maxPlanetID;          // 생성될 행성의 최대 범위

    private PlanetData currentPlanetData;
    private PlanetData nextPlanetData;

    public event System.Action<PlanetData> OnReload;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ResetGame();
    }

    private void Start()
    {
        maxPlanetID = 4;
    }

    public void OnClick_RetryButton()
    {
        // 활성화중인 씬 열기
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClick_QuitButton()
    {
        // 게임 일시중지
        Time.timeScale = 0f;

        // 종료 안내문 활성화
        quitPanel.gameObject.SetActive(true);
    }
    public void OnClick_ConfirmButton()
    {
        // 빌드 완료후 실행파일에서 실행 중인 경우
        Application.Quit();

        // 유니티 에디터에서 실행 중인 경우
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void OnClick_CancleButton()
    {
        // 게임 재개
        Time.timeScale = 1f;

        // 종료 안내문 비홠성화
        quitPanel.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        // 게임 오버 상태를 참으로 변경
        isGameOver = true;

        // 게임 일시중지
        Time.timeScale = 0f;

       

        if(ScoreManager.instance.score > Get10thScore())
        {
            inputNameUI.SetActive(true);
        }
        else
        {
            // 게임 오버 UI를 활성화
            gameOverUI.SetActive(true);
        }

    }

    public void ResetGame()
    {
        StartCoroutine(StartGame());  
    }

    IEnumerator StartGame()
    {
        // 게임오버 UI 비활성화
        gameOverUI.SetActive(false);

        // 종료 UI 비활성화
        quitPanel.gameObject.SetActive(false);

        // 게임 재개
        Time.timeScale = 1f;

        yield return new WaitForSeconds(3);

        //GameOver();
    }

    // 랜덤 행성 선택
    private PlanetData SelectRandomPlanet()
    {
        int id = UnityEngine.Random.Range(0, maxPlanetID + 1);

        //return PlanetManager.GetPlanetData((uint)id);

        return nextPlanetData;   // 지울예정
    }

    private void UpdatePlanetData()
    {
        currentPlanetData = nextPlanetData;
        nextPlanetData = SelectRandomPlanet();


        if (nextPlanetData.sprite != null)
        {
            nextPlanetImage.sprite = nextPlanetData.sprite;
        }
        else
        {
            nextPlanetImage.sprite = null;
        }

        // nextPlanetImage 사이즈 설정
        nextPlanetImage.rectTransform.sizeDelta = 50 * nextPlanetData.radius * Vector2.one;
    }

    // 행성 리로드
    public void ReloadPlanet()
    {
        UpdatePlanetData();
        OnReload?.Invoke(currentPlanetData);
    }

    // 10등의 스코어 점수 가져오기
    public int Get10thScore()
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
}
