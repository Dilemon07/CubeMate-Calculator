using UnityEngine;

public class ExitHandler : MonoBehaviour
{
    public GameObject exitPanel; // Ссылка на окно подтверждения

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Если нажата кнопка "Назад"
        {
            if (!exitPanel.activeSelf) // Если панель выключена — включаем
            {
                exitPanel.SetActive(true);
            }
            else // Если уже открыта — скрываем
            {
                exitPanel.SetActive(false);
            }
        }
    }

    // Выход из игры
    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Закрытие окна без выхода
    public void CancelExit()
    {
        exitPanel.SetActive(false);
    }
}
