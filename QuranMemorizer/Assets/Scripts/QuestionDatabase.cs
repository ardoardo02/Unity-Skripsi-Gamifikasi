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
    public Dictionary<int, List<int[]>> questionsType_4 = new Dictionary<int, List<int[]>>();
    public Dictionary<int, List<string>> questionsType_5 = new Dictionary<int, List<string>>();

    // Shared array of options
    public Dictionary<int, string[]> optionsType_1 = new Dictionary<int, string[]>();
    public Dictionary<int, string[]> optionsType_2 = new Dictionary<int, string[]>();
    public Dictionary<int, List<string>> optionsType_3 = new Dictionary<int, List<string>>();
    public Dictionary<int, List<string>> optionsType_4 = new Dictionary<int, List<string>>();
    public Dictionary<int, List<string>> optionsType_5 = new Dictionary<int, List<string>>();

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
            new Question("Apa arti Al-Fatihah", "dalam Bahasa Indonesia?", 0), // ayat 1
            new Question("Berapa banyak ayat yang terdapat dalam", "Surah Al-Fatihah?", 4),
            new Question("Berikut adalah isi ayat:", "بِسْمِ اللّٰهِ الرَّحْمٰنِ الرَّحِيْمِ", 8),
            new Question("Berapa kali dalam sehari Surah Al-Fatihah dibaca", "dalam shalat wajib lima waktu?", 12), // ayat 2
            new Question("Surah Al-Fatihah sering", "disebut sebagai?", 16),
            new Question("Berikut adalah isi ayat:", "اَلْحَمْدُ لِلّٰهِ رَبِّ الْعٰلَمِيْنَۙ", 9),
            new Question("Apa arti dari", "'Ar-Rahmanir-Rahim'?", 24), // ayat 3
            new Question("Dalam Surah Al-Fatihah, kita memohon agar", " ditunjukkan jalan yang?", 28),
            new Question("Berikut adalah isi ayat:", "الرَّحْمٰنِ الرَّحِيْمِۙ", 10),
            new Question("Apa arti dari", "'Maliki yaumiddin'?", 26), // ayat 4
            new Question("Surah ke berapa dalam Al-Qur'an?", "Al-Fatihah berada?", 8),
            new Question("Berikut adalah isi ayat:", "مٰلِكِ يَوْمِ الدِّيْنِۗ", 11),
            new Question("Surah Al-Fatihah termasuk", "dalam kelompok surah?", 48), // ayat 5
            new Question("Apa manfaat membaca Surah Al-Fatihah", "dalam shalat?", 52),
            new Question("Berikut adalah isi ayat:", "اِيَّاكَ نَعْبُدُ وَاِيَّاكَ نَسْتَعِيْنُۗ", 5),
            new Question("Apa arti dari", "'Ihdinas siratal mustaqim'?", 27), // ayat 6
            new Question("Ayat mana dalam Surah Al-Fatihah yang", "mengandung permohonan petunjuk?", 6),
            new Question("Berikut adalah isi ayat:", "اِهْدِنَا الصِّرَاطَ الْمُسْتَقِيْمَۙ", 6),
            new Question("Apa yang dimaksud dengan", "Sab'ul Mathani?", 72), // ayat 7
            new Question("Mengapa Surah Al-Fatihah disebut", "Ummul Kitab?", 76),
            new Question("Berikut merupakan isi dari potongan ayat:", "صِرَاطَ الَّذِيْنَ اَنْعَمْتَ عَلَيْهِمْ ەۙ", 4),
        });
        questionsType_1.Add(112, new List<Question>{
            new Question("Apa arti Al-Ikhlas", "dalam Bahasa Indonesia?", 0), // ayat 1
            new Question("Berapa banyak ayat yang terdapat dalam", "Surah Al-Ikhlas?", 4),
            new Question("Berikut ini merupakan isi ayat?", "قُلْ هُوَ ٱللَّهُ أَحَدٌ", 8),
            new Question("Surah Al-Ikhlas menekankan aspek fundamental apa", "dari iman Islam?", 12), // ayat 2
            new Question("Di Juz' (bagian) Quran mana Surah", "Al-Ikhlas berada?", 16),
            new Question("Berikut ini merupakan isi ayat?", "ٱللَّهُ ٱلصَّمَدُ", 9),
            new Question("Pesan utama apa yang disampaikan dalam", "Surah Al-Ikhlas?", 24), // ayat 3
            new Question("Surah Al-Ikhlas sering", "disebut sebagai?", 28),
            new Question("Berikut ini merupakan isi ayat?", "لَمْ يَلِدْ وَلَمْ يُولَدْ", 5),
            new Question("Nabi mana yang disebut dalam Surah Al-Ikhlas", "sebagai Rasul Allah?", 36), // ayat 4
            new Question("Surah Al-Ikhlas menolak konsep apa yang bertentangan", "dengan keesaan Allah?", 40),
            new Question("Berikut ini merupakan isi ayat?", "وَلَمْ يَكُن لَّهُۥ كُفُوًا أَحَدٌۢ", 4),
        });
        questionsType_1.Add(113, new List<Question>{
            new Question("Apa arti Al-Falaq", "dalam Bahasa Indonesia?", 0), // ayat 1
            new Question("Berapa banyak ayat yang terdapat dalam", "Surah Al-Falaq?", 4),
            new Question("Berikut adalah isi ayat:", "قُلْ اَعُوْذُ بِرَبِّ الْفَلَقِۙ", 8),
            new Question("Apa tema utama dalam", "Surah Al-Falaq?", 12), // ayat 2
            new Question("Surah ke berapa dalam Al-Qur'an?", "Al-Falaq berada?", 16),
            new Question("Berikut adalah isi ayat:", "مِنْ شَرِّ مَا خَلَقَۙ", 9),
            new Question("Apa manfaat membaca Surah Al-Falaq", "secara rutin?", 24), // ayat 3
            new Question("Apa yang dimaksud dengan", "'ghasiqin idza waqab'?", 28),
            new Question("Berikut adalah isi ayat:", "وَمِنْ شَرِّ غَاسِقٍ اِذَا وَقَبَۙ", 5),
            new Question("Surah Al-Falaq termasuk dalam", "kelompok surah?", 36), // ayat 4
            new Question("Kejahatan tukang sihir disebutkan", "dalam ayat ke?", 6),
            new Question("Berikut adalah isi ayat:", "وَمِنْ شَرِّ النَّفّٰثٰتِ فِى الْعُقَدِۙ", 6),
            new Question("Dalam Surah Al-Falaq, kita memohon perlindungan dari", "kejahatan malam ketika...", 48), // ayat 5
            new Question("Dalam Surah Al-Falaq, dari kejahatan apa kita memohon", "perlindungan dalam ayat terakhir?", 52),
            new Question("Berikut adalah isi ayat:", "وَمِنْ شَرِّ حَاسِدٍ اِذَا حَسَدَࣖ", 4),
        });
        questionsType_1.Add(114, new List<Question>{
            new Question("Apa arti An-Nas", "dalam Bahasa Indonesia?", 0), // ayat 1
            new Question("Berapa banyak ayat yang terdapat dalam", "Surah An-Nas?", 4),
            new Question("Berikut adalah isi ayat:", "قُلْ اَعُوْذُ بِرَبِّ النَّاسِۙ", 8),
            new Question("Apa tema utama dalam", "Surah An-Nas?", 12), // ayat 2
            new Question("Surah ke berapa dalam Al-Qur'an?", "An-Nas berada?", 16),
            new Question("Berikut adalah isi ayat:", "مَلِكِ النَّاسِۙ", 9),
            new Question("Dari apa kita memohon perlindungan dalam", "Surah An-Nas?", 24), // ayat 3
            new Question("Siapa yang membisikkan kejahatan ke dalam dada manusia", "menurut Surah An-Nas?", 28),
            new Question("Berikut adalah isi ayat:", "اِلٰهِ النَّاسِۙ", 5),
            new Question("Surah An-Nas diturunkan", "di kota?", 36), // ayat 4
            new Question("Apa yang dimaksud dengan 'waswasil khannas'", " dalam Surah An-Nas?", 40),
            new Question("Berikut adalah isi ayat:", "مِنْ شَرِّ الْوَسْوَاسِ ەۙ الْخَنَّاسِۖ", 7),
            new Question("Surah An-Nas termasuk dalam", "kelompok surah?", 48), // ayat 5
            new Question("Siapa yang disebut sebagai raja manusia", "dalam Surah An-Nas?", 52),
            new Question("Berikut adalah isi ayat:", "الَّذِيْ يُوَسْوِسُ فِيْ صُدُوْرِ النَّاسِۙ", 6),
            new Question("Berapa kali kata 'An-Nas' disebutkan", "dalam Surah An-Nas?", 6), // ayat 6
            new Question("Apa manfaat membaca Surah An-Nas", "secara rutin?", 64),
            new Question("Berikut adalah isi ayat:", "مِنَ الْجِنَّةِ وَالنَّاسِ ࣖ", 4),
        });

        // Set shared options
        optionsType_1.Add(1, new string[] { 
            "Pembuka", "Penutup", "Pertengahan", "Akhir", // Question 1, answer index 0 -- ayat 1
            "7", "5", "6", "8", // answer index 4
            "1", "2", "3", "4", // answer index 8
            "17", "20", "23", "25", // Question 4, answer index 12 -- ayat 2
            "Ummul Kitab", "Al-Baqarah", "Al-Ikhlas", "Al-Kahfi", // answer index 16
            "9", "10", "11", "12", // answer index 9
            "Maha Pengasih, Maha Penyayang", "Tuhan semesta alam", "Raja di Hari Pembalasan", "Tunjukilah kami jalan yang lurus", // Question 7, answer index 24 -- ayat 3
            "Lurus", "Bengkok", "Gelap", "Terang", // answer index 28
            "13", "14", "15", "16", // answer index 10
            "Segala puji bagi Allah", "Tuhan semesta alam", "Dengan menyebut nama Allah", "Hanya Engkaulah yang kami sembah", // Question 10, answer index 26 -- ayat 4
            "111", "112", "113", "114", // answer index 8
            "18", "19", "21", "22", // answer index 11
            "Makkiyah", "Madaniyah", "Muqatta'ah", "Mutasyabihat", // Question 13, answer index 48 -- ayat 5
            "Memohon petunjuk dan rahmat dari Allah", "Mendapat perlindungan dari Allah", "Mendapat rezeki berlimpah", "Memperoleh kekuatan fisik", // answer index 52
            "24", "26", "27", "28", // answer index 5
            "Jalan orang-orang yang Engkau beri nikmat", "Jalan yang lurus", "Jalan orang-orang yang sesat", "Jalan yang gelap", // Question 16, answer index 27 -- ayat 6
            "29", "30", "31", "32", // answer index 6
            "103", "101", "102", "104", // answer index 6
            "Tujuh ayat yang diulang-ulang", "Orang-orang yang sesat", "Pertolongan", "Syafaat", // Question 19, answer index 72 -- ayat 7
            "Mengandung inti sari ajaran Al-Qur'an", "Merupakan surah pertama dalam Al-Qur'an", "Memiliki tujuh ayat", "Diturunkan di Makkah", // answer index 76
            "105", "106", "107", "108", // answer index 4
        });
        optionsType_1.Add(112, new string[] { 
            "Keikhlasan", "Malam", "Pengampunan", "Yang Maha Pengasih", // Question 1, answer index 0 -- ayat 1
            "4", "3", "5", "6", // Question 2, answer index 4
            "1", "2", "7", "8", // Question 3, answer index 8
            "Tauhid", "Zakat", "Shalat", "Puasa", // Question 4, answer index 12 -- ayat 2
            "30", "27", "28", "29", // Question 5, answer index 16
            "9", "10", "11", "12", // Question 6, answer index 9
            "Keesaan Allah", "Atribut belas kasih Allah", "Pentingnya silathurami", "Pentingnya shalat", // Question 7, answer index 24 -- ayat 3
            "Ketulusan", "Kunci", "Kriteria", "Cahaya", // Question 8, answer index 28
            "13", "14", "15", "16", // Question 9, answer index 5
            "Muhammad", "Ibrahim", "Isa", "Musa", // Question 10, answer index 36 -- ayat 4
            "Politesime", "Ateisme", "Agnotisisme", "Pantheisme", // Question 11, answer index 40
            "17", "18", "19", "20", // Question 12, answer index 4
        });
        optionsType_1.Add(113, new string[] { 
            "Subuh", "Cahaya", "Kegelapan", "Malam", // Question 1, answer index 0 -- ayat 1
            "5", "3", "4", "6", // answer index 4
            "1", "2", "7", "8", // answer index 8
            "Perlindungan dari Allah", "Keadilan", "Kebijaksanaan", "Kesabaran", // Question 4, answer index 12 -- ayat 2
            "113", "112", "114", "115", // answer index 16
            "9", "10", "11", "12", // answer index 9
            "Mendapat perlindungan dari Allah", "Memperoleh kekayaan", "Mendapat ketenaran", "Mendapat ilmu pengetahuan", // Question 7, answer index 24 -- ayat 3
            "Malam yang gelap gulita", "Matahari terbit", "Fajar menyingsing", "Angin bertiup kencang", // answer index 28
            "13", "14", "15", "16", // answer index 5
            "Muawwidzatain", "Makkiyah", "Madaniyah", "Thiwal", // Question 10, answer index 36 -- ayat 4
            "17", "19", "19", "20", // answer index 6
            "29", "30", "31", "32", // answer index 6
            "Gelap gulita", "Terbit fajar", "Hujan turun", "Bulan terbit", // Question 13, answer index 48 -- ayat 5
            "Hasad ketika dengki", "Fitnah ketika siang", "Penghianatan ketika malam", "Sihir ketika subuh", // answer index 52
            "100", "101", "102", "103", // answer index 4
        });
        optionsType_1.Add(114, new string[] { 
            "Manusia", "Jin", "Malaikat", "Alam semesta", // Question 1, answer index 0 -- ayat 1
            "6", "3", "5", "4", // answer index 4
            "1", "2", "7", "8", // answer index 8
            "Perlindungan dari bisikan setan", "Tauhid", "Keadilan", "Kebijaksanaan", // Question 4, answer index 12 -- ayat 2
            "114", "112", "113", "115", // answer index 16
            "9", "10", "11", "12", // answer index 9
            "Kejahatan bisikan setan", "Kejahatan malam", "Kejahatan sihir", "Kejahatan manusia", // Question 7, answer index 24 -- ayat 3
            "Iblis dan setan", "Manusia", "Malaikat", "Jin dan manusia", // answer index 28
            "13", "14", "15", "16", // answer index 5
            "Madinah", "Makkah", "Thaif", "Yaman", // Question 10, answer index 36 -- ayat 4
            "Bisikan jahat yang bersembunyi", "Kejahatan sihir", "Malam yang gelap gulita", "Hasad ketika dengki", // answer index 40
            "17", "18", "19", "20", // answer index 7
            "Muawwidzatain", "Makkiyah", "Madaniyah", "Thiwal", // Question 13, answer index 48 -- ayat 5
            "Allah SWT", "Muhammad SAW", "Malaikat Jibril", "Jin", // answer index 52
            "100", "101", "102", "103", // answer index 6
            "28", "29", "30", "31", // Question 16, answer index 6 -- ayat 6
            "Mendapat perlindungan dari Allah", "Memperoleh kekayaan", "Mendapat ketenaran", "Mendapat ilmu pengetahuan", // answer index 64
            "52", "51", "52", "53", // answer index 4
        });





        // ~~~~~~~~~~~~~~~~~~~~~~ Type 2 Questions ~~~~~~~~~~~~~~~~~~~~~~
        // Populate the question database with sample questions and options
        questionsType_2.Add(1, new List<Question>{
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/1_1", 0),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/1_1", 4),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/1_2", 1),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/1_2", 5),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/1_3", 2),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/1_3",12),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/1_4", 3),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/1_4", 20),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/1_5", 8),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/1_5", 28),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/1_6", 9),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/1_6", 29),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/1_7", 10),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/1_7", 36),
        });
        questionsType_2.Add(112, new List<Question>{
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/112_1", 0),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/112_1", 4),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/112_2", 1),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/112_2", 7),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/112_3", 2),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/112_3", 20),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/112_4", 3),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/112_4", 21),
        });
        questionsType_2.Add(113, new List<Question>{
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/113_1", 0),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/113_1", 4),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/113_2", 1),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/113_2", 7),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/113_3", 2),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/113_3", 15),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/113_4", 3),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/113_4", 20),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/113_5", 8),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/113_5", 28),
        });
        questionsType_2.Add(114, new List<Question>{
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/114_1", 0),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/114_1", 5),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/114_2", 1),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/114_2", 12),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/114_3", 2),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/114_3", 21),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/114_4", 3),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/114_4", 30),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/114_5", 8),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/114_5", 36),
            new Question("Berikut ini merupakan bunyi ayat?", "Audios/114_6", 9),
            new Question("Bagaimana cara membaca bunyi berikut ini?", "Audios/114_6", 37),
        });

        // Set shared options
        optionsType_2.Add(1, new string[] { 
            "1", "2", "3", "4", // Question 1, answer index 0 -- ayat 1
            "بِسْمِ اللّٰهِ الرَّحْمٰنِ الرَّحِيْمِ", "اَلْحَمْدُ لِلّٰهِ رَبِّ الْعٰلَمِيْنَۙ", "قُلْ هُوَ اللّٰهُ اَحَدٌۚ", "اِلٰهِ النَّاسِۙ", // answer index 4
            "5", "6", "7", "8", // Question 3, answer index 1 -- ayat 2
            "الرَّحْمٰنِ الرَّحِيْمِۙ", "اَللّٰهُ الصَّمَدُۚ", "مَلِكِ النَّاسِۙ", "الَّذِيْ جَمَعَ مَالًا وَّعَدَّدَهٗۙ", // answer index 5
            "28", "29", "30", "31", // Question 5, answer index 2 -- ayat 3
            "مٰلِكِ يَوْمِ الدِّيْنِۗ", "قُلْ اَعُوْذُ بِرَبِّ النَّاسِۙ", "مِنْ شَرِّ الْوَسْوَاسِ ەۙ الْخَنَّاسِۖ", "مِنْ شَرِّ مَا خَلَقَۙ", // answer index 12
            "15", "20", "25", "10", // Question 7, answer index 3 -- ayat 4
            "اِيَّاكَ نَعْبُدُ وَاِيَّاكَ نَسْتَعِيْنُۗ", "اِهْدِنَا الصِّرَاطَ الْمُسْتَقِيْمَۙ", "لَمْ يَلِدْ وَلَمْ يُوْلَدْۙ", "وَلَمْ يَكُنْ لَّهٗ كُفُوًا اَحَدٌࣖ", // answer index 20
            "111", "112", "113", "114", // Question 9, answer index 8 -- ayat 5
            "صِرَاطَ الَّذِيْنَ اَنْعَمْتَ عَلَيْهِمْ...ەۙ", "قُلْ اَعُوْذُ بِرَبِّ الْفَلَقِۙ", "وَمِنْ شَرِّ حَاسِدٍ اِذَا حَسَدَࣖ", "قُلْ يٰٓاَيُّهَا الْكٰفِرُوْنَۙ", // answer index 28
            "9", "11", "13", "17", // Question 11, answer index 9 -- ayat 6
            "الَّذِيْ يُوَسْوِسُ فِيْ صُدُوْرِ النَّاسِۙ", "وَمِنْ شَرِّ النَّفّٰثٰتِ فِى الْعُقَدِۙ", "لِاِيْلٰفِ قُرَيْشٍۙ", "الَّذِيْنَ هُمْ يُرَاۤءُوْنَۙ", // answer index 29
            "100", "101", "102", "103", // Question 13, answer index 10 -- ayat 7
            "مِنَ الْجِنَّةِ وَالنَّاسِࣖ", "وَمِنْ شَرِّ غَاسِقٍ اِذَا وَقَبَۙ", "لَآ اَعْبُدُ مَا تَعْبُدُوْنَۙ", "فَوَيْلٌ لِّلْsمُصَلِّيْنَۙ", // answer index 36
        });
        optionsType_2.Add(112, new string[] { 
            "1", "2", "3", "4", // Question 1, answer index 0 -- ayat 1
            "قُلْ هُوَ ٱللَّهُ أَحَدٌ", "بِسْمِ ٱللَّهِ", "ٱلرَّحْمَـٰنِ ٱلرَّحِيمِ", "ٱللَّهُ ٱلصَّمَدُ", // answer index 4
            "5", "6", "7", "8", // Question 3, answer index 1 -- ayat 2
            "قُلْ أَعُوذُ بِرَبِّ ٱلنَّاسِ", "مَلِكِ ٱلنَّاسِ", "مَـٰلِكِ يَوْمِ ٱلدِّينِ", "لَكُمْ دِينُكُمْ وَلِىَ دِينِ", // answer index 7
            "28", "29", "30", "31", // Question 5, answer index 2 -- ayat 3
            "لَمْ يَلِدْ وَلَمْ يُولَدْ", "وَلَمْ يَكُن لَّهُۥ كُفُوًا أَحَدٌۢ", "قُلْ أَعُوذُ بِرَبِّ ٱلْفَلَقِ", "مِن شَرِّ مَا خَلَقَ", // answer index 20
            "15", "20", "25", "10", // Question 7, answer index 3 -- ayat 4
            "وَمِن شَرِّ غَاسِقٍ إِذَا وَقَبَ", "وَٱلْعَصْرِ", "إِنَّ ٱلْإِنسَـٰنَ لَفِى خُسْرٍ", "لِإِيلَـٰفِ قُرَيْشٍ", // answer index 21
        });
        optionsType_2.Add(113, new string[] { 
            "1", "2", "3", "4", // Question 1, answer index 0 -- ayat 1
            "قُلْ اَعُوْذُ بِرَبِّ الْفَلَقِۙ", "قُلْ اَعُوْذُ بِرَبِّ النَّاسِۙ", "قُلْ هُوَ اللّٰهُ اَحَدٌۚ", "مِنْ شَرِّ مَا خَلَقَۙ", // answer index 4
            "5", "6", "7", "8", // Question 3, answer index 1 -- ayat 2
            "مَلِكِ النَّاسِۙ", "اَللّٰهُ الصَّمَدُۚ", "قُلْ يٰٓاَيُّهَا الْكٰفِرُوْنَۙ", "وَمِنْ شَرِّ غَاسِقٍ اِذَا وَقَبَۙ", // answer index 7
            "28", "29", "30", "31", // Question 5, answer index 2 -- ayat 3
            "وَمِنْ شَرِّ النَّفّٰثٰتِ فِى الْعُقَدِۙ", "اِلٰهِ النَّاسِۙ", "لَمْ يَلِدْ وَلَمْ يُوْلَدْۙ", "الرَّحْمٰنِ الرَّحِيْمِۙ", // answer index 15
            "15", "20", "25", "10", // Question 7, answer index 3 -- ayat 4
            "وَمِنْ شَرِّ حَاسِدٍ اِذَا حَسَدَࣖ", "وَٱلْعَصْرِ", "مِنْ شَرِّ الْوَسْوَاسِ ەۙ الْخَنَّاسِۖ", "وَلَمْ يَكُنْ لَّهٗ كُفُوًا اَحَدٌࣖ", // answer index 20
            "111", "112", "113", "114", // Question 9, answer index 8 -- ayat 5
            "الَّذِيْ يُوَسْوِسُ فِيْ صُدُوْرِ النَّاسِۙ", "مِنَ الْجِنَّةِ وَالنَّاسِࣖ", "مٰلِكِ يَوْمِ الدِّيْنِۗ", "بِسْمِ اللّٰهِ الرَّحْمٰنِ الرَّحِيْمِ", // answer index 28
        });
        optionsType_2.Add(114, new string[] { 
            "1", "2", "3", "4", // Question 1, answer index 0 -- ayat 1
            "قُلْ اَعُوْذُ بِرَبِّ الْفَلَقِۙ", "قُلْ اَعُوْذُ بِرَبِّ النَّاسِۙ", "قُلْ هُوَ اللّٰهُ اَحَدٌۚ", "مِنْ شَرِّ مَا خَلَقَۙ", // answer index 5
            "5", "6", "7", "8", // Question 3, answer index 1 -- ayat 2
            "مَلِكِ النَّاسِۙ", "اَللّٰهُ الصَّمَدُۚ", "قُلْ يٰٓاَيُّهَا الْكٰفِرُوْنَۙ", "وَمِنْ شَرِّ غَاسِقٍ اِذَا وَقَبَۙ", // answer index 12
            "28", "29", "30", "31", // Question 5, answer index 2 -- ayat 3
            "وَمِنْ شَرِّ النَّفّٰثٰتِ فِى الْعُقَدِۙ", "اِلٰهِ النَّاسِۙ", "لَمْ يَلِدْ وَلَمْ يُوْلَدْۙ", "الرَّحْمٰنِ الرَّحِيْمِۙ", // answer index 21
            "15", "20", "25", "10", // Question 7, answer index 3 -- ayat 4
            "وَمِنْ شَرِّ حَاسِدٍ اِذَا حَسَدَࣖ", "وَٱلْعَصْرِ", "مِنْ شَرِّ الْوَسْوَاسِ ەۙ الْخَنَّاسِۖ", "وَلَمْ يَكُنْ لَّهٗ كُفُوًا اَحَدٌࣖ", // answer index 30
            "111", "112", "113", "114", // Question 9, answer index 8 -- ayat 5
            "الَّذِيْ يُوَسْوِسُ فِيْ صُدُوْرِ النَّاسِۙ", "مِنَ الْجِنَّةِ وَالنَّاسِࣖ", "مٰلِكِ يَوْمِ الدِّيْنِۗ", "بِسْمِ اللّٰهِ الرَّحْمٰنِ الرَّحِيْمِ", // answer index 36
            "9", "11", "13", "17", // Question 11, answer index 9 -- ayat 6
            "اَلْحَمْدُ لِلّٰهِ رَبِّ الْعٰلَمِيْنَۙ", "اِهْدِنَا الصِّرَاطَ الْمُسْتَقِيْمَۙ", "لِاِيْلٰفِ قُرَيْشٍۙ", "فَوَيْلٌ لِّلْمُصَلِّيْنَۙ", // answer index 37
        });





        // ~~~~~~~~~~~~~~~~~~~~~~ Type 3 Questions (Matching) ~~~~~~~~~~~~~~~~~~~~~~
        // Questions
        questionsType_3.Add(1, new Dictionary<int, string>{
            { 0, "Ayat 1" },
            { 1, "Arti" },
            { 2, "Ayat 2" },
            { 3, "Kelompok Surah" },
            { 4, "Ayat 3" },
            { 5, "Sering Disebut" },
            { 6, "Ayat 4" },
            { 7, "Surah Ke-" },
            { 8, "Ayat 5" },
            { 9, "Jumlah Ayat" },
            { 10, "Ayat 6" },
            { 11, "Diturunkan di" },
            { 12, "Ayat 7" },
            { 13, "Jumlah Dibaca Sehari Shalat" },
        });
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
        questionsType_3.Add(113, new Dictionary<int, string>{
            { 0, "Ayat 1" },
            { 1, "Arti" },
            { 2, "Ayat 2" },
            { 3, "Kelompok Surah" },
            { 4, "Ayat 3" },
            { 5, "Tema Utama" },
            { 6, "Ayat 4" },
            { 7, "Surah Ke-" },
            { 8, "Ayat 5" },
            { 9, "Jumlah Ayat" },
        });
        questionsType_3.Add(114, new Dictionary<int, string>{
            { 0, "Ayat 1" },
            { 1, "Arti" },
            { 2, "Ayat 2" },
            { 3, "Kelompok Surah" },
            { 4, "Ayat 3" },
            { 5, "Tema Utama" },
            { 6, "Ayat 4" },
            { 7, "Surah Ke-" },
            { 8, "Ayat 5" },
            { 9, "Jumlah Ayat" },
            { 10, "Ayat 6" },
            { 11, "Diturunkan di" },
        });

        // Options
        optionsType_3.Add(1, new List<string> {
            { "بِسْمِ اللّٰهِ الرَّحْمٰنِ الرَّحِيْمِ" } ,
            { "Pembuka" },
            { "اَلْحَمْدُ لِلّٰهِ رَبِّ الْعٰلَمِيْنَۙ" } ,
            { "Makkiyah" },
            { "الرَّحْمٰنِ الرَّحِيْمِۙ" } ,
            { "Ummul Kitab" },
            { "مٰلِكِ يَوْمِ الدِّيْنِۗ" } ,
            {  "1" },
            { "اِيَّاكَ نَعْبُدُ وَاِيَّاكَ نَسْتَعِيْنُۗ" } ,
            {  "7" },
            { "اِهْدِنَا الصِّرَاطَ الْمُسْتَقِيْمَۙ" } ,
            {  "Makkah" },
            { "صِرَاطَ الَّذِيْنَ اَنْعَمْتَ عَلَيْهِمْ" } ,
            {  "17" },
        });
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
        optionsType_3.Add(113, new List<string> {
            { "قُلْ اَعُوْذُ بِرَبِّ الْفَلَقِۙ" } ,
            { "Subuh" },
            { "مِنْ شَرِّ مَا خَلَقَۙ" } ,
            { "Muawwidzatain" },
            { "وَمِنْ شَرِّ غَاسِقٍ اِذَا وَقَبَۙ" } ,
            { "Perlindungan dari Allah" },
            { "وَمِنْ شَرِّ النَّفّٰثٰتِ فِى الْعُقَدِۙ" } ,
            {  "113" },
            { "وَمِنْ شَرِّ حَاسِدٍ اِذَا حَسَدَࣖ" } ,
            {  "5" },
        });
        optionsType_3.Add(114, new List<string> {
            { "قُلْ اَعُوْذُ بِرَبِّ النَّاسِۙ" } ,
            { "Manusia" },
            { "مَلِكِ النَّاسِۙ" } ,
            { "Muawwidzatain" },
            { "اِلٰهِ النَّاسِۙ" } ,
            { "Perlindungan Bisikan Setan" },
            { "مِنْ شَرِّ الْوَسْوَاسِ ەۙ الْخَنَّاسِۖ" } ,
            {  "114" },
            { "الَّذِيْ يُوَسْوِسُ فِيْ صُدُوْرِ النَّاسِۙ" } ,
            {  "6" },
            { "مِنَ الْجِنَّةِ وَالنَّاسِࣖ" } ,
            {  "Madinah" },
        });




        // ~~~~~~~~~~~~~~~~~~~~~~ Type 4 Questions (Continue) ~~~~~~~~~~~~~~~~~~~~~~
        // Questions
        questionsType_4.Add(1, new List<int[]>{
            new int[] { 0, 1, 2, 3 }, // ayat 1
            new int[] { 4, 5, 6 }, // ayat 2
            new int[] { 7, 8 }, // ayat 3
            new int[] { 9, 10, 11 }, // ayat 4
            new int[] { 12, 13, 14, 15 }, // ayat 5
            new int[] { 16, 17, 18 }, // ayat 6
            new int[] { 19, 20, 21, 22, 23, 24, 25, 26 }, // ayat 7
        });
        questionsType_4.Add(112, new List<int[]>{
            new int[] { 0, 1, 2 }, // ayat 1
            new int[] { 3, 4 }, // ayat 2
            new int[] { 5, 6, 7, 8 }, // ayat 3
            new int[] { 9, 10 }, // ayat 4
        });
        questionsType_4.Add(113, new List<int[]>{
            new int[] { 0, 1, 2 }, // ayat 1
            new int[] { 3, 4 }, // ayat 2
            new int[] { 5, 6 }, // ayat 3
            new int[] { 7, 8 }, // ayat 4
            new int[] { 9, 10 }, // ayat 5
        });
        questionsType_4.Add(114, new List<int[]>{
            new int[] { 0, 1, 2 }, // ayat 1
            new int[] { 3, 4 }, // ayat 2
            new int[] { 5, 6 }, // ayat 3
            new int[] { 7, 8, 9 }, // ayat 4
            new int[] { 10, 11, 12, 13 }, // ayat 5
            new int[] { 14, 15 }, // ayat 6
        });

        // Options
        optionsType_4.Add(1, new List<string> {
            "بِسْمِ", "اللّٰهِ", "الرَّحْمٰنِ", "الرَّحِيْمِ", // ayat 1 (0, 1, 2, 3)
            "اَلْحَمْدُ", "لِلّٰهِ رَبِّ", "الْعٰلَمِيْنَۙ", // ayat 2 (4, 5, 6)
            "الرَّحْمٰنِ", "الرَّحِيْمِۙ", // ayat 3 (7, 8)
            "مٰلِكِ", "يَوْمِ", "الدِّيْنِۗ", // ayat 4 (9, 10, 11)
            "اِيَّا", "كَ نَعْبُدُ", "وَاِيَّا", "كَ نَسْتَعِيْنُۗ", // ayat 5 (12, 13, 14, 15)
            "اِهْدِنَا", "الصِّرَاطَ", "الْمُسْتَقِيْمَۙ", // ayat 6 (16, 17, 18)
            "صِرَاطَ", "الَّذِيْنَ", "اَنْعَمْتَ", "عَلَيْهِمْ ەۙ", "غَيْرِ", "الْمَغْضُوْبِ", "عَلَيْهِمْ وَلَا", "الضَّاۤلِّيْنَࣖ", // ayat 7 (19, 20, 21, 22, 23, 24, 25, 26)
        });
        optionsType_4.Add(112, new List<string> {
            "قُلْ", "هُوَ ٱللَّهُ", "أَحَدٌ", // ayat 1 (0, 1, 2)
            "ٱللَّهُ", "ٱلصَّمَدُ", // ayat 2 (3, 4)
            "لَمْ", "يَلِدْ", "وَلَمْ", "يُولَدْ", // ayat 3 (5, 6, 7, 8)
            "وَلَمْ يَكُن لَّهُۥ", "كُفُوًا اَحَدٌࣖ", // ayat 4 (9, 10)
        });
        optionsType_4.Add(113, new List<string> {
            "قُلْ", "اَعُوْذُ", "بِرَبِّ الْفَلَقِۙ", // ayat 1 (0, 1, 2)
            "مِنْ شَرِّ مَا", "خَلَقَۙ", // ayat 2 (3, 4)
            "وَمِنْ شَرِّ غَاسِقٍ", "اِذَا وَقَبَۙ", // ayat 3 (5, 6)
            "وَمِنْ شَرِّ النَّفّٰثٰتِ", "فِى الْعُقَدِۙ", // ayat 4 (7, 8)
            "وَمِنْ شَرِّ حَاسِدٍ", "اِذَا حَسَدَࣖ", // ayat 5 (9, 10)
        });
        optionsType_4.Add(114, new List<string> {
            "قُلْ", "اَعُوْذُ", "بِرَبِّ النَّاسِۙ", // ayat 1 (0, 1, 2)
            "مَلِكِ", "النَّاسِۙ", // ayat 2 (3, 4)
            "اِ", "لٰهِ النَّاسِۙ", // ayat 3 (5, 6)
            "مِنْ شَرِّ", "الْوَسْوَاسِ ەۙ", "الْخَنَّاسِۖ", // ayat 4 (7, 8, 9)
            "الَّذِيْ", "يُوَسْوِسُ فِيْ", "صُدُوْ", "رِ النَّاسِۙ", // ayat 5 (10, 11, 12, 13)
            "مِنَ الْجِنَّةِ", "وَالنَّاسِࣖ" // ayat 6 (14, 15)
        });




        // ~~~~~~~~~~~~~~~~~~~~~~ Type 5 Questions (Vocal) ~~~~~~~~~~~~~~~~~~~~~~
        // Questions
        questionsType_5.Add(1, new List<string>{
            "بِسْمِ اللّٰهِ الرَّحْمٰنِ الرَّحِيْمِ", // ayat 1
            "اَلْحَمْدُ لِلّٰهِ رَبِّ الْعٰلَمِيْنَۙ", // ayat 2
            "الرَّحْمٰنِ الرَّحِيْمِۙ", // ayat 3
            "مٰلِكِ يَوْمِ الدِّيْنِۗ", // ayat 4
            "اِيَّاكَ نَعْبُدُ وَاِيَّاكَ نَسْتَعِيْنُۗ", // ayat 5
            "اِهْدِنَا الصِّرَاطَ الْمُسْتَقِيْمَۙ", // ayat 6
            "صِرَاطَ الَّذِيْنَ اَنْعَمْتَ عَلَيْهِمْ ەۙ غَيْرِ الْمَغْضُوْبِ عَلَيْهِمْ وَلَا الضَّاۤلِّيْنَࣖ", // ayat 7
        });
        questionsType_5.Add(112, new List<string>{
            "قُلْ هُوَ اللّٰهُ اَحَدٌۚ", // ayat 1
            "اَللّٰهُ الصَّمَدُۚ", // ayat 2
            "لَمْ يَلِدْ وَلَمْ يُوْلَدْۙ", // ayat 3
            "وَلَمْ يَكُنْ لَّهٗ كُفُوًا اَحَدٌࣖ", // ayat 4
        });
        questionsType_5.Add(113, new List<string>{
            "قُلْ اَعُوْذُ بِرَبِّ الْفَلَقِۙ", // ayat 1
            "مِنْ شَرِّ مَا خَلَقَۙ", // ayat 2
            "وَمِنْ شَرِّ غَاسِقٍ اِذَا وَقَبَۙ", // ayat 3
            "وَمِنْ شَرِّ النَّفّٰثٰتِ فِى الْعُقَدِۙ", // ayat 4
            "وَمِنْ شَرِّ حَاسِدٍ اِذَا حَسَدَࣖ", // ayat 5
        });
        questionsType_5.Add(114, new List<string>{
            "قُلْ اَعُوْذُ بِرَبِّ النَّاسِۙ", // ayat 1
            "مَلِكِ النَّاسِۙ", // ayat 2
            "اِلٰهِ النَّاسِۙ", // ayat 3
            "مِنْ شَرِّ الْوَسْوَاسِ ەۙ الْخَنَّاسِۖ", // ayat 4
            "الَّذِيْ يُوَسْوِسُ فِيْ صُدُوْرِ النَّاسِۙ", // ayat 5
            "مِنَ الْجِنَّةِ وَالنَّاسِࣖ", // ayat 6
        });

        // Options
        optionsType_5.Add(1, new List<string> {
            "Audios/1_1", // ayat 1
            "Audios/1_2", // ayat 2
            "Audios/1_3", // ayat 3
            "Audios/1_4", // ayat 4
            "Audios/1_5", // ayat 5
            "Audios/1_6", // ayat 6
            "Audios/1_7", // ayat 7
        });
        optionsType_5.Add(112, new List<string> {
            "Audios/112_1", // ayat 1
            "Audios/112_2", // ayat 2
            "Audios/112_3", // ayat 3
            "Audios/112_4", // ayat 4
        });
        optionsType_5.Add(113, new List<string> {
            "Audios/113_1", // ayat 1
            "Audios/113_2", // ayat 2
            "Audios/113_3", // ayat 3
            "Audios/113_4", // ayat 4
            "Audios/113_5", // ayat 5
        });
        optionsType_5.Add(114, new List<string> {
            "Audios/114_1", // ayat 1
            "Audios/114_2", // ayat 2
            "Audios/114_3", // ayat 3
            "Audios/114_4", // ayat 4
            "Audios/114_5", // ayat 5
            "Audios/114_6", // ayat 6
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

