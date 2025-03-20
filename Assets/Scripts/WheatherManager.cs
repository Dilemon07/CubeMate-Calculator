using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using System;

public class WeatherManager : MonoBehaviour
{
    public string apiKey = "adbf308b3ae55cf08220533f720f4ddd";
    private string apiUrl = "https://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&appid={1}";

    public TMP_Dropdown cityDropdown; // Выбор города
    public TextMeshProUGUI cityText;  // Текст для города
    public TextMeshProUGUI temperatureText;  // Температура
    public Image weatherIcon; // Иконка погоды

    // Иконки погоды
    public Sprite clearSky, fewClouds, scatteredClouds, brokenClouds, rain, thunderstorm, snow, mist;

    private string selectedCity = "Moscow"; // Город по умолчанию

    void Start()
    {
        cityDropdown.onValueChanged.AddListener(delegate { UpdateCity(); });
        StartCoroutine(GetWeatherData(selectedCity));
    }

    // 📌 Обновляем город при изменении в `Dropdown`
    public void UpdateCity()
    {
        selectedCity = cityDropdown.options[cityDropdown.value].text;
        StartCoroutine(GetWeatherData(selectedCity));
    }

    // 📌 Получаем данные о погоде
    IEnumerator GetWeatherData(string city)
    {
        string url = string.Format(apiUrl, city, apiKey);
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                ParseWeatherData(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Ошибка загрузки погоды: " + request.error);
            }
        }
    }

    // 📌 Разбираем JSON-ответ от API
    void ParseWeatherData(string json)
    {
        WeatherInfo weatherInfo = JsonUtility.FromJson<WeatherInfo>(json);

        cityText.text = weatherInfo.name;
        temperatureText.text = Mathf.Round(weatherInfo.main.temp) + "°C";

        string weatherCondition = weatherInfo.weather[0].main.ToLower();
        weatherIcon.sprite = GetWeatherIcon(weatherCondition);
    }

    // 📌 Устанавливаем иконку погоды
    Sprite GetWeatherIcon(string condition)
    {
        switch (condition)
        {
            case "clear": return clearSky;
            case "clouds": return fewClouds;
            case "rain": return rain;
            case "thunderstorm": return thunderstorm;
            case "snow": return snow;
            case "mist": return mist;
            default: return fewClouds;
        }
    }
}

// 📌 Структура данных для парсинга JSON
[System.Serializable]
public class WeatherInfo
{
    public string name;
    public WeatherMain main;
    public WeatherCondition[] weather;
}

[System.Serializable]
public class WeatherMain
{
    public float temp;
}

[System.Serializable]
public class WeatherCondition
{
    public string main;
}   


