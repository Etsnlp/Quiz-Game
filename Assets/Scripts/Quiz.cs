using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;

    [Header("Answers")]
    [SerializeField] GameObject[] answersButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();

    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;

        if(timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        { 
            DisplayAnswer(-1);
            SetButtonsState(false);


        }
        
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonsState(false);
        timer.CancelTimer();
    }

        void DisplayAnswer(int index)
    {
        Image buttonImage;

        if(index == question.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct";
            buttonImage = answersButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        } 
        else
        {
            correctAnswerIndex = question.GetCorrectAnswerIndex();
            string correctAnswer = question.GetAnswer(correctAnswerIndex);
            questionText.text= "Wrong! the correct answer was;\n" + correctAnswer;
            buttonImage = answersButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            //Image wrongButtonImage = answersButtons[index].GetComponent<Image>();
            //wrongButtonImage.sprite = wrongAnswerSprite;
        }

    }



    void GetNextQuestion()
    {
        SetButtonsState(true);
        SetDefaultButtonSprites();
        DisplayQuestion();

    }

    void DisplayQuestion()
    {
        questionText.text = question.GetQuestion();

        for(int i = 0; i < answersButtons.Length; i++)
        {
        TextMeshProUGUI buttonText = answersButtons[i].GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = question.GetAnswer(i);
        }

    }

    
    void SetButtonsState(bool state)
    {
        for(int i =0; i < answersButtons.Length; i++)
        {
            Button button = answersButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {

        for(int i = 0; i < answersButtons.Length; i++)
        {
            Image buttonImage = answersButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }

    }

}
