# UNITY INTELLIGENT HOUSE
![Intelligent House](https://i.imgur.com/7A8fKBh.png)
## Abstract 

## Abstrakt

## Tvorcovia
Na projekte sa podielali traja študenti Technickej univerzity v Košiciach. Každý z nich mal za úlohu implementovať niekoľko riešení, ktoré zabezpečujú dané funkcie projektu.

- ***Tomáš Juščík*** - Vytvorenie agenta v prostredí DialogFlow, vytvorenie intentov pre komunikáciu User - Agent, implementovanie DialogFlowApi v python-e a následne volanie scriptu v Unity projekte
- ***Richard Kačúr*** - Vytvorenie Unity3D projektu, vytvorenie 2D prostredia bytu ako aj vytvorenie 2D avatara, ošetrenie kolízie avatara s objektmi, pohyb avatara, implementácia WeatherApi
- ***Dávid Gajdoš*** - Implementácia FaceRecognitionApi, vytvorenie a implementácia rôznych senzorov v prostredí ľd bytu

## Architektúra projektu
### Vzužité programovacie jazyky ###
Celý projekt je tvorený z dvoch častí :
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- **Unity3D projekt**\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- **Python projekt**
### Unity projekt ###
- Assets\
  - Animations - Obsahuje súbory pre základne animácie objektov.
  - Graphics - Obasahuje 2D textury, ktoré slúžia ako grafická reprezentácia daných objektov v 2D protredí
  - Lights - Obsahuje textúry pre svetlo v byte
  - Plugins - Obsahuje externú knižnicu pre prácu s python scriptomami v prostredí Unity3D
  - Scenes - Obsahuje scény, ktoré sa využívajú v projekte
  - Scripts - Obsahuje všetky scripty, ktoré zabezpečujú funkcie v projekte
  - Tileset - 
### Python projekt ###

