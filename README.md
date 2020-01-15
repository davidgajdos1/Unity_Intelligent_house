<h1 align="center">UNITY INTELLIGENT HOUSE</h1>

## Abstract
The point of this project was to create an intelligent house assistant in Unity3D with use of cloud services and implementation
of vocal controlling.

We have created custom tilesets, avatar, objects and devices as pixel-art textures. Player interacts with game using keyboard and communicates with agent through a microphone.

Player is able to see temperatures in each room, input from webcamera, temperature outside synchronised with his current location using WeatherAPI, and communicate with virtual agent using Google DialogFlow API.
Verification of users is provided by Azure FaceAPI.

## Zadanie

*Osobný domáci asistent. Vytvorenie modelu bytu, aspoň 2+1, aspoň 2 ľudia, hlasový asistent sám rieši stav prostredia v byte a reaguje na požiadavky užívateľov.*

<p align="center">
  <img src="https://i.imgur.com/7A8fKBh.png">
</p>

## Tvorcovia
Na projekte sa podielali traja študenti Technickej univerzity v Košiciach. Každý z nich mal za úlohu implementovať niekoľko riešení, ktoré zabezpečujú dané funkcie projektu.

- ***Tomáš Juščík*** - Vytvorenie agenta v prostredí DialogFlow, vytvorenie intentov pre komunikáciu User - Agent, implementovanie DialogFlowApi v python-e a následne volanie scriptu v Unity projekte
- ***Richard Kačúr*** - Vytvorenie Unity3D projektu, vytvorenie 2D prostredia bytu ako aj vytvorenie 2D avatara, ošetrenie kolízie avatara s objektmi, pohyb avatara, implementácia WeatherApi
- ***Dávid Gajdoš*** - Implementácia FaceRecognitionApi, vytvorenie a implementácia rôznych senzorov v prostredí 2D bytu

## Architektúra projektu

Celý projekt je tvorený z dvoch častí : \
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- **Unity3D projekt**\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- **Python projekt**
### Unity projekt ###
- ***Assets***
  - **Animations** - *Obsahuje súbory pre základne animácie objektov*
  - **Graphics** - *Obasahuje 2D textury, ktoré slúžia ako grafická reprezentácia daných objektov v 2D protredí*
  - **Lights** - *Obsahuje textúry pre svetlo v byte*
  - **Plugins** - *Obsahuje externú knižnicu pre prácu s python scriptomami v prostredí Unity3D*
  - **Scenes** - *Obsahuje scény, ktoré sa využívajú v projekte*
  - **Scripts** - *Obsahuje všetky scripty, ktoré zabezpečujú funkcie v projekte*
    - **AvatarController** - *Tento script obsahuje funkcie pre pohyb avatara pomocou klavesnice ako aj zabezpečuje logiku kolízií v priestore, verifikácia užívateľov*
    - **DialogFlow** - *Tento script zabezpečuje spustenie python scriptu (Je nutné nastaviť správnu cestu pre python script Main.py).*
    - **displayWebcam** - *Tento script slúži na zachytávanie obrazu z web kamery a následnú implementáciu FaceRecognitionApi*
    - **teplomer** - *Script pre nastavenie vonkajšej teploty pomocou WeatherApi*
    - **termostat** - *Script pre generovanie hodnôt v izbách*
    - **Weather** - *Tento script slúži na komunikáciu medzi projektom a WeatherApi*
  - **Tileset** - 

### Python projekt ###
- **Main.py** - *Main script, v ktorom sa volá funkcia Recording z Record.py*
- **DialogFlow.py** - *Script, ktorý zabezpečuje odoslanie nahratého hlasového povelu, ktorý je vo formáte .avi do DialogFlow pomocou DialogFlowApi, kde sa následne vyhodnotí odpoveď agentom a je zaslaná naspať v rovnakom formáte. Po obdržaní odpovede od agenta sa hlasová správa prehraje užívateľovi*
- **Record.py** - *Scirpt, ktorý zabezpečuje zachytenie hlasového povelu cez mikrofón. Nahrávanie je aktívne pokiaľ je stlačena klávesa P*

