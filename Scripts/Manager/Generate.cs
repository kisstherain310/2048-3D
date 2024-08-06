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
    public int lastNumber = 64;
    public int lastOfLastNumber = 32;
    public int lastOfLastOfLastNumber = 16;
    public int GenerateNumber(int score){
        int number = ProcessScoreNormal(score);
        lastOfLastOfLastNumber = lastOfLastNumber;
        lastOfLastNumber = lastNumber;
        lastNumber = number;
        return number;
    }
    private int ProcessScoreEasy(int score){
        if(score < 20) return GenerateInit();
        else if(score < 500) return GenerateEasy();
        else if(score < 1000) return GenerateNormal();
        else if(score < 10000) return GenerateHard();
        else if(score < 100000) return GenerateHarder();
        else return GenerateHardest();
    }
    private int ProcessScoreNormal(int score){
        if(score < 20) return GenerateInit();
        else if(score < 700) return GenerateEasy();
        else if(score < 2000) return GenerateNormal();
        else if(score < 5000) return GenerateHard();
        else if(score < 100000) return GenerateHarder();
        else return GenerateHardest();
    }
    private int ProcessScoreHard(int score){
        if(score < 20) return GenerateInit();
        else if(score < 1000) return GenerateHard();
        else if(score < 10000) return GenerateHarder();
        else return GenerateHardest();
    }
    private int GenerateInit(){
        int number = (int)Mathf.Pow(2, Random.Range(1, 3));
        return number;
    }
    private int GenerateEasy(){
        int number = (int)Mathf.Pow(2, Random.Range(1, 4));
        while (number == lastNumber)
            number = (int)Mathf.Pow(2, Random.Range(1, 4));
        return number;
    }
    private int GenerateNormal(){
        int number = (int)Mathf.Pow(2, Random.Range(1, 6));
        while (number == lastNumber)
            number = (int)Mathf.Pow(2, Random.Range(1, 6));
        return number;
    }
    private int GenerateHard(){
        int number = (int)Mathf.Pow(2, Random.Range(1, 7));
        while (number == lastNumber)
            number = (int)Mathf.Pow(2, Random.Range(1, 7));
        return number;
    }
    private int GenerateHarder(){
        int number = (int)Mathf.Pow(2, Random.Range(1, 7));
        while (number == lastNumber || number == lastOfLastNumber)
        {
            number = (int)Mathf.Pow(2, Random.Range(1, 7));
        }
        return number;
    }
    private int GenerateHardest(){
        int number = (int)Mathf.Pow(2, Random.Range(1, 7));
        while (number == lastNumber || number == lastOfLastNumber || number == lastOfLastOfLastNumber)
        {
            number = (int)Mathf.Pow(2, Random.Range(1, 7));
        }
        return number;
    }
}
