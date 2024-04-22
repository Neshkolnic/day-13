using System;
using System.IO;
using System.Windows.Forms;

namespace день_13
{
    public partial class Form1 : Form
    {
        string filepath = "C:\\Users\\Admin\\Desktop\\Практика КПИЯП\\Приложения по дням\\13 день\\questions.txt";
        private string[] questions; // Массив для хранения вопросов
        private int currentQuestionIndex = 0; // Индекс текущего вопроса
        private int chlen = 0; // Индекс текущего вопроса
        private int radio_index = 1; // Индекс radiobuttons
        byte[] correctAnswers = new byte[] { 2, 1, 2, 0, 2, 0, 1, 0, 1, 0 };
        private int score = 0; // Переменная для хранения количества правильных ответов
        private Timer timer; // Таймер
        private int remainingTime = 10; // Оставшееся время в секундах

        public Form1()
        {
            InitializeComponent();
            questions = File.ReadAllLines(filepath);
            DisplayCurrentQuestion(); // Отображение первого вопроса
            DisplayRadio();
            StartTimer(); // Запуск таймера
        }

        private void StartTimer()
        {
            timer = new Timer();
            timer.Interval = 1000; // Интервал таймера - 1 секунда
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingTime--;
            if (remainingTime <= 0)
            {
                // По истечении времени останавливаем таймер и подсчитываем баллы
                timer.Stop();
                MessageBox.Show($"Время вышло! Вы набрали {score} баллов.");
                // Блокировка кнопки "Next"
                label1.Text = score.ToString();
                label3.Text = "Время вышло!!!!!!!";
                Nextbutton.Enabled = false;
            }
            else
            {
                // Обновление текста лейбла с оставшимся временем
                label3.Text = $"Оставшееся время: {remainingTime} сек.";
            }
        }

        private void DisplayCurrentQuestion()
        {
            if (currentQuestionIndex < 10)
            {
                // Отображение текущего вопроса и его номера
                textBox1.Text = $"Вопрос {currentQuestionIndex + 1}:\r\n{questions[chlen]}";
                chlen += 6;
            }
            else
            {
                // Все вопросы закончились, выводим сообщение
                textBox1.Text = "Тест завершен!";
                label1.Text = score.ToString();
            }
        }

        private int CheckAnswer(byte correctAnswer, byte selectedAnswer)
        {
            if (correctAnswer == selectedAnswer)
            {
                return 1; // Возвращаем 1 балл за правильный ответ
            }
            else
            {
                return 0; // Возвращаем 0 баллов за неправильный ответ
            }
        }

        private void DisplayRadio()
        {
            if (radio_index < questions.Length)
            {
                radioButton1.Text = $"{questions[radio_index]}";
                radioButton2.Text = $"{questions[radio_index + 1]}";
                radioButton3.Text = $"{questions[radio_index + 2]}";
                radioButton4.Text = $"{questions[radio_index + 3]}";
                radio_index += 6;
            }
            else
            {
                radioButton1.Text = "";
                radioButton2.Text = "";
                radioButton3.Text = "";
                radioButton4.Text = "";
            }
        }

        private void Nextbutton_Click(object sender, EventArgs e)
        {
            byte selectedAnswer;
            if (radioButton1.Checked)
            {
                selectedAnswer = 0;
            }
            else if (radioButton2.Checked)
            {
                selectedAnswer = 1;
            }
            else if (radioButton3.Checked)
            {
                selectedAnswer = 2;
            }
            else if (radioButton4.Checked)
            {
                selectedAnswer = 3;
            }
            else
            {
                // Ни один ответ не выбран
                MessageBox.Show("Пожалуйста, выберите ответ");
                return;
            }
            int scoreForQuestion = 0;
            if (currentQuestionIndex < 10)
            {
                scoreForQuestion = CheckAnswer(correctAnswers[currentQuestionIndex], selectedAnswer);
                score += scoreForQuestion;
            }

            // Выводим сообщение о правильности ответа
            if (scoreForQuestion == 1)
            {
                MessageBox.Show("Правильно!");
            }
            else
            {
                MessageBox.Show("Неправильно!");
            }

            currentQuestionIndex++;
            DisplayCurrentQuestion();
            DisplayRadio();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
