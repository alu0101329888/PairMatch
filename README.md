# Interfaces Inteligentes - ULL
## Prototipo - PairMatch
### Autores:
 - Thomas Edward Bradley
 - Jakob Guttman
 - Edwin Plasencia Hernández

## Descripción del proyecto

El trabajo desarrollado por el equipo ha sido una aplicación para las plataformas **Cardboard** y **Oculus Quest 2/Pro** del juego de encontrar las parejas o _Pair matching_. En él, el jugador debe encontrar en el menor número de intentos todas las parejas en un tablero, cada vez que se seleccionan dos elementos que no coinciden, éstos se vuelven a ocultar, mientras que si se seleccionan dos elementos iguales, se ocultan y cuentan como una combinación correcta.

## Cuestiones importantes para el uso

Existen dos versiones de la aplicación, la versión para **Cardboard** y la versión para **Oculus Quest 2** y **Oculus Quest Pro**, las mecánicas de las dos son iguales y funcionan de manera similar, cada una está desarrollada e implementada para su determinada plataforma, sin embargo, existen varias diferencias en cuanto al entorno y a efectos especiales entre las dos, principalmente porque la implementación para la plataforma de _Oculus_ puede permitirse mayor complejidad debido a la mayor potencia de los dispositivos.

## Instalación y uso

### Instalación para Cardboard:
- Descarga la versión para _Cardboard_ en tu dispositivo Android
  - Se puede encontrar cada versión en la pestaña **releases** del repositorio.
- Habilita la instalación de aplicaciones de terceros u _"origenes desconocidos"_ en _Configuración_
- Instala el archivo _apk_ y ejecuta la aplicación
- Sigue los pasos dentro de la aplicación para inicializar _Cardboard_

### Instalación para Oculus Quest 2 y Oculus Quest Pro:
- Descarga la versión para **Oculus Quest** en tu ordenador
  - Se puede encontrar cada versión en la pestaña **releases** del repositorio.
- Habilita en la aplicación de móvil **Oculus Mobile App** el modo para desarrolladores
- Instala en tu ordenador una aplicación para sideloading como **SideQuest**
- Conecta las gafas al ordenador por cable, preferiblemente *USB 3.0*
- Instala a través de la aplicación de sideloading el apk de la versión de **Oculus Quest**

## Hitos de programación logrados

- En este proyecto se han implementado correctamente las llamadas de eventos, donde cada bloque y botón se comunican con el _Game Manager_ para lograr la funcionalidad deseada.
- A su vez, se han implementado ejecución de sonidos y música con AudioClips y AudioSources en los altavoces de la escena.
- Se ha desarrollado e implementado para plataformas de _Realidad virtual_ como **Cardboard** y **Oculus Quest**
- Se han implementado elementos y assets de la **Unity Asset Store** así como de recursos gratuitos y de __licencia libre__.

## Aspectos a destacar

La versión para __Cardboard__ utiliza una iluminación simple junto con un entorno básico, éste consiste en una esfera oscura con focos para iluminar los botones y bloques, de manera que se obtenga un efecto parecido al del conocido juego **Beat Saber**, donde la iluminación es tenue y establece como importante sólo los elementos principales del juego, los sonidos son aquellos utilizados en un menú común y la música es tranquila y adecuada al juego con continuidad para que mantenga coherencia con el tipo de juego que es.

En la versión para las plataformas **Oculus** se tiene un entorno más detallado, siendo éste una pequeña pradera con flores, rocas, árboles y casas además de un cielo iluminado con nubes e islas flotantes que rota con el tiempo para simular el movimiento de las nubes. A su vez, la iluminación de la escena es direccional para simular un sol y aunque se mantienen los focos para iluminar los botones, no son necesarios en el final de la escena. Sin embargo, la escena es dinámica y evoluciona conforme el jugador avanza en el juego, primero se establece una niebla oscura y espesa en torno al jugador junto con el oscurecimiento del cielo (por ello mantenemos los focos de los bloques y botón), a medida que el jugador encuentra parejas, la niebla se va disipando dejando ver más parte de la escena, hasta que cuando se encuentran todas, se elimina por completo la niebla y se ilumina el cielo.

Durante esta evolución, también cabe destacar el cambio del audio, los sonidos de los bloques y selección son ligeramente diferentes pero mantienen la misma esencia que la versión de **Cardboard**. A diferencia de la versión de **Cardboard** sin embargo, el sonido de selección correcta se cambió para una mayor coherencia e inmersión y la música de fondo también, se usan además _AudioMixers_ para lograr que el audio de la música tenga un __Highpass__ variable conforme el jugador encuentra parejas, de manera que la canción obtenga un mayor rango y mejores bajos a medida que el jugador va ganando hasta que llega al final de la partida y se escucha la canción en su mayor rango.

## Sensores

Mientras que no se implementó una lectura y análisis de los valores otorgados por los sensores de los dispositivos de manera directa como leer el giroscopio o el accelerómetro, sí que son utilizados por la aplicación. En la versión de **Cardboard**, el módulo de realidad virtual realiza los cálculos y lecturas por nosotros y en la versión de **Oculus**, además de una implementación similar a la de Cardboard, se añade un módulo de interacción por rayos donde se coge la orientación y posición de los mandos/controladores y se castea un **Ray** para interactuar con los elementos de la escena.

## Gif animado de ejecución

El siguiente gif contiene una partida completa de la versión de Oculus:
![oculus](media/gif/oculus_gameplay.gif)

Sin embargo es recomendable ver una versión en vídeo ya que de esta manera se aprecian los cambios de audio y efectos especiales mejor:

[![oculus_video](media/img/watch.png)](media/video/oculus_gameplay.mp4)

## Reparto de tareas