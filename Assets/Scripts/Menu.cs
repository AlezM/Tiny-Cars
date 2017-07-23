using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public int window;
    public bool ingame = false;
    void Start()
    {
        window = 1;
    }

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200));
        if (window == 1)
        {
            if (GUI.Button(new Rect(10, 30, 180, 30), "Играть"))
            {
                window = 2;
            }
            if (GUI.Button(new Rect(10, 70, 180, 30), "Настройки"))
            {
                window = 3;
            }
            if (GUI.Button(new Rect(10, 110, 180, 30), "Об Игре"))
            {
                window = 4;
            }
            if (GUI.Button(new Rect(10, 150, 180, 30), "Выход"))
            {
                window = 5;
            }
        }

        if (window == 2)
        {
            ingame = true;
            // Тут будет запуск остальных скриптов

        }

        if (window == 3)
        {
            GUI.Label(new Rect(50, 10, 180, 30), "Настройки Игры");
            if (GUI.Button(new Rect(10, 40, 180, 30), "Игра"))
            {
            }
            if (GUI.Button(new Rect(10, 80, 180, 30), "Аудио"))
            {
            }
            if (GUI.Button(new Rect(10, 120, 180, 30), "Видео"))
            {
            }
            if (GUI.Button(new Rect(10, 160, 180, 30), "Назад"))
            {
                window = 1;
            }
        }

        if (window == 4)
        {
            GUI.Label(new Rect(50, 10, 180, 30), "Об Игре");
            GUI.Label(new Rect(10, 40, 180, 40), "Информация об разработчике и об игре");
            if (GUI.Button(new Rect(10, 90, 180, 30), "Назад"))
            {
                window = 1;
            }
        }

        if (window == 5)
        {
            GUI.Label(new Rect(50, 10, 180, 30), "Вы уже выходите?");
            if (GUI.Button(new Rect(10, 40, 180, 30), "Да"))
            {
                Application.Quit();
            }
            if (GUI.Button(new Rect(10, 80, 180, 30), "Нет"))
            {
                window = 1;
            }
        }
        GUI.EndGroup();
    }
}
