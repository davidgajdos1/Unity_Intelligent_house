<h1 align="center">UNITY INTELLIGENT HOUSE</h1>

## Abstract
The point of this project was to create an intelligent house assistant in Unity3D with use of cloud services and implementation
of vocal controlling.

We have created custom tilesets, avatar, objects and devices as pixel-art textures. Player interacts with game using keyboard and communicates with agent through a microphone.

Player is able to see temperatures in each room, input from webcamera, temperature outside synchronised with his current location using WeatherAPI, and communicate with virtual agent using Google DialogFlow API.
Verification of users is provided by Azure FaceAPI.

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
    - **AvatarController** - *Tento script obsahuje funkcie pre pohyb avatara pomocou klavesnice ako aj zabezpečuje logiku kolízií v priestore*
    - **DialogFlow** - *Tento script zabezpečuje spustenie python scriptu (Je nutné nastaviť správnu cestu pre python script Main.py).*
    - **displayWebcam** - *Tento script slúži na zachytávanie obrazu z web kamery a následnú implementáciu FaceRecognitionApi*
    - **teplomer** - *Script pre generovanie hodnôt v senzoroch*
    - **termostat** - *Script pre generovanie hodnôt v termostate*
    - **Weather** - *Tento script slúži na komunikáciu medzi projektom a WeatherApi*
  - **Tileset** - 

### Python projekt ###
- **Main.py** - *Main script, v ktorom sa volá funkcia Recording z Record.py*
- **DialogFlow.py** - *Script, ktorý zabezpečuje odoslanie nahratého hlasového povelu, ktorý je vo formáte .avi do DialogFlow pomocou DialogFlowApi, kde sa následne vyhodnotí odpoveď agentom a je zaslaná naspať v rovnakom formáte. Po obdržaní odpovede od agenta sa hlasová správa prehraje užívateľovi*
- **Record.py** - *Scirpt, ktorý zabezpečuje zachytenie hlasového povelu cez mikrofón. Nahrávanie je aktívne pokiaľ je stlačena klávesa P*

## Využité cloudové služby ##
### WeatherApi ###
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

>Link na video: (link-na-youtube)

