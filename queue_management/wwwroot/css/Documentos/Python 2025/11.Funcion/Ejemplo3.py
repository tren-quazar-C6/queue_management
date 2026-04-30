def Mayor():
    num1 = int(input("ingresa un numero: "))
    num2 = int(input("ingresa un numero: "))

    if num1 > num2:
        return num1
    elif num1 < num2:
        return num2
    else:
        return 0
    

print(Mayor())