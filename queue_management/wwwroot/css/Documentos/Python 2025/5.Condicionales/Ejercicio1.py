#Crear un programa que pida al usuario una letra, y si es vocal, muestre el mensaje "Es vocal". Sino, decirle al usuario que no es vocal

Letra = input(" Ingrese una letra: ")

"""if  Letra.lower() == "a" or Letra.lower() == "e" or Letra.lower() == "i" or Letra.lower() == "o" or Letra.lower() == "u":
    print("Es vocal")
else:
    print("No es una vocal")"""


#o se usa la siguiente

if Letra.lower() in "aeiou":
    print("Es vocal")
else:
    print("No es una vocal")