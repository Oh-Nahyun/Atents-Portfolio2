using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class RankPanel : MonoBehaviour
{
    /// <summary>
    /// 패널에서 표시되는 랭크 한 줄
    /// </summary>
    RankLine[] rankLines;

    /// <summary>
    /// 최고 득점 (1등 ~ 5등)
    /// </summary>
    int[] highScores;

    /// <summary>
    /// 최고 득점자 이름 (1등 ~ 5등)
    /// </summary>
    string[] rankerNames;

    /// <summary>
    /// 여기서 표시할 랭크 수
    /// </summary>
    const int rankCount = 5;

    private void Awake()
    {
        rankLines = GetComponentsInChildren<RankLine>();
        highScores = new int[rankCount];
        rankerNames = new string[rankCount];
    }

    /// <summary>
    /// 랭킹 데이터 초기값으로 모두 설정하는 함수
    /// </summary>
    void SetDefaultData()
    {
        /// 실습_240123
        /// 1st AAA 1000000
        /// 2nd BBB 100000
        /// 3rd CCC 10000
        /// 4th DDD 1000
        /// 5th EEE 100
        for (int i = 0; i < rankCount; i++)
        {
            // highScores 채우기
            int score = 10;
            for (int j = rankCount - i; j > 0; j--)
            {
                score *= 10;
            }
            highScores[i] = score;

            // rankerNames 채우기
            char temp = 'A'; // temp = 65
            temp = (char)((byte)temp + (byte)i);
            rankerNames[i] = $"{temp}{temp}{temp}"; // AAA ~ EEE
        }

        RefreshRankLines(); // UI 갱신
    }

    /// <summary>
    /// 랭킹 데이터를 파일에 저장하는 함수
    /// </summary>
    void SaveRankData()
    {
        SaveData data = new SaveData(); // 저장용 클래스 인스턴스 만들기
        data.rankerNames = rankerNames; // 저장용 객체에 데이터 넣기
        data.highScores = highScores;
        string jsonText = JsonUtility.ToJson(data); // 저장용 객체의 내용을 json 형식의 문자열로 변경

        string path = $"{Application.dataPath}/Save/";
        //string path1 = "\\Save"; // 다 같은 표현
        //string path2 = "/Save";
        //string path3 = @"\Save";

        if (!Directory.Exists(path)) // Exists : true면 폴더가 있다. false면 폴더가 없다.
        {
            // path 폴더가 없다.
            Directory.CreateDirectory(path); // path에 지정된 폴더를 만든다.
        }

        //string fullPath = $"{path}Save.txt";
        //System.IO.File.WriteAllText(fullPath, "저장저장");

        // json
        string fullPath = $"{path}Save.json"; // 전체 경로 만들기
        System.IO.File.WriteAllText(fullPath, jsonText); // 파일로 저장
    }

    /// <summary>
    /// 저장된 랭킹 데이터를 불러오는 함수
    /// </summary>
    /// <returns>성공 여부 (true면 성, flase면 실패)</returns>
    bool LoadRankData()
    {
        bool result = false;

        return result;
    }

    /// <summary>
    /// 랭킹 데이터를 업데이트하는 함수
    /// </summary>
    /// <param name="score"></param>
    void UpdateRankData(int score)
    {

    }

    /// <summary>
    /// 현재 설정된 랭킹 데이터에 맞게 UI 갱신하는 함수
    /// </summary>
    void RefreshRankLines()
    {
        for (int i = 0; i < rankCount; i++)
        {
            rankLines[i].SetData(rankerNames[i], highScores[i]);
        }
    }

    public void Test_DefaultRankPanel()
    {
        SetDefaultData();
        RefreshRankLines();
    }

    public void Test_SaveRankPanel()
    {
        SetDefaultData();
        RefreshRankLines();
        SaveRankData();
    }
}
