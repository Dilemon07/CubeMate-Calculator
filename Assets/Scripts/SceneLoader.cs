using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Переключение на сцену "Погода"
    public void LoadWeatherScene()
    {
        SceneManager.LoadScene("WeatherScene");
    }

    // Возвращение на главную сцену
    public void LoadCalculatorScene()
    {
        SceneManager.LoadScene("Main");
    }
}
