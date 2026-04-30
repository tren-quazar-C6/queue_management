Jugadores = {
    1 : "Casillas", 
    15 : "Ramos",
    3 : "Pique", 
    5 : "Puyol",
    11 : "Capdevila", 
    14 : "Xabi Alonso",
    16 : "Busquets",
    8 : "Xavi Hernandez",
    18 : "Pedrito", 
    6 : "Iniesta",
    7 : "Villa"
}
  # Solicitar al usuario que ingrese un país
Numero = int(input("Elije un numero de jugador: "))
# Buscar la capital en el diccionario
Numero_jugador = Jugadores.get(Numero)

if Numero_jugador:
    print(f'El numero que eligiste es {Numero} y corresponde al jugador: {Numero_jugador}')
else:
    print(f"Lo siento, no tengo información sobre el jugador numero: {Numero}.")


