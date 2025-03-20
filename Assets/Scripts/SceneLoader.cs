using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // ������������ �� ����� "������"
    public void LoadWeatherScene()
    {
        SceneManager.LoadScene("WeatherScene");
    }

    // ����������� �� ������� �����
    public void LoadCalculatorScene()
    {
        SceneManager.LoadScene("Main");
    }
}
