
letras = ('a','e','i','o','u')

num = int(input('ingresar el numero de la letra que quieres de 1 a 5: '))
num -= 1
if letras[num]:
    print(f"la letra es: {letras[num]}")
else:
    print("no existe")