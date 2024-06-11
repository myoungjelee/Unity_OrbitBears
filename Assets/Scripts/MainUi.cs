using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public GameObject quitPanel;      // 종료 안내 UI
    public void OnClick_RetryButton()
    {
        SoundManager.Instance.PlayClickSound();

        StartCoroutine(RetryCoRoutine());
    }

    IEnumerator RetryCoRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        // 활성화중인 씬 열기
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClick_QuitButton()
    {
        SoundManager.Instance.PlayClickSound();

        StartCoroutine(QuitCoRoutine());
    }

    IEnumerator QuitCoRoutine()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        // 종료 안내문 활성화
        quitPanel.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }

    public void OnClick_ConfirmButton()
    {
        SoundManager.Instance.PlayClickSound();

        StartCoroutine(ConfirmCoRoutine());
    }

    IEnumerator ConfirmCoRoutine()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        // 유니티 에디터에서 실행 중인 경우
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드 완료후 실행파일에서 실행 중인 경우
        Application.Quit();
#endif
    }

    public void OnClick_CancleButton()
    {
        SoundManager.Instance.PlayClickSound();

        StartCoroutine(CancleCoRoutine());
    }

    IEnumerator CancleCoRoutine()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        Time.timeScale = 1.0f;
        // 종료 안내문 비홠성화
        quitPanel.gameObject.SetActive(false);
    }

}
