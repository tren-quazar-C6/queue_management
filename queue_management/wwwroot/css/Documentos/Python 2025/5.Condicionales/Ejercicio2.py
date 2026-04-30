#Escribir un programa que, dado un número entero, muestre su valor absoluto. Nota: para los números positivos su valor absoluto es igual al número (el valor absoluto de 52 es 52), 
#mientras que, para los negativos, su valor absoluto es el número multiplicado por -1 (el valor absoluto de -52 es 52).


Numero = int(input(" Ingresar un numero: "))

if Numero >= abs(Numero):
    print(f"el numero Ingresado es absoluto: {Numero}")
else:
    print(f"el numero ingresado {Numero}, no es absoluto. Su absoluto es: {abs(Numero)}")

