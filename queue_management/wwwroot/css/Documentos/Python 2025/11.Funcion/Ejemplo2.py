
def facto():
    num = int(input("Ingresa un numero: "))
   
    if num > 0:
        fac = 1
        for i in range(1 , num +1):
            fac = fac * i

        print(f" el factorial de {num} es {fac} ")
        
    else:
        print("el numero es negativo")

facto()