## Využité cloudové služby ##
### WeatherApi ###
&nbsp;&nbsp; - Implementácia WeatherAPI je v skripte Weather.cs . Využité je API zo stránky https://openweathermap.org/ Free verzia nám dovoľuje sa požiadať o dáta 60krát v priebehu jednej hodiny. Získané dáta dostávame v XML dokumente, z ktorého dostávame informácie o teplote, vlhkosti, zamračenosti a taktiež mesta, z ktorého meranie pochádza. Získaná teplota sa zobrazí v UI.\
Ukážka API requestu pre získanie dát z Košíc podľa geografickej polohy.
```C#
string url = "http://api.openweathermap.org/data/2.5/find?lat=48.67&lon=21.33&units=metric&type=accurate&mode=xml&APPID=APPIKEY";
```

### DialogFlow ###
*DialogFlow* je **Cloudová služba**, poskytovaná firmou **Google**. Pomocou tejto služby si vieme vytvoriť *agenta*, ktorý pomocou intentov bude komunikovať s *užívateľom*. *Užívateľ* nemusí vytvárať vlastné intenty na to, aby agent vedel komunikovať s *užívateľom*. *Agent* je vopred natrenovaný na intenty, ktoré sa nachádzaju v **Google databáze**. *Užívateľ* môže s agentom komunikovať pomocou písaného textu alebo pomocou hlasových nahrávok. **Google** poskytuje k tejto službe aj svoju službu na *speech-to-text* a *text-to-speech*.

<p align="center">
  <img src="https://i.imgur.com/9DpgRju.png">
</p>

Ukážka komunikácie medzi **agentom** a **uživateľom**:
```
Užívateľ -> Agent: Ahoj.
Agent -> Užívateľ: Môžeš sa identifikovať?
Užívateľ -> Agent: Tu je Tomáš.
Agent -> Užívateľ: Ahoj Tomáš. Ako ti môžem pomôcť?
```

### FaceRecognitionApi ###
- (Microsoft Azure, subskripcia Azure for Students, 30000 volaní mesačne zdarma), endpoint https://faceappcloudy.cognitiveservices.azure.com/
Hlavný skript sa nachádza v priečinku Assets/Scripts/displayWebcam.cs
Služba funguje ako RESTapi, kde vstupom je fotografia a výstupom je JSON obsahujúci výstup zo služby.
Pre správne fungovanie je nutné na disku vytvoriť priečinok C:\WebcamSnaps\, alebo nastaviť inú cestu pre ukladanie snímok z kamery.
Verifikáciu zabezpečuje porovnanie fotografie z webkamery s vytvorenou skupinou na Azure FaceAPI, kde má každý užívateľ pridelený jednoznačný identifikátor a vracia confidence level, ako veľmi sa tvár podobá s tvárou v databáze. Pre úspešnú verifikáciu musí mať daný užívateľ confidence aspoň 0.8.
Vzor výstupného JSONu: 
```C#
[{"faceId":"7a60c6b4-31a4-409c-86eb-8a59b75f5255","faceRectangle":{"top":202,"left":187,"width":203,"height":203},"faceAttributes":{"smile":0.0,"headPose":{"pitch":-8.1,"roll":2.2,"yaw":-0.6},"gender":"male","age":25.0,"facialHair":{"moustache":0.4,"beard":0.4,"sideburns":0.1},"glasses":"NoGlasses","emotion":{"anger":0.0,"contempt":0.0,"disgust":0.0,"fear":0.0,"happiness":0.0,"neutral":0.999,"sadness":0.001,"surprise":0.001},"blur":{"blurLevel":"high","value":1.0},"exposure":{"exposureLevel":"goodExposure","value":0.59},"noise":{"noiseLevel":"medium","value":0.34},"makeup":{"eyeMakeup":false,"lipMakeup":false},"accessories":[],"occlusion":{"foreheadOccluded":false,"eyeOccluded":false,"mouthOccluded":false},"hair":{"bald":0.82,"invisible":false,"hairColor":[]}}}]
```


>Link na video: https://www.youtube.com/watch?v=4Bg3-5NMHdE

