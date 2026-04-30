# Escribir un programa que solicite al usuario un vocal en minuscula, y luego una letra en mayúsculas. 
# El programa debe convertir la letra en minúscula y la vocal en mayúscula, y al final, deben ser concatenadas ambas


Minus = input("Ingrese una letra Minuscula: ")
Mayus = input("Ingrese una letra Mayuscula: ")

#print( "la letra minuscula pasará a mayuscula: ", Minus.upper() , "\nLa letra Mayuscula pasará a Minuscula: ", Mayus.lower() )

print(f"La letra Minuscula: '{Minus}' \b pasara a mayuscula: {Minus.upper()} \nLa letra mayuscula \b '{Mayus}' pasará a Minuscula {Mayus.lower()}")