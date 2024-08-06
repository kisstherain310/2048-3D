using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    public static Generate instance;
    void Awake()
    {
        instance = this;
    }
    string typeUser = "normal";
    public int lastNumber = 64;
    public int lastOfLastNumber = 32;
    public int lastOfLastOfLastNumber = 16;
    public int GenerateNumber(int score)
    {
        int number = 2;
        switch (typeUser)
        {
            case "easy":
                number = ProcessScoreEasy(score);
                break;
            case "normal":
                number = ProcessScoreNormal(score);
                break;
            case "hard":
                number = ProcessScoreHard(score);
                break;
        }
        lastOfLastOfLastNumber = lastOfLastNumber;
        lastOfLastNumber = lastNumber;
        lastNumber = number;
        return number;
    }
    public void ProcessUserData()
    {
        float average = 0;
        List<int> listScore = GameManager.Instance.dataManager.userData.listScore;
        if (listScore == null || listScore.Count <= 3)
        {
            typeUser = "normal";
            return;
        }
        for (int i = 0; i < listScore.Count; i++)
        {
            average += (float)listScore[i];
        }
        average /= listScore.Count;
        if (average < 1000) typeUser = "easy";
        else if (average < 10000) typeUser = "normal";
        else typeUser = "hard";
    }
    private int ProcessScoreEasy(int score)
    {
        if (score < 20) return GenerateInit();
        else if (score < 600) return GenerateEasy();
        else if (score < 1000) return GenerateNormal();
        else if (score < 10000) return GenerateHard();
        else if (score < 100000) return GenerateHarder();
        else return GenerateHardest();
    }
    private int ProcessScoreNormal(int score)
    {
        if (score < 20) return GenerateInit();
        else if (score < 500) return GenerateEasy();
        else if (score < 1000) return GenerateNormal();
        else if (score < 5000) return GenerateHard();
        else if (score < 20000) return GenerateHarder();
        else return GenerateHardest();
    }
    private int ProcessScoreHard(int score)
    {
        if (score < 20) return GenerateInit();
        else if (score < 1000) return GenerateHard();
        else if (score < 10000) return GenerateHarder();
        else return GenerateHardest();
    }
    private int GenerateInit()
    {
        int number = (int)Mathf.Pow(2, Random.Range(1, 3));
        return number;
    }
    private int GenerateEasy()
    {
        int number = (int)Mathf.Pow(2, Random.Range(1, 4));
        while (number == lastNumber)
            number = (int)Mathf.Pow(2, Random.Range(1, 4));
        return number;
    }
    private int GenerateNormal()
    {
        int number = (int)Mathf.Pow(2, Random.Range(1, 6));
        while (number == lastNumber)
            number = (int)Mathf.Pow(2, Random.Range(1, 6));
        return number;
    }
    private int GenerateHard()
    {
        int number = (int)Mathf.Pow(2, Random.Range(1, 7));
        while (number == lastNumber)
            number = (int)Mathf.Pow(2, Random.Range(1, 7));
        return number;
    }
    private int GenerateHarder()
    {
        int number = (int)Mathf.Pow(2, Random.Range(1, 7));
        while (number == lastNumber || number == lastOfLastNumber)
        {
            number = (int)Mathf.Pow(2, Random.Range(1, 7));
        }
        return number;
    }
    private int GenerateHardest()
    {
        int number = (int)Mathf.Pow(2, Random.Range(1, 7));
        while (number == lastNumber || number == lastOfLastNumber || number == lastOfLastOfLastNumber)
        {
            number = (int)Mathf.Pow(2, Random.Range(1, 7));
        }
        return number;
    }
}
