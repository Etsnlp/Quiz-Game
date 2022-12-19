using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int questionSeen = 0;
    int correctAnswer = 0;

    public int GetCorrectAnswer()
    {
        return correctAnswer;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswer++;
    }

    public int GetQuestionSeen()
    {
        return questionSeen;
    }

    public void IncrementQuestionSeen()
    {
        questionSeen++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswer / (float)questionSeen * 100);
    }

}
