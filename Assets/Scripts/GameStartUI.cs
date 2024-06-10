using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartUI : MonoBehaviour
{ 
    public void PlayGame()
    {
        // 씬을 로드하여 게임을 시작합니다.
        SceneManager.LoadScene("Solar_System");
    }
}
