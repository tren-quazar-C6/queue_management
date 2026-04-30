"""Escribe un programa que pida dos palabras y diga si riman o no. 
Si coinciden las tres últimas letras tiene que decir que riman. 
Si coinciden sólo las dos últimas tiene que decir que riman un poco y si no, que no riman."""

Palabra1 = input("Ingresa la primera palabra: ")
Palabra2 = input("Ingresa la segunda palabra: ")




if Palabra1[-3: ] == Palabra2[-3: ]:
    print("Si riman")
else:
    print("No riman")