# TrickDisk

##Singleton Pattern:
###Clases: AudioManager y GameManager implementan el patrón Singleton al tener una propiedad estática para acceder a una única instancia de la clase.

##Observer Pattern (Observador):
###Clase: AudioManager al registrar sonidos en los botones (AddButtonSound) para notificar eventos de clic.

##Command Pattern (Comando):
###Clase: GameManager tiene métodos como GoToMainMenu y ReloadGame, que encapsulan acciones específicas.

##Strategy Pattern (Estrategia):
###Clase: El manejo de la lógica de sonido y la interacción con el AudioManager en GameManager en función de las configuraciones del usuario.

##Template Method Pattern (Método de Plantilla):
###Método: El método UpdateScore en GameManager.

##State Pattern (Estado):
###Clase: En Player, se manejan diferentes estados del jugador (hasGameStarted, canRotate, canShoot, canMove, canScore).

