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
    public Dictionary<int, Dictionary<int, string>> questionsType_3 = new Dictionary<int, Dictionary<int, string>>();

    // Shared array of options
    public Dictionary<int, string[]> optionsType_1 = new Dictionary<int, string[]>();
    public Dictionary<int, string[]> optionsType_2 = new Dictionary<int, string[]>();
    public Dictionary<int, List<string>> optionsType_3 = new Dictionary<int, List<string>>();

    public Dictionary<int, string> surahName = new Dictionary<int, string> {
        { 1, "Al-Fatihah" },
        { 112, "Al-Ihklas" },
        { 113, "Al-Falaq" },
        { 114, "An-Nas" }
    };

    void Awake()
    {
        // ~~~~~~~~~~~~~~~~~~~~~~ Type 1 Questions ~~~~~~~~~~~~~~~~~~~~~~
        // Populate the question database with sample questions and options
        questionsType_1.Add(1, new List<Question>{
            new Question("Apa arti Al-Ikhlas", "dalam Bahasa Indonesia?", 0), // ayat 1
            new Question("Berapa banyak ayat yang terdapat dalam", "Surah Al-Ikhlas?", 4),
            new Question("Berikut adalah isi ayat:", "قُلْ هُوَ ٱللَّهُ أَحَدٌ", 8),
            new Question("Surah Al-Ikhlas menekankan aspek fundamental apa", "dari iman Islam?", 12), // ayat 2
            new Question("Di Juz' (bagian) Quran mana Surah", "Al-Ikhlas berada?", 16),
            new Question("Berikut adalah isi ayat:", "ٱللَّهُ ٱلصَّمَدُ", 9),
            new Question("Pesan utama apa yang disampaikan dalam", "Surah Al-Ikhlas?", 24), // ayat 3
            new Question("Surah Al-Ikhlas sering", "disebut sebagai:", 28),
            new Question("Berikut adalah isi ayat:", "لَمْ يَلِدْ وَلَمْ يُولَدْ", 5),
            new Question("Nabi mana yang disebut dalam Surah Al-Ikhlas", "sebagai Rasul Allah?", 36), // ayat 4
            new Question("Surah Al-Ikhlas menolak konsep apa yang bertentangan", "dengan keesaan Allah?", 40),
            new Question("Berikut adalah isi ayat:", "وَلَمْ يَكُن لَّهُۥ كُفُوًا أَحَدٌۢ", 4),
            new Question("Surah Al-Ikhlas menekankan aspek fundamental apa", "dari iman Islam?", 48), // ayat 5
            new Question("Di Juz' (bagian) Quran mana Surah", "Al-Ikhlas berada?", 52),
            new Question("Berikut adalah isi ayat:", "ٱللَّهُ ٱلصَّمَدُ", 56),
            new Question("Pesan utama apa yang disampaikan dalam", "Surah Al-Ikhlas?", 60), // ayat 6
            new Question("Surah Al-Ikhlas sering", "disebut sebagai:", 64),
            new Question("Berikut adalah isi ayat:", "لَمْ يَلِدْ وَلَمْ يُولَدْ", 68),
            new Question("Nabi mana yang disebut dalam Surah Al-Ikhlas", "sebagai Rasul Allah?", 72), // ayat 7
            new Question("Surah Al-Ikhlas menolak konsep apa yang bertentangan", "dengan keesaan Allah?", 76),
            new Question("Berikut adalah isi ayat:", "وَلَمْ يَكُن لَّهُۥ كُفُوًا أَحَدٌۢ", 80),
        });
        questionsType_1.Add(112, new List<Question>{
            new Question("Apa arti Al-Ikhlas", "dalam Bahasa Indonesia?", 0), // ayat 1
            new Question("Berapa banyak ayat yang terdapat dalam", "Surah Al-Ikhlas?", 4),
            new Question("Berikut ini merupakan isi ayat?", "قُلْ هُوَ ٱللَّهُ أَحَدٌ", 8),
            new Question("Surah Al-Ikhlas menekankan aspek fundamental apa", "dari iman Islam?", 12), // ayat 2
            new Question("Di Juz' (bagian) Quran mana Surah", "Al-Ikhlas berada?", 16),
            new Question("Berikut ini merupakan isi ayat?", "ٱللَّهُ ٱلصَّمَدُ", 9),
            new Question("Pesan utama apa yang disampaikan dalam", "Surah Al-Ikhlas?", 24), // ayat 3
            new Question("Surah Al-Ikhlas sering", "disebut sebagai:", 28),
            new Question("Berikut ini merupakan isi ayat?", "لَمْ يَلِدْ وَلَمْ يُولَدْ", 5),
            new Question("Nabi mana yang disebut dalam Surah Al-Ikhlas", "sebagai Rasul Allah?", 36), // ayat 4
            new Question("Surah Al-Ikhlas menolak konsep apa yang bertentangan", "dengan keesaan Allah?", 40),
            new Question("Berikut ini merupakan isi ayat?", "وَلَمْ يَكُن لَّهُۥ كُفُوًا أَحَدٌۢ", 4),
        });
        questionsType_1.Add(113, new List<Question>{
            new Question("Apa arti Al-Ikhlas", "dalam Bahasa Indonesia?", 0), // ayat 1
            new Question("Berapa banyak ayat yang terdapat dalam", "Surah Al-Ikhlas?", 4),
            new Question("Berikut adalah isi ayat:", "قُلْ هُوَ ٱللَّهُ أَحَدٌ", 8),
            new Question("Surah Al-Ikhlas menekankan aspek fundamental apa", "dari iman Islam?", 12), // ayat 2
            new Question("Di Juz' (bagian) Quran mana Surah", "Al-Ikhlas berada?", 16),
            new Question("Berikut adalah isi ayat:", "ٱللَّهُ ٱلصَّمَدُ", 9),
            new Question("Pesan utama apa yang disampaikan dalam", "Surah Al-Ikhlas?", 24), // ayat 3
            new Question("Surah Al-Ikhlas sering", "disebut sebagai:", 28),
            new Question("Berikut adalah isi ayat:", "لَمْ يَلِدْ وَلَمْ يُولَدْ", 5),
            new Question("Nabi mana yang disebut dalam Surah Al-Ikhlas", "sebagai Rasul Allah?", 36), // ayat 4
            new Question("Surah Al-Ikhlas menolak konsep apa yang bertentangan", "dengan keesaan Allah?", 40),
            new Question("Berikut adalah isi ayat:", "وَلَمْ يَكُن لَّهُۥ كُفُوًا أَحَدٌۢ", 4),
            new Question("Nabi mana yang disebut dalam Surah Al-Ikhlas", "sebagai Rasul Allah?", 48), // ayat 5
            new Question("Surah Al-Ikhlas menolak konsep apa yang bertentangan", "dengan keesaan Allah?", 52),
            new Question("Berikut adalah isi ayat:", "وَلَمْ يَكُن لَّهُۥ كُفُوًا أَحَدٌۢ", 56),
        });
        questionsType_1.Add(114, new List<Question>{
            new Question("Apa arti Al-Ikhlas", "dalam Bahasa Indonesia?", 0), // ayat 1
            new Question("Berapa banyak ayat yang terdapat dalam", "Surah Al-Ikhlas?", 4),
            new Question("Berikut adalah isi ayat:", "قُلْ هُوَ ٱللَّهُ أَحَدٌ", 8),
            new Question("Surah Al-Ikhlas menekankan aspek fundamental apa", "dari iman Islam?", 12), // ayat 2
            new Question("Di Juz' (bagian) Quran mana Surah", "Al-Ikhlas berada?", 16),
            new Question("Berikut adalah isi ayat:", "ٱللَّهُ ٱلصَّمَدُ", 9),
            new Question("Pesan utama apa yang disampaikan dalam", "Surah Al-Ikhlas?", 24), // ayat 3
            new Question("Surah Al-Ikhlas sering", "disebut sebagai:", 28),
            new Question("Berikut adalah isi ayat:", "لَمْ يَلِدْ وَلَمْ يُولَدْ", 5),
            new Question("Nabi mana yang disebut dalam Surah Al-Ikhlas", "sebagai Rasul Allah?", 36), // ayat 4
            new Question("Surah Al-Ikhlas menolak konsep apa yang bertentangan", "dengan keesaan Allah?", 40),
            new Question("Berikut adalah isi ayat:", "وَلَمْ يَكُن لَّهُۥ كُفُوًا أَحَدٌۢ", 4),
            new Question("Pesan utama apa yang disampaikan dalam", "Surah Al-Ikhlas?", 48), // ayat 5
            new Question("Surah Al-Ikhlas sering", "disebut sebagai:", 52),
            new Question("Berikut adalah isi ayat:", "لَمْ يَلِدْ وَلَمْ يُولَدْ", 56),
            new Question("Nabi mana yang disebut dalam Surah Al-Ikhlas", "sebagai Rasul Allah?", 60), // ayat 6
            new Question("Surah Al-Ikhlas menolak konsep apa yang bertentangan", "dengan keesaan Allah?", 64),
            new Question("Berikut adalah isi ayat:", "وَلَمْ يَكُن لَّهُۥ كُفُوًا أَحَدٌۢ", 68),
        });

        // Set shared options
        optionsType_1.Add(1, new string[] { 
            "Keikhlasan", "Malam", "Pengampunan", "Yang Maha Pengasih", // Question 1, answer index 0
            "4", "3", "5", "6", // Question 2, answer index 4
            "1", "2", "7", "8", // Question 3, answer index 8
            "Tauhid", "Zakat", "Shalat", "Puasa", // Question 4, answer index 12
            "30", "27", "28", "29", // Question 5, answer index 16
            "9", "10", "11", "12", // Question 6, answer index 9
            "Keesaan Allah", "Atribut belas kasih Allah", "Pentingnya silathurami", "Pentingnya shalat", // Question 7, answer index 24
            "Ketulusan", "Kunci", "Kriteria", "Cahaya", // Question 8, answer index 28
            "13", "14", "15", "16", // Question 9, answer index 5
            "Muhammad", "Ibrahim", "Isa", "Musa", // Question 10, answer index 36
            "Politesime", "Ateisme", "Agnotisisme", "Pantheisme", // Question 11, answer index 40
            "17", "18", "19", "20", // Question 12, answer index 4
            "Tauhid", "Zakat", "Shalat", "Puasa", // Question 13, answer index 48
            "30", "27", "28", "29", // Question 14, answer index 52
            "2", "1", "3", "4", // Question 15, answer index 56
            "Keesaan Allah", "Atribut belas kasih Allah", "Pentingnya silathurami", "Pentingnya shalat", // Question 16, answer index 60
            "Ketulusan", "Kunci", "Kriteria", "Cahaya", // Question 17, answer index 64
            "3", "1", "2", "4", // Question 18, answer index 68
            "Muhammad", "Ibrahim", "Isa", "Musa", // Question 19, answer index 72
            "Politesime", "Ateisme", "Agnotisisme", "Pantheisme", // Question 20, answer index 76
            "4", "1", "2", "3", // Question 21, answer index 80
        });
        optionsType_1.Add(112, new string[] { 
            "Keikhlasan", "Malam", "Pengampunan", "Yang Maha Pengasih", // Question 1, answer index 0
            "4", "3", "5", "6", // Question 2, answer index 4
            "1", "2", "7", "8", // Question 3, answer index 8
            "Tauhid", "Zakat", "Shalat", "Puasa", // Question 4, answer index 12
            "30", "27", "28", "29", // Question 5, answer index 16
            "9", "10", "11", "12", // Question 6, answer index 9
            "Keesaan Allah", "Atribut belas kasih Allah", "Pentingnya silathurami", "Pentingnya shalat", // Question 7, answer index 24
            "Ketulusan", "Kunci", "Kriteria", "Cahaya", // Question 8, answer index 28
            "13", "14", "15", "16", // Question 9, answer index 5
            "Muhammad", "Ibrahim", "Isa", "Musa", // Question 10, answer index 36
            "Politesime", "Ateisme", "Agnotisisme", "Pantheisme", // Question 11, answer index 40
            "17", "18", "19", "20", // Question 12, answer index 4
        });
        optionsType_1.Add(113, new string[] { 
            "Keikhlasan", "Malam", "Pengampunan", "Yang Maha Pengasih", // Question 1, answer index 0
            "4", "3", "5", "6", // Question 2, answer index 4
            "1", "2", "7", "8", // Question 3, answer index 8
            "Tauhid", "Zakat", "Shalat", "Puasa", // Question 4, answer index 12
            "30", "27", "28", "29", // Question 5, answer index 16
            "9", "10", "11", "12", // Question 6, answer index 9
            "Keesaan Allah", "Atribut belas kasih Allah", "Pentingnya silathurami", "Pentingnya shalat", // Question 7, answer index 24
            "Ketulusan", "Kunci", "Kriteria", "Cahaya", // Question 8, answer index 28
            "13", "14", "15", "16", // Question 9, answer index 5
            "Muhammad", "Ibrahim", "Isa", "Musa", // Question 10, answer index 36
            "Politesime", "Ateisme", "Agnotisisme", "Pantheisme", // Question 11, answer index 40
            "17", "18", "19", "20", // Question 12, answer index 4
            "Muhammad", "Ibrahim", "Isa", "Musa", // Question 13, answer index 48
            "Politesime", "Ateisme", "Agnotisisme", "Pantheisme", // Question 14, answer index 52
            "4", "1", "2", "3", // Question 15, answer index 56
        });
        optionsType_1.Add(114, new string[] { 
            "Keikhlasan", "Malam", "Pengampunan", "Yang Maha Pengasih", // Question 1, answer index 0
            "4", "3", "5", "6", // Question 2, answer index 4
            "1", "2", "7", "8", // Question 3, answer index 8
            "Tauhid", "Zakat", "Shalat", "Puasa", // Question 4, answer index 12
            "30", "27", "28", "29", // Question 5, answer index 16
            "9", "10", "11", "12", // Question 6, answer index 9
            "Keesaan Allah", "Atribut belas kasih Allah", "Pentingnya silathurami", "Pentingnya shalat", // Question 7, answer index 24
            "Ketulusan", "Kunci", "Kriteria", "Cahaya", // Question 8, answer index 28
            "13", "14", "15", "16", // Question 9, answer index 5
            "Muhammad", "Ibrahim", "Isa", "Musa", // Question 10, answer index 36
            "Politesime", "Ateisme", "Agnotisisme", "Pantheisme", // Question 11, answer index 40
            "17", "18", "19", "20", // Question 12, answer index 4
            "Keesaan Allah", "Atribut belas kasih Allah", "Pentingnya silathurami", "Pentingnya shalat", // Question 13, answer index 48
            "Ketulusan", "Kunci", "Kriteria", "Cahaya", // Question 14, answer index 52
            "3", "1", "2", "4", // Question 15, answer index 56
            "Muhammad", "Ibrahim", "Isa", "Musa", // Question 16, answer index 60
            "Politesime", "Ateisme", "Agnotisisme", "Pantheisme", // Question 17, answer index 64
            "4", "1", "2", "3", // Question 18, answer index 68
        });

        // ~~~~~~~~~~~~~~~~~~~~~~ Type 2 Questions ~~~~~~~~~~~~~~~~~~~~~~
        // Populate the question database with sample questions and options
        questionsType_2.Add(1, new List<Question>{
            new Question("What is the capital of France?", "CasualGameSounds/DM-CGS-49", 0),
            new Question("Which country is known as the Land of the Rising Sun?", "CasualGameSounds/DM-CGS-49", 4),
            new Question("What is the largest planet in our solar system?", "CasualGameSounds/DM-CGS-49", 8),
            new Question("Which planet is known as the Red Planet?", "CasualGameSounds/DM-CGS-49", 10),
            new Question("Who painted the Mona Lisa?", "CasualGameSounds/DM-CGS-49", 16),
            new Question("Who wrote the famous play Romeo and Juliet?", "CasualGameSounds/DM-CGS-49", 20),
            new Question("What is the chemical symbol for Water?", "CasualGameSounds/DM-CGS-49", 24),
            new Question("What is the largest mammal on Earth?", "CasualGameSounds/DM-CGS-49", 28),
            new Question("Which of the following is NOT a primary color?", "CasualGameSounds/DM-CGS-49", 32),
            new Question("What is the currency of Japan?", "CasualGameSounds/DM-CGS-49", 36),
            new Question("What is the chemical symbol for Water?", "CasualGameSounds/DM-CGS-49", 40),
            new Question("What is the largest mammal on Earth?", "CasualGameSounds/DM-CGS-49", 44),
            new Question("Which of the following is NOT a primary color?", "CasualGameSounds/DM-CGS-49", 48),
            new Question("What is the currency of Japan?", "CasualGameSounds/DM-CGS-49", 52)
        });
        questionsType_2.Add(112, new List<Question>{
            new Question("Berikut ini merupakan bunyi ayat?", "CasualGameSounds/DM-CGS-49", 0),
            new Question("Berikut ini merupakan potongan ayat?", "CasualGameSounds/DM-CGS-49", 4),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "CasualGameSounds/DM-CGS-49", 8),
            new Question("Which planet is known as the Red Planet?", "CasualGameSounds/DM-CGS-49", 10),
            new Question("Who painted the Mona Lisa?", "CasualGameSounds/DM-CGS-49", 16),
            new Question("Who wrote the famous play Romeo and Juliet?", "CasualGameSounds/DM-CGS-49", 20),
            new Question("What is the chemical symbol for Water?", "CasualGameSounds/DM-CGS-49", 24),
            new Question("What is the largest mammal on Earth?", "CasualGameSounds/DM-CGS-49", 28),
            new Question("Which of the following is NOT a primary color?", "CasualGameSounds/DM-CGS-49", 32),
            new Question("What is the currency of Japan?", "CasualGameSounds/DM-CGS-49", 36)
        });
        questionsType_2.Add(113, new List<Question>{
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
        questionsType_2.Add(114, new List<Question>{
            new Question("What is the capital of France?", "CasualGameSounds/DM-CGS-49", 0),
            new Question("Which country is known as the Land of the Rising Sun?", "CasualGameSounds/DM-CGS-49", 4),
            new Question("What is the largest planet in our solar system?", "CasualGameSounds/DM-CGS-49", 8),
            new Question("Which planet is known as the Red Planet?", "CasualGameSounds/DM-CGS-49", 10),
            new Question("Who painted the Mona Lisa?", "CasualGameSounds/DM-CGS-49", 16),
            new Question("Who wrote the famous play Romeo and Juliet?", "CasualGameSounds/DM-CGS-49", 20),
            new Question("What is the chemical symbol for Water?", "CasualGameSounds/DM-CGS-49", 24),
            new Question("What is the largest mammal on Earth?", "CasualGameSounds/DM-CGS-49", 28),
            new Question("Which of the following is NOT a primary color?", "CasualGameSounds/DM-CGS-49", 32),
            new Question("What is the currency of Japan?", "CasualGameSounds/DM-CGS-49", 36),
            new Question("Which of the following is NOT a primary color?", "CasualGameSounds/DM-CGS-49", 40),
            new Question("What is the currency of Japan?", "CasualGameSounds/DM-CGS-49", 44),
        });

        // Set shared options
        optionsType_2.Add(1, new string[] { 
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
            "H2O", "CO2", "NaCl", "O2", // Question 7, answer index 40
            "Blue Whale", "Elephant", "Giraffe", "Hippopotamus", // Question 8, answer index 44
            "Yellow", "Red", "Blue", "Green", // Question 9, answer index 48
            "Yen", "Dollar", "Euro", "Pound", // Question 10, answer index 52
        });
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
        optionsType_2.Add(113, new string[] { 
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
        optionsType_2.Add(114, new string[] { 
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
            "Yellow", "Red", "Blue", "Green", // Question 9, answer index 40
            "Yen", "Dollar", "Euro", "Pound", // Question 10, answer index 44
        });

        // ~~~~~~~~~~~~~~~~~~~~~~ Type 3 Questions (Matching) ~~~~~~~~~~~~~~~~~~~~~~
        // Questions
        questionsType_3.Add(112, new Dictionary<int, string>{
            { 0, "Ayat 1" },
            { 1, "Arti" },
            { 2, "Ayat 2" },
            { 3, "Menekankan Konsep" },
            { 4, "Ayat 3" },
            { 5, "Pesan Utama" },
            { 6, "Ayat 4" },
            { 7, "Sering Disebut" },
        });

        // Options
        optionsType_3.Add(112, new List<string> {
            { "قُلْ هُوَ ٱللَّهُ أَحَدٌ" } ,
            { "Keihklasan" },
            { "ٱللَّهُ ٱلصَّمَدُ" },
            { "Tauhid" },
            { "لَمْ يَلِدْ وَلَمْ يُولَدْ" },
            { "Keesaan Allah" },
            {  "وَلَمْ يَكُن لَّهُۥ كُفُوًا أَحَدٌۢ" },
            {  "Ketulusan" },
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

