
'''
Diferencias entre una lista y un CONJUNTO 
un CONJUNTO permite el ingreso de varios valores repetidos pero NO los muestra y NO los cuenta.

La LISTA permite el ingreso de varios valores repetidos pero este los muestra y TAMBIEN los cuenta.

'''
conjunto = {1,2,3,4,4,5,5,6,6}
print(conjunto)
lista = [1,2,3,4,4,5,5,6,6]
print(lista)

#para agregar un dato en un conjunto 

conjunto.add(30)
print(conjunto)

#para eliminar un dato dentro del conjunto, busca el dato ingresado y lo elimina
conjunto.remove(5)
print(conjunto)

#otro metodo para eliminar es discard y hace lo mismo 
conjunto.discard(9)
print(conjunto)

#pop Elimina un dato de manera aleatoria
conjunto.pop()
print(conjunto)

#update lo que hace es añadir numeraciones iterables
conjunto.update([9,15,20])
print(conjunto)

#Clear borra todo el conjunto

conjunto.clear()
print(conjunto)

