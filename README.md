# Prueba de Unity de LabCave

## _Explicación de la prueba y relación de cambios_

Para la prueba he optado por modificar el mínimo posible el propio sistema del template para asegurar sobre todo la estabilidad y evitar bugs. 

Para cambiar el tipo de juego simplemente se ha añadido el objecto ObjectiveCompleteLaps y se ha modificado en la escena para que sea una carrera por vueltas.

Para añadir el sistema de “Coins” se ha añadido otro objeto de objetivo llamado ObjectiveCrashMode y se ha marcado como objetivo opcional. De esta manera aparecen todas las monedas de la pista y cuantas te quedan por recoger.

Para guardar el mejor tiempo y la cantidad de coleccionables recogidos se ha creado un GameSaveManager que maneja un scriptable object llamado GameScoreScriptable que contiene los datos que buscamos guardar (mejor tiempo y monedas). De esta manera se ha implementado un sistema de guardado local rudimentario pero expandible. (Como se ha dicho en el enunciado, el scriptable object es volátil y sólo se guarda en local) 

Con el propósito de guardar el mejor tiempo, se han añadido líneas al script TimeDisplay que guardan directamente el mejor tiempo (El sistema de “bestTimes” está directamente ligado al view de los tiempos ya que no hace nada con esa información, sólo la muestra. Hubiera sido más eficiente unificar la gestión de cronómetros y tiempos en el propio TimeManager para que tuviese más sentido, pero por hacer la prueba más rápida se ha optado por mantenerlo como en el template original). También comprueba si el mejor tiempo guardado es mejor o no que el mejor tiempo actual y si se ha batido el récord se muestra una notificación con el sistema de notificaciones “toast” del propio template (para mantener consistencia).

Se ha creado un script llamado CollectObject que hereda de TargetObject para implementar las monedas. Este script lleva incorporada también el guardado de la cantidad de monedas recogidas.

Se ha creado en el IntroMenu un elemento en el canvas para mostrar el mejor tiempo y las monedas recogidas hasta el momento. Se ha creado un script llamado ScoreDisplay para mostrarlos. Este script coge los datos directamente del GameSaveManager.

Como partes opcionales:
Se ha implementado el sistema de “new” Input System de Unity para poder implementar los sistemas de input en pantalla. Con este propósito, se han añadido en el MainScene, en el canvas los controladores táctiles. Además, se ha modificado el script KeyboardInput del kart para que acepte los eventos del Input System. El input en modo móvil está probado en un Nothing Phone con Android 14 y no es tan fiable como el teclado así que habría que depurarlo en un futuro.

Se ha modificado ligeramente las pantallas de Intro, Win y Lose para que estén más al alcance de los pulgares al estar en landscape. Todo es responsive en modo landscape, aunque no en portrait.

En el menú de pausa de la MainScene se ha añadido un botón de debug que abre un menú con algunos ejemplos de modificaciones que se pueden hacer a los objetivos o al kart del jugador, como las vueltas, la velocidad, la aceleración, etc. Esto lo gestiona el script DebugMenuController. Hay que tener en cuenta que, cuando se cambia la cantidad de vueltas, el mensaje del objetivo no se actualiza hasta que pasas por la salida pero el valor sí que se tiene en cuenta.

## Preguntas

## _Si en vez de tener un template de Unity hubieras tenido que crear tu el proyecto,cómo lo hubieras organizado a nivel de carpeta y estructura? ¿Qué hubieras cambiado o hecho diferente?_

Creo que depende enteramente del proyecto. El sistema de organización por tipo (como el del template) es bastante sencillo de entender, está bastante extendido y muchas veces es el que los propios desarrolladores de Unity utilizan. Suele ser efectivo para proyectos pequeños como este, con pocas escenas y unas cuantas funcionalidades muy específicas.

Ahora, si el proyecto es cada vez más grande o si se planea extenderlo continuamente con nuevas funcionalidades y con muchas personas trabajando en distintas partes, considero que la organización por “Features” es bastante más efectiva a la hora de organizarse.

Si hubiera optado por la primera opción habría añadido tal vez una carpeta de Plugins para no ensuciar tanto la raíz del proyecto. Si hubiera optado por la segunda habría creado una carpeta por cada feature (KartMechanics, UI, EnemyAI, Network, etc.) para tener relativamente “a mano” en la que esté trabajando en ese momento.

## _Si se te hubiera pedido almacenar las puntuaciones de manera persistente (en vez de localmente) a nivel de id de usuario, ¿qué servicios hubieras usado?_

Si tuviera que almacenar por id de usuario asumo que el proyecto tiene conectividad a internet o al menos un backend que soporte las cuentas. En mi experiencia, PlayFab por ejemplo tiene soporte para almacenar distintas variables de jugador, como monedas, coleccionables, etc. O directamente el almacenamiento de archivos JSON en el servidor por cada usuario. Seguramente cambiaría el sistema de guardado e implementaría el guardado desde el backend para esos valores. 

Si no dispusiera de un backend que lo gestionara, como se trata de nivel de usuario seguramente generaría un JSON y lo encriptaría para guardarlo en el dispositivo. O directamente usaría algún plugin como EasySave que ahorran bastante tiempo.

## _Si se te hubiera pedido localizar el juego a varios idiomas, ¿cómo lo habrías hecho?_

Se que Unity tiene ahora su propio sistema de localización integrado, pero en mi experiencia es muy engorroso. Lo que generalmente he visto que mejor funciona en crear un sistema de reemplazo de .text que se base en un XML con distintos labels para cada text que se encuentre en el juego. He visto que suele ser muy útil de esta manera porque puedes pasar el XML directamente a distintos traductores (por ejemplo Inglés, Alemán, Portugués, etc.) y ellos mismos pueden modificar el contenido en el idioma concreto. Luego simplemente cambias el XML según el idioma seleccionado y así cambias todos los textos. (También valen JSON, CSV, YAML, pero XML suele ser directo, sencillo y más o menos legible).

## _¿Dónde y cómo implementarías compras en la aplicación? ¿Qué servicios conoces o utilizarías para ello?_

En el caso del Kart seguramente en la introScene añadiría una tienda para comprar distintos tipos de karts, de pilotos, pistas, etc. y crearía un gestor para la tienda conectado a un servicio de compras in-app. 

Los principales servicios que conozco son Unity IAP; que es el principal de Unity y aúna varias stores, Google Play IAB y Apple IAP.

Seguramente utilizaría los servicios de Unity IAP y de Unity Ads en conjunto con Analytics para gestionar las compras in-app, así me ahorro tener que montar servidores de verificación, gestión de distintas plataformas, etc.

## _Si tuvieras que monetizar el juego a través de anuncios, ¿cómo lo harías o qué estrategia seguirías? ¿Qué servicios de anuncios conoces para poder monetizar la aplicación?_

Normalmente huiría del típico banner en la parte de debajo de la pantalla que pulsas sin querer y te lleva a la store. Probablemente crearía un sistema de “vidas” o “power ups” relativamente flexible para que cuando al jugador le cueste mucho un circuito o quiera terminar antes pueda pagar esas vidas o power apps con dinero in-game, con dinero de la tienda o con ver uno o dos anuncios, dándole las tres opciones para que no tenga la sensación de que la dificultad se sube artificialmente para que pierda y pase por caja. 
Para la implementación, como he mencionado antes, seguramente use Unity Ads ya que está ligado a los servicios de Analytics y Unity IAP. Como alternativa, en algún momento usé AdMob para algún pequeño proyecto pero creo que últimamente no tenía compatibilidad con algunas de las últimas versiones de Unity.
