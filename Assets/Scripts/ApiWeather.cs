using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;


public class ApiWeather : MonoBehaviour
{
    // Start is called before the first frame update
    private string _city = "";
    [SerializeField] private TextMeshProUGUI _cityError;
    [SerializeField] private TMP_InputField _SearchInput;
    [SerializeField] private TextMeshProUGUI _cityName;
    [SerializeField] private Image _weatherIcon;
    [SerializeField] private TextMeshProUGUI _weather;
    [SerializeField] private TextMeshProUGUI _temperature;


    void Start()
    {

        _SearchInput.onValueChanged.AddListener(delegate { onValueChanged(); });
    }

    public void onValueChanged()
    {

        //if enter is pressed


    }
    //update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SearchCity();
            _cityName.text = "City : " + _city;
        }
    }

    public void SearchCity()
    {
        _city = _SearchInput.text;
        StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?q=" + _city + "&APPID=fb7ea3ac85bb67a9bdb0b4b9a51f1c72"));
    }

    [System.Serializable]
    public class Weather
    {
        public int id;
        public string main;
        public string description;
        public string icon;
    }

    [System.Serializable]
    public class Main
    {
        public float temp;
        public float feels_like;
        public float temp_min;
        public float temp_max;
        public int pressure;
        public int humidity;
    }

    [System.Serializable]
    public class WeatherData
    {
        public Weather[] weather;
        public Main main;
    }
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // On envoie la requête et on attend la réponse
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(webRequest.error);
                _cityError.text = "City not found";
            }
            else
            {
                _cityError.text = "";
                WeatherData weatherData = JsonUtility.FromJson<WeatherData>(webRequest.downloadHandler.text);
                _weather.text = "Weather : " + weatherData.weather[0].main;
                _weatherIcon.sprite = Resources.Load<Sprite>("WeatherIcons/" + weatherData.weather[0].icon);
                _temperature.text = "Temperature : " + (weatherData.main.temp - 273.15f).ToString("0.00") + "°C";


                Debug.Log("Received: " + webRequest.downloadHandler.text);
            }
        }
    }
}
