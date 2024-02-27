using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // This attribute allows the class to be visible in the Unity inspector.
public class LevelData
{
    public int surahNumber;
    public string surahName;
    public string idnName;
    public string arabicName;
    public List<int> score; // total score 0 - infinity
    public List<int> gradeScore; // score 0 - 100 to define grade S, A, B, C

    public LevelData(int surahNumber, string surahName, string idnName, string arabicName, List<int> score, List<int> gradeScore)
    {
        this.surahNumber = surahNumber;
        this.surahName = surahName;
        this.idnName = idnName;
        this.arabicName = arabicName;
        this.score = score;
        this.gradeScore = gradeScore;
    }
}

// [System.Serializable]
// public class ScoreData
// {
//     public int S;
//     public int A;
//     public int B;
//     public int C;
    
//     public ScoreData(int S, int A, int B, int C)
//     {
//         this.S = S;
//         this.A = A;
//         this.B = B;
//         this.C = C;
//     }
// }