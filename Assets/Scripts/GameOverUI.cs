using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    //public ScoreManager scoreManager;
    private TextMeshProUGUI scoreText;
    public GameObject rnakingImage;

    private void OnEnable()
    {
        // "Score Text (TMP)" 이름을 가진 게임 오브젝트를 찾아 TextMeshProUGUI 컴포넌트를 할당
        scoreText = GameObject.Find("Score Text (TMP)").GetComponent<TextMeshProUGUI>();
        //rankingImage = GameObject.Find("Ranking Image").GetComponent<Image>();
        UpdateScoreText();
    }


    public void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"SCORE  :  {ScoreManager.Instance.score}";
        }
        else
        {
            Debug.Log("스코어텍스트가 없습니다.");
        }
    }

    public void ReStart()
    {
        // StartCoroutine(ReStartCoRoutine());
        // 활성화중인 씬 열기
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator ReStartCoRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        // 활성화중인 씬 열기
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        SoundManager.Instance.PlayClickSound();

        // 유니티 에디터에서 실행 중인 경우
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        // 빌드 완료후 실행파일에서 실행 중인 경우
        Application.Quit();

        //StartCoroutine(QuitGameCoRoutine()); 
    }

    IEnumerator QuitGameCoRoutine()
    {
        SoundManager.Instance.PlayClickSound();

        yield return new WaitForSeconds(0.2f);

        // 유니티 에디터에서 실행 중인 경우
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드 완료후 실행파일에서 실행 중인 경우
        Application.Quit();
#endif
    }

    public void Ranking()
    {
        bool isRnak = rnakingImage.activeSelf;
        rnakingImage.SetActive(!isRnak);
    }
}
