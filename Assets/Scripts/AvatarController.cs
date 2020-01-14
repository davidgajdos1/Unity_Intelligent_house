using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
	public float movementSpeed = 1.5f;
	public Rigidbody2D avatarBody;
	
 private GameObject svetlo_chodba = null;
	private GameObject svetlo_obyvacka1 = null;
	private GameObject svetlo_obyvacka2 = null;
	private GameObject svetlo_obyvacka3 = null;
	private GameObject svetlo_obyvacka4 = null;
	private GameObject svetlo_kupelna = null;
	private GameObject svetlo_spalna = null;
	private GameObject svetlo_TV = null;	

	private termostat termostat_chodba_script = null;
	private termostat termostat_obyvacka_script = null;
	private termostat termostat_spalna_script = null;
	private termostat termostat_kupelna_script = null;
	private teplomer teplomer_vonkajsi_script = null;
	private Weather weather_script = null;

    private DialogFlow dialogflow_script = null;
	
	private GameObject chodba = null;
	private GameObject spalna = null;
	private GameObject obyvacka = null;
	private GameObject kupelna = null;

	private GameObject weather_go = null;
	private GameObject termostat_chodba = null;
	private GameObject termostat_obyvacka = null;
	private GameObject termostat_spalna = null;
	private GameObject termostat_kupelna = null;
	private GameObject teplomer_vonkajsi = null;

	private GameObject TV = null;


    private GameObject DialogFlowManager = null;

	private Vector2 movement;
	private Vector2 pos_chodba;
	private Vector2 pos_spalna;
	private Vector2 pos_obyvacka;
	private Vector2 pos_kupelna;

	private bool automatActive = false;

	private List<GameObject> svetla_obyvacka = null;
    void Start()
	{
		#region Najdenie senzorov
		termostat_chodba = GameObject.Find("Termostat_chodba");
		termostat_obyvacka = GameObject.Find("Termostat_obyvacka");
		termostat_spalna = GameObject.Find("Termostat_spalna");
		termostat_kupelna = GameObject.Find("Termostat_kupelna");
		teplomer_vonkajsi = GameObject.Find("Teplomer_vonkajsi");
		weather_go = GameObject.Find("Weather");


		termostat_chodba_script = termostat_chodba.GetComponent<termostat>();
		termostat_obyvacka_script = termostat_obyvacka.GetComponent<termostat>();
		termostat_spalna_script = termostat_spalna.GetComponent<termostat>();
		termostat_kupelna_script = termostat_kupelna.GetComponent<termostat>();
		teplomer_vonkajsi_script = teplomer_vonkajsi.GetComponent<teplomer>();
		weather_script = weather_go.GetComponent<Weather>();
		#endregion

		#region Najdenie svetiel
		svetlo_chodba = GameObject.Find("chodba_svetlo");
		svetlo_obyvacka1 = GameObject.Find("obyvacka_svetlo1");
		svetlo_obyvacka2 = GameObject.Find("obyvacka_svetlo2");
		svetlo_obyvacka3 = GameObject.Find("obyvacka_svetlo3");
		svetlo_obyvacka4 = GameObject.Find("obyvacka_svetlo4");



		svetlo_kupelna = GameObject.Find("kupelna_svetlo");
		svetlo_spalna = GameObject.Find("spalna_svetlo");
		svetlo_TV = GameObject.Find("TV_svetlo");
		svetla_obyvacka = new List<GameObject>();
		svetla_obyvacka.Add(svetlo_obyvacka1);
		svetla_obyvacka.Add(svetlo_obyvacka2);
		svetla_obyvacka.Add(svetlo_obyvacka3);
		svetla_obyvacka.Add(svetlo_obyvacka4);


		#endregion
		
		#region Vypnutie svetiel
		foreach (GameObject go in svetla_obyvacka)
		{
			go.SetActive(false);
		}
		svetlo_chodba.SetActive(false);
		svetlo_kupelna.SetActive(false);
		svetlo_spalna.SetActive(false);
		svetlo_TV.SetActive(false);
		#endregion

		#region Najdenie miestnosti a TV
		chodba = GameObject.Find("chodba");
		spalna = GameObject.Find("spalna");
		kupelna = GameObject.Find("kupelna");
		obyvacka = GameObject.Find("obyvacka");

		TV = GameObject.Find("televizor");
        #endregion

        #region Najdenie dialogflow
        DialogFlowManager = GameObject.Find("DialogFlowManager");
        dialogflow_script = DialogFlowManager.GetComponent<DialogFlow>();
        #endregion
    }

    void Update()
    {
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

		#region Nastavenie automatizacie svetiel
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (automatActive)
			{
				automatActive = false;
			}
			else
			{
				automatActive = true;
			}
		}
		#endregion

		#region Ziskanie colliderov a pozicii miestnosti
		pos_chodba = chodba.transform.position;
		pos_spalna = spalna.transform.position;
		pos_kupelna = kupelna.transform.position;
		pos_obyvacka = obyvacka.transform.position;

		var collider_avatar = avatarBody.GetComponent<Collider2D>();
		var collider_chodba = chodba.GetComponent<Collider2D>();
		var collider_spalna = spalna.GetComponent<Collider2D>();
		var collider_kupelna = kupelna.GetComponent<Collider2D>();
		var collider_obyvacka = obyvacka.GetComponent<Collider2D>();
		#endregion

		#region Ovladanie svetiel
		if (automatActive)
		{
			if (collider_avatar.bounds.Intersects(collider_chodba.bounds))
			{
				svetlo_chodba.SetActive(true);
			}
			else
			{
				svetlo_chodba.SetActive(false);
			}
			if (collider_avatar.bounds.Intersects(collider_spalna.bounds))
			{
				svetlo_spalna.SetActive(true);
			}
			else
			{
				svetlo_spalna.SetActive(false);
			}
			if (collider_avatar.bounds.Intersects(collider_kupelna.bounds))
			{
				svetlo_kupelna.SetActive(true);
			}
			else
			{
				svetlo_kupelna.SetActive(false);
			}
			if (collider_avatar.bounds.Intersects(collider_obyvacka.bounds))
			{
				foreach (GameObject go in svetla_obyvacka)
				{
					go.SetActive(true);
				}
			}
			else
			{
				foreach (GameObject go in svetla_obyvacka)
				{
					go.SetActive(false);
				}
			}
		}
		else 
		{
			if (collider_avatar.bounds.Intersects(collider_chodba.bounds))
			{
				if (Input.GetKeyDown(KeyCode.F)) { svetlo_chodba.SetActive(!svetlo_chodba.activeSelf); }
			}
			if (collider_avatar.bounds.Intersects(collider_kupelna.bounds))
			{
				if (Input.GetKeyDown(KeyCode.F)) { svetlo_kupelna.SetActive(!svetlo_kupelna.activeSelf); }
			}
			if (collider_avatar.bounds.Intersects(collider_obyvacka.bounds))
			{
				if (Input.GetKeyDown(KeyCode.F)) 
				{
					foreach (GameObject go in svetla_obyvacka)
					{
						go.SetActive(!go.activeSelf);
					}
				}
			}
			if (collider_avatar.bounds.Intersects(collider_spalna.bounds))
			{
				if (Input.GetKeyDown(KeyCode.F)) { svetlo_spalna.SetActive(!svetlo_spalna.activeSelf); }
			}
		}
		#endregion

		#region Ovladanie TV
		if (collider_avatar.bounds.Intersects(collider_obyvacka.bounds))
		{
			//vypnutie zapnutie TV cez medzernik ak je avatar v obyvacke
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (svetlo_TV.activeSelf)
				{
					svetlo_TV.SetActive(false);
				}
				else
				{
					svetlo_TV.SetActive(true);
				}
			}
		}
		#endregion
	}
	void FixedUpdate()
	{
		avatarBody.MovePosition(avatarBody.position + movement * movementSpeed * Time.fixedDeltaTime);
		avatarBody.freezeRotation = true;
	}

	private void OnGUI()
	{
		GUI.Box(new Rect(1, 20, 175, 130), 
		"AutoLight Mode: " + automatActive + 
		"\nTeplota chodba: " + termostat_chodba_script.getTemperature()+
		"\nTeplota obyvacka: " + termostat_obyvacka_script.getTemperature()+
		"\nTeplota spalna: " + termostat_spalna_script.getTemperature()+
		"\nTeplota kupelna: " + termostat_kupelna_script.getTemperature()+
		"\nTeplota vonku: " + weather_script.getTemperature()+
		"\nPocasie: " + weather_script.getWeatherTitle()+
        "\nPush-to-Talk-Active: " + dialogflow_script.IsPressed()
		);
	}
}
