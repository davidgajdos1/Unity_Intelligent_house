using UnityEngine;
using System.Xml;
using System;
using System.Collections;
using System.Globalization;

public class Weather : MonoBehaviour
{
	private float teplota_von;
	private string typ_pocasia;
	void Start()
	{
		StartCoroutine(updateWeather());
	}


	IEnumerator updateWeather()
	{
		while (true)
		{
			string url = "http://api.openweathermap.org/data/2.5/find?lat=48.67&lon=21.33&units=metric&type=accurate&mode=xml&APPID=421db507080baf4cfbbc8fd3cdaf628d";
			WWW www = new WWW(url);
			yield return www;
			if (www.error == null)
			{
				//421db507080baf4cfbbc8fd3cdaf628d   --> API KEY

				Debug.Log("Loaded following XML " + www.text);
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(www.text);
				Debug.Log("City: " + xmlDoc.SelectSingleNode("cities/list/item/city/@name").InnerText);
				Debug.Log("Temperature: " + xmlDoc.SelectSingleNode("cities/list/item/temperature/@value").InnerText);
				Debug.Log("Humidity: " + xmlDoc.SelectSingleNode("cities/list/item/humidity /@value").InnerText);
				Debug.Log("Cloud : " + xmlDoc.SelectSingleNode("cities/list/item/clouds/@value").InnerText);
				Debug.Log("Title: " + xmlDoc.SelectSingleNode("cities /list/item/weather/@value").InnerText);

				typ_pocasia = xmlDoc.SelectSingleNode("cities /list/item/weather/@value").InnerText;
				teplota_von = float.Parse(xmlDoc.SelectSingleNode("cities/list/item/temperature/@value").InnerText, CultureInfo.InvariantCulture.NumberFormat);
			}
			else
			{
				Debug.Log("ERROR: " + www.error);

			}
			yield return new WaitForSeconds(100000.0f);
		}
	}

	public float getTemperature()
	{
		return teplota_von;
	}

	public string getWeatherTitle()
	{
		return typ_pocasia;
	}
}
