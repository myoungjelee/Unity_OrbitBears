using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TextMeshPro scoreText;
    public TextMeshPro BestText;
    public bool isGameOver = false;
    public GameObject gameOver;
    public GameObject rankingUI;


    // Update is called once per frame
    public void UpdateScoreText()
    {
        scoreText.text = $"SCORE : {scoreManager.score}";
    }

    public void UpdateBestText()
    {
        BestText.text = $"BEST : {scoreManager.score}";

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
