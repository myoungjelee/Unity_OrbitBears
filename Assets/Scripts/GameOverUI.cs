using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    //public ScoreManager scoreManager;
    private TextMeshProUGUI scoreText;
    public TextMeshPro BestText;
    public bool isGameOver = false;
    public GameObject gameOver;
    public GameObject rankingUI;

    private void OnEnable()
    {
        // "Score Text (TMP)" 이름을 가진 게임 오브젝트를 찾아 TextMeshProUGUI 컴포넌트를 할당
        scoreText = GameObject.Find("Score Text (TMP)").GetComponent<TextMeshProUGUI>();
        UpdateScoreText();
    }


    public void UpdateScoreText()
    {

    }

   

    public void ReStart()
    {
        //gameObject.SetActive(false);
        // 현재 실행 중인 씬을 다시 불러오는 코드.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OUT()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void Ranking()
    {
        gameObject.SetActive(false); //게임종료 UI 끄기
        rankingUI.SetActive(true);   //랭킹 UI  켜기
    }
}
