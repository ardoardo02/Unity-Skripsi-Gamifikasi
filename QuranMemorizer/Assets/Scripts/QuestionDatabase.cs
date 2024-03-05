using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionDatabase : MonoBehaviour
{
    // Define a custom data structure to represent a question
    public class Question
    {
        public string mainQuestionText;
        public string secondaryQuestionText;
        public int correctAnswerIndex;

        public Question(string mainQuestionText, string secondaryQuestionText, int correctAnswerIndex)
        {
            this.mainQuestionText = mainQuestionText;
            this.secondaryQuestionText = secondaryQuestionText;
            this.correctAnswerIndex = correctAnswerIndex;
        }
    }

    // List to store your questions
    public Dictionary<int, List<Question>> questions = new Dictionary<int, List<Question>>();

    // Shared array of options
    public Dictionary<int, string[]> options = new Dictionary<int, string[]>();

    void Awake()
    {
        // Populate the question database with sample questions and options
        questions.Add(112, new List<Question>{
            new Question("What is the capital of", "France", 0),
            new Question("Which country is known as the", "Land of the Rising Sun", 4),
            new Question("What is the largest planet in our", "solar system", 8),
            new Question("Which planet is known as the", "Red Planet", 10),
            new Question("Who painted the", "Mona Lisa", 16),
            new Question("Who wrote the famous play", "Romeo and Juliet", 20),
            new Question("What is the chemical symbol for", "Water", 24),
            new Question("What is the largest mammal on", "Earth", 28),
            new Question("Which of the following is NOT", "a primary color", 32),
            new Question("What is the currency of", "Japan", 36)
        });

        // Set shared options
        options.Add(112, new string[] { 
            "Paris", "London", "Rome", "Berlin", // Question 1, answer index 0
            "Japan", "India", "China", "South Korea", // Question 2, answer index 4
            "Jupiter", "Saturn", "Mars", "Earth", // Question 3, answer index 8
            "Moon", "Venus", "Mercury", "Sun", // Question 4, answer index 10
            "Leonardo da Vinci", "Frida Kahlo", "Vincent van Gogh", "Pablo Picasso", // Question 5, answer index 16
            "William Shakespeare", "Oscar Wilde", "George Bernard Shaw", "Samuel Beckett", // Question 6, answer index 20
            "H2O", "CO2", "NaCl", "O2", // Question 7, answer index 24
            "Blue Whale", "Elephant", "Giraffe", "Hippopotamus", // Question 8, answer index 28
            "Yellow", "Red", "Blue", "Green", // Question 9, answer index 32
            "Yen", "Dollar", "Euro", "Pound", // Question 10, answer index 36
        });
    }

    // Example usage
    /*
    void Start()
    {
        // Access the first question
        Question currentQuestion = questions[0];

        // Display question text
        Debug.Log("Question: " + currentQuestion.mainQuestionText);

        // Display options
        foreach (string option in options)
        {
            Debug.Log("Option: " + option);
        }

        // Get the correct answer
        int correctAnswerIndex = currentQuestion.correctAnswerIndex;
        Debug.Log("Correct Answer Index: " + correctAnswerIndex);
        Debug.Log("Correct Answer: " + options[correctAnswerIndex]);
    }
    */
}

