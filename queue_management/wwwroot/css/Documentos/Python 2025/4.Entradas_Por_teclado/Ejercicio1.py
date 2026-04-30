from math import sqrt

a = int(input("ingrese un numero para A : "))
b = int(input("ingrese un numero para B: "))
c = int(input("ingrese un numero para C: "))

leu = ((b**2) - (4*a*c))

if leu < 0:
    print("no se puede realizar la operacion porque no se puede sacar raiz cuadrada a cero ")
else:
   x1 = (-b - sqrt(leu))/2*a
   x2 = (-b + sqrt(leu))/2*a
   print(x1)
   print(x2)

