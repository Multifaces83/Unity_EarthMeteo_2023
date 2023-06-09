using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class ApiWeather : MonoBehaviour
{

    private CursorPositionConversion _cursorPositionConversion;
    private string _city = "";
    private string _iconCode = "";
    private float _lat = 0;
    private float _lon = 0;

    [SerializeField] private Camera _camera;
    private Transform _cameraTransform;
    private float _speed = 2.0f;
    private float _maxAngle = 180.0f;
    [SerializeField] private GameObject _marker;
    private float _markerDistance = 2f;
    [SerializeField] private TextMeshProUGUI _cityError;
    [SerializeField] private TMP_InputField _SearchInput;
    [SerializeField] private TextMeshProUGUI _cityName;
    [SerializeField] private Image _weatherIcon;
    [SerializeField] private TextMeshProUGUI _weather;
    [SerializeField] private TextMeshProUGUI _temperature;
    [SerializeField] private TextMeshProUGUI _feelsLike;
    [SerializeField] private TextMeshProUGUI _temperatureMin;
    [SerializeField] private TextMeshProUGUI _temperatureMax;
    [SerializeField] private TextMeshProUGUI _humidity;
    [SerializeField] private TextMeshProUGUI _pressure;

    void Start()
    {
        _cameraTransform = _camera.transform;
        _weatherIcon.enabled = false;
        _cursorPositionConversion = GetComponent<CursorPositionConversion>();
        _SearchInput.onValueChanged.AddListener(delegate { onValueChanged(); });
    }

    public void onValueChanged()
    {
    }
    //update
    void Update()
    {
        SearchCity();
        //_cityName.text = "City : " + _city;
        SearchCityByPosition();

    }


    public void SearchCity()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //_marker.transform.position = new Vector3(_cursorPositionConversion.GetCursorPosition().x, _cursorPositionConversion.GetCursorPosition().y + _markerDistance, _cursorPositionConversion.GetCursorPosition().z);
            //_marker.transform.position = _cursorPositionConversion.GetCursorPosition();
            _city = _SearchInput.text;
            StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?q=" + _city + "&APPID=fb7ea3ac85bb67a9bdb0b4b9a51f1c72"));
        }
    }
    public void SearchCityByPosition()
    {
        //_city = _SearchInput.text;
        if (Input.GetMouseButtonDown(0))
        {
            //Calcul direction de la cible
            //Vector3 targetDirection = _cursorPositionConversion.GetCursorPosition() - _cameraTransform.position;
            //Calcul rotation necessaire pour faire face à la cible
            //_cameraTransform.rotation = Quaternion.RotateTowards(_cameraTransform.rotation, Quaternion.LookRotation(targetDirection), _maxAngle * Time.deltaTime);


            //_marker.transform.position = _cursorPositionConversion.GetCursorPosition();
            _marker.transform.position = new Vector3(_cursorPositionConversion.GetCursorPosition().x, _cursorPositionConversion.GetCursorPosition().y + _markerDistance, _cursorPositionConversion.GetCursorPosition().z);
            _lat = _cursorPositionConversion.GetLatitude();
            _lon = _cursorPositionConversion.GetLongitude();
            StartCoroutine(GetRequest("https://api.openweathermap.org/data/2.5/weather?lat=" + _lat + "&lon=" + _lon + "&appid=fb7ea3ac85bb67a9bdb0b4b9a51f1c72"));
        }
    }

    [System.Serializable]
    public class Coord
    {
        public float lon;
        public float lat;

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
    public class Sys
    {
        public string country;

    }

    [System.Serializable]
    public class WeatherData
    {
        public Coord coord;
        public Weather[] weather;
        public Main main;
        public Sys sys;
        public string name;
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
                _cityName.text = "City : " + weatherData.name + ", " + weatherData.sys.country;
                if (weatherData.name == "Globe")
                {
                    _cityError.text = "You are not on Earth";
                }
                _lon = weatherData.coord.lon;
                _lat = weatherData.coord.lat;
                _weather.text = "Weather : " + weatherData.weather[0].main;
                _iconCode = weatherData.weather[0].icon;
                _temperature.text = "Temperature : " + (weatherData.main.temp - 273.15f).ToString("0.00") + "°C";
                _feelsLike.text = "Feels like : " + (weatherData.main.feels_like - 273.15f).ToString("0.00") + "°C";
                _temperatureMin.text = "Temperature min : " + (weatherData.main.temp_min - 273.15f).ToString("0.00") + "°C";
                _temperatureMax.text = "Temperature max : " + (weatherData.main.temp_max - 273.15f).ToString("0.00") + "°C";
                _humidity.text = "Humidity : " + weatherData.main.humidity + "%";
                _pressure.text = "Pressure : " + weatherData.main.pressure + "hPa";
                _SearchInput.text = "";
                StartCoroutine(LoadImage());
                Debug.Log("Received: " + webRequest.downloadHandler.text);
            }
        }
    }

    IEnumerator LoadImage()
    {
        string iconWithUrl = "https://openweathermap.org/img/wn/" + _iconCode + "@2x.png";
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(iconWithUrl);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            // Get downloaded asset bundle
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            _weatherIcon.sprite = sprite;
            _weatherIcon.enabled = true;
        }
    }
}
