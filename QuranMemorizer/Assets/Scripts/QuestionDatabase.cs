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
    public Dictionary<int, List<Question>> questionsType_1 = new Dictionary<int, List<Question>>();
    public Dictionary<int, List<Question>> questionsType_2 = new Dictionary<int, List<Question>>();

    // Shared array of options
    public Dictionary<int, string[]> optionsType_1 = new Dictionary<int, string[]>();
    public Dictionary<int, string[]> optionsType_2 = new Dictionary<int, string[]>();

    void Awake()
    {
        // ~~ Type 1 Questions ~~
        // Populate the question database with sample questions and options
        questionsType_1.Add(112, new List<Question>{
            new Question("Apa arti Al-Ikhlas", "dalam Bahasa Indonesia?", 0),
            new Question("Berapa banyak ayat yang terdapat dalam", "Surah Al-Ikhlas?", 4),
            new Question("Surah Al-Ikhlas menekankan aspek fundamental apa", "dari iman Islam?", 8),
            new Question("Di Juz' (bagian) Quran mana Surah", "Al-Ikhlas berada?", 12),
            new Question("Pesan utama apa yang disampaikan dalam", "Surah Al-Ikhlas?", 16),
            new Question("Surah Al-Ikhlas sering", "disebut sebagai:", 20),
            new Question("Nabi mana yang disebut dalam Surah Al-Ikhlas", "sebagai Rasul Allah?", 24),
            new Question("Surah Al-Ikhlas menolak konsep apa yang bertentangan", "dengan keesaan Allah?", 28),
            new Question("Kata pertama dari Surah", "Al-Ikhlas adalah?", 32),
            new Question("Surah Al-Ikhlas sering dibaca", "dalam shalat apa?", 36)
        });

        // Set shared options
        optionsType_1.Add(112, new string[] { 
            "Keikhlasan", "Malam", "Pengampunan", "Yang Maha Pengasih", // Question 1, answer index 0
            "3", "4", "5", "6", // Question 2, answer index 4
            "Tauhid", "Zakat", "Shalat", "Puasa", // Question 3, answer index 8
            "29", "27", "28", "30", // Question 4, answer index 12
            "Keesaan Allah", "Atribut belask kasih Allah", "Pentingnya silathurami", "Pentingnya shalat", // Question 5, answer index 16
            "Ketulusan", "Kunci", "Kriteria", "Cahaya", // Question 6, answer index 20
            "Muhammad", "Ibrahim", "Isa", "Musa", // Question 7, answer index 24
            "Politesime", "Ateisme", "Agnotisisme", "Pantheisme", // Question 8, answer index 28
            "Qul", "Huwa", "Allah", "Ar-Rahman", // Question 9, answer index 32
            "Isya", "Dzuhur", "Ashar", "Subuh", // Question 10, answer index 36
        });

        // ~~ Type 2 Questions ~~
        // Populate the question database with sample questions and options
        questionsType_2.Add(112, new List<Question>{
            new Question("What is the capital of France?", "CasualGameSounds/DM-CGS-49", 0),
            new Question("Which country is known as the Land of the Rising Sun?", "CasualGameSounds/DM-CGS-49", 4),
            new Question("What is the largest planet in our solar system?", "CasualGameSounds/DM-CGS-49", 8),
            new Question("Which planet is known as the Red Planet?", "CasualGameSounds/DM-CGS-49", 10),
            new Question("Who painted the Mona Lisa?", "CasualGameSounds/DM-CGS-49", 16),
            new Question("Who wrote the famous play Romeo and Juliet?", "CasualGameSounds/DM-CGS-49", 20),
            new Question("What is the chemical symbol for Water?", "CasualGameSounds/DM-CGS-49", 24),
            new Question("What is the largest mammal on Earth?", "CasualGameSounds/DM-CGS-49", 28),
            new Question("Which of the following is NOT a primary color?", "CasualGameSounds/DM-CGS-49", 32),
            new Question("What is the currency of Japan?", "CasualGameSounds/DM-CGS-49", 36)
        });

        // Set shared options
        optionsType_2.Add(112, new string[] { 
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

