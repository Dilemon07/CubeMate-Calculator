using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class CalculatorDisplay : MonoBehaviour
{
    public TextMeshPro cubeText; // Основной текст на грани куба
    public TextMeshPro historyText; // Текст на левой грани для истории операций

    private string currentInput = "";
    private double lastResult = 0;
    private char lastOperator = '\0';
    private bool isNewInput = true;
    private List<string> operationHistory = new List<string>(); // Список для хранения истории операций

    // Добавление цифры
    public void AppendNumber(string number)
    {
        Debug.Log("Appending number: " + number);

        if (currentInput.Length < 15)
        {
            if (isNewInput)
            {
                currentInput = number;
                isNewInput = false;
            }
            else
            {
                currentInput += number;
            }

            UpdateDisplay();
        }
    }

    // Добавление оператора
    public void AppendOperator(string operation)
    {
        if (string.IsNullOrEmpty(operation))
        {
            Debug.LogError("Operation string is empty!");
            return;
        }

        // Проверка на случай, когда после оператора идет минус
        if (currentInput == "" && lastResult == 0)
        {
            // Если в начале, например, вводится - (для отрицательного числа), то продолжаем вводить
            currentInput = "-";
            isNewInput = false;
            return;
        }

        if (currentInput != "")
        {
            // Если минус был введён, то его надо учесть как знак отрицательного числа
            if (currentInput.StartsWith("-"))
            {
                currentInput = currentInput.Substring(1); // Убираем минус, чтобы правильно работать с числом
            }

            if (lastOperator != '\0') Calculate(); // Выполнение предыдущей операции

            lastResult = double.Parse(currentInput); // Сохраняем последнее значение
            currentInput = "";
        }

        lastOperator = operation[0];
        isNewInput = true;
        UpdateDisplay();
    }

    // Добавление точки
    public void AppendDecimal()
    {
        if (!currentInput.Contains("."))
        {
            if (currentInput == "" || isNewInput) currentInput = "0";
            currentInput += ".";
            UpdateDisplay();
        }
    }

    // Вычисление результата
    public void Calculate()
    {
        if (currentInput == "" || lastOperator == '\0') return;

        if (double.TryParse(currentInput, out double secondOperand))
        {
            // Сохраняем текущую операцию для истории
            string operationString = lastResult + " " + lastOperator + " " + secondOperand;

            // Проводим вычисления
            switch (lastOperator)
            {
                case '+': lastResult += secondOperand; break;
                case '-': lastResult -= secondOperand; break;
                case '*': lastResult *= secondOperand; break;
                case '/':
                    if (secondOperand != 0) lastResult /= secondOperand;
                    else { cubeText.text = "Ошибка"; ClearAll(); return; }
                    break;
                case '%': lastResult = (lastResult * secondOperand) / 100; break;
            }

            lastResult = Math.Round(lastResult, 5);
            currentInput = lastResult.ToString();

            // Добавляем операцию в историю
            operationHistory.Insert(0, operationString + " = " + lastResult); // Новая операция добавляется в начало списка
                
            // Если история превышает максимальное количество строк, удаляем самую старую операцию
            if (operationHistory.Count > 3) // Ограничение на 10 строк (можно изменить)
            {
                operationHistory.RemoveAt(operationHistory.Count - 1);
            }

            // Обновляем историю на левой грани куба
            UpdateHistory();

            lastOperator = '\0';
            isNewInput = true;
            UpdateDisplay();
        }
        else
        {
            cubeText.text = "Ошибка";
            ClearAll();
        }
    }

    // Удаление последнего символа
    public void DeleteLast()
    {
        if (currentInput.Length > 0)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
            UpdateDisplay();
        }
    }

    // Очистка всего (кроме истории)
    public void ClearAll()
    {
        currentInput = "";
        lastResult = 0;
        lastOperator = '\0';
        isNewInput = true;
        UpdateDisplay();
    }

    // Обновление отображения
    private void UpdateDisplay()
    {
        if (cubeText != null)
        {
            cubeText.text = string.IsNullOrEmpty(currentInput) ? "0" : currentInput;
            Debug.Log("Обновление CubeText: " + cubeText.text);
        }
        else
        {
            Debug.LogError("❌ CubeText не привязан в Inspector!");
        }
    }

    // Обновление истории операций на левой грани куба
    private void UpdateHistory()
    {
        if (historyText != null)
        {
            // Формируем многострочный текст для истории
            string historyString = "";
            foreach (var operation in operationHistory)
            {
                historyString += operation + "\n"; // Каждая операция на новой строке
            }

            // Обновляем текст на левой грани куба
            historyText.text = historyString;
        }
        else
        {
            Debug.LogError("❌ HistoryText не привязан в Inspector!");
        }
    }

    // Обработчик кнопки "-" для отрицательных чисел
    public void ToggleSign()
    {
        if (isNewInput)
        {
            currentInput = "-";
            isNewInput = false;
        }
        else
        {
            if (currentInput.StartsWith("-"))
            {
                currentInput = currentInput.Substring(1);
            }
            else
            {
                currentInput = "-" + currentInput;
            }
        }
        UpdateDisplay();
    }
}