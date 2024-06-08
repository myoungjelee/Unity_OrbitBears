using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

namespace RankingSytem
{
    public class RankingSystem : MonoBehaviour
    {
        private Transform entryContainer;
        private Transform entryTemplate;
        private List<Transform> highscoreEntryTransforms;

        private const int MAX_ENTRY = 10;

        private void Awake()
        {
            //AddHighscoreEntry(432, "AAA");
            //AddHighscoreEntry(1200, "BBB");
            //AddHighscoreEntry(78, "CCC");
            //AddHighscoreEntry(347, "DDD");
            //AddHighscoreEntry(908, "EEE");
            //AddHighscoreEntry(1642, "FFF");
            //AddHighscoreEntry(1592, "GGG");
            //AddHighscoreEntry(1989, "HHH");

            entryContainer = transform.Find("ScoreEntryContainer");
            entryTemplate = entryContainer.Find("ScoreEntryTemplate");

            entryTemplate.gameObject.SetActive(false);

            string jsonString = PlayerPrefs.GetString("highscoreTable");
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

            if (PlayerPrefs.HasKey("highscoreTable"))
            {
                // 스코어 내림차순 정렬
                for (int i = 0; i < highscores.highscoreEntries.Count; i++)
                {
                    for (int j = i; j < highscores.highscoreEntries.Count; j++)
                    {
                        if (highscores.highscoreEntries[j].score > highscores.highscoreEntries[i].score)
                        {
                            HighscoreEntry temp = highscores.highscoreEntries[i];
                            highscores.highscoreEntries[i] = highscores.highscoreEntries[j];
                            highscores.highscoreEntries[j] = temp;
                        }
                    }
                }

                highscoreEntryTransforms = new List<Transform>();
                foreach (HighscoreEntry entry in highscores.highscoreEntries)
                {
                    CreateHighscoreEntryTransform(entry, entryContainer, highscoreEntryTransforms);
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightAlt))
            {
                PlayerPrefs.DeleteKey("highscoreTable");
            }
        }

        private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transforms)
        {
            float templateHeight = 50f;

            Transform entryTransform = Instantiate(entryTemplate, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transforms.Count);
            entryTransform.gameObject.SetActive(true);

            // 순위 및 트로피색 설정
            int rank = transforms.Count + 1;
            string rankString;
            switch (rank)
            {
                default:
                    rankString = $"{rank}TH";
                    entryTransform.Find("Trophy").gameObject.SetActive(false);
                    break;
                case 1:
                    rankString = "1ST";
                    entryTransform.Find("Trophy").GetComponent<Image>().color = new Color(213 / 255f, 161 / 255f, 30 / 255f, 255 / 255f);
                    break;
                case 2:
                    rankString = "2ND";
                    entryTransform.Find("Trophy").GetComponent<Image>().color = HexColor("#A3A3A3");
                    break;
                case 3:
                    rankString = "3RD";
                    entryTransform.Find("Trophy").GetComponent<Image>().color = HexColor("#CD7F32");
                    break;
            }
            entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>().text = rankString;

            int score = highscoreEntry.score;
            entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

            string name = highscoreEntry.name;
            entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;

            // 1등 색상 강조하기
            entryTransform.Find("BackGround").gameObject.SetActive(rank % 2 == 1);
            if (rank == 1)
            {
                entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>().color = Color.green;
                entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().color = Color.green;
                entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().color = Color.green;
            }

            // 최신 항목 색상 강조하기
            int latestScore = PlayerPrefs.GetInt("latestScore");
            string latestName = PlayerPrefs.GetString("latestName");

            if (score == latestScore && name == latestName)
            {
                entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>().color = Color.yellow;
                entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().color = Color.yellow;
                entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().color = Color.yellow;

                // 강조한 후 정보 삭제
                PlayerPrefs.DeleteKey("latestScore");
                PlayerPrefs.DeleteKey("latestName");
            }


            transforms.Add(entryTransform);
        }

        public void AddHighscoreEntry(int score, string name)
        {
            // 점수 엔트리 생성
            HighscoreEntry entry = new HighscoreEntry { score = score, name = name };

            // 새로 추가된 항목 저장
            PlayerPrefs.SetInt("latestScore", score);
            PlayerPrefs.SetString("latestName", name);

            // 점수 로드
            string jsonString = PlayerPrefs.GetString("highscoreTable");
            Highscores highscores = new Highscores();

            if (!string.IsNullOrEmpty(jsonString))
            {
                highscores = JsonUtility.FromJson<Highscores>(jsonString);
            }
            else
            {
                highscores.highscoreEntries = new List<HighscoreEntry>();
            }

            // 새로운 점수 엔트리 추가
            highscores.highscoreEntries.Add(entry);

            // 점수 내림차순 정렬
            highscores.highscoreEntries.Sort((x, y) => y.score.CompareTo(x.score));

            // 상위 10개 점수만 유지
            if (highscores.highscoreEntries.Count > MAX_ENTRY)
            {
                highscores.highscoreEntries.RemoveRange(MAX_ENTRY, highscores.highscoreEntries.Count - MAX_ENTRY);
            }

            // 점수 업데이트
            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();
        }

        public class Highscores
        {
            public List<HighscoreEntry> highscoreEntries;
        }


        [System.Serializable]
        public class HighscoreEntry
        {
            public int score;
            public string name;
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

}
