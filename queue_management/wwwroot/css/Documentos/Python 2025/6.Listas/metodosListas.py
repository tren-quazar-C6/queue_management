lista = [8,1,2,7,4,5,9,6,6]
print(lista)
car = int(input("ingresa un numero: "))
# agregar datos a una lista
#lista.append(car)

# agregar un valor en la posicion que yo quiera
lista.insert(2, car)
print(lista)

#nos permite saber cuantas veces aparece el valor en al lista
print(lista.count(6))

#index busca en un parametro y retorna el primero q encuentre
print(lista.index(2))

# ordenar una lista, solo permite con valores numericos o caractaeres, al ingresar el dato y no especificarlo SIEMPRE LO TOMA POR DEFECTO COMO CARACTER

lista.sort()
print(lista)

# ordena una lista de forma al reves o reversible

lista.reverse()
print(lista)
