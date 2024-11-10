

/*Para armar este juego se siguio el siguiente tutorial 
 * https://www.youtube.com/watch?v=ZqLGW2cbw5o
 * 
 * 
 * Modo creador
 * Contamos con un modo creador, se activa poniendo en verdadero la booleana del GameManager
 * En este modo creamos notas en vez de destruirlas, lo que nos permite ir tocandolas con el ritmo de la canción. Luego de armar nuestra secuencia de notas debemos PAUSAR el juego.
 * Copiar las notas del Hierarchy, deterner el juego, y pegar las notas en nuestra escena. 
 * Luego nos resta acomodar las notas al comienzo y colocar nuestra nota ganadora al final de la canción. Recuerden salir del modo creación para probar el juego, sino seguirán creando notas.
 * 
 * Nota Ganadora
 * Esta nota la utilizamos para saber cuando termina la canción. Cuando creen sus patrones recuerden ponerla en la final de la secuencia.
 * Esta nota es invisible, la puede encontrar desde el Hierarchy
 * 
 * 
 * Rock Meter
 * Lo utilizamos para determinar cuando pierde el juego. Tiene un valor máximo de 50 puntos, cuando erra una nota baja 2 puntos y cuando acierta suma 1. 
 * Todo esto pueden configurarlo desde el GameManager, en las funciones AgregarGolpe y ResetearGolpes.
 * 
 * El juego cuenta con un contador de notas acertadas (NotasHit) y contador de mayor cantidad de golpes consecutivos.
 * 
 * */


