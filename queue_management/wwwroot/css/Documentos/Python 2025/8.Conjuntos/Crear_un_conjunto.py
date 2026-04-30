#CRea directamente un conjunto, es similar a un diccionario pero no tiene Clave : valor
Conjunto_numeros = {1,2,3,4,5}
print(type(Conjunto_numeros))

#en este caso si no se coloca doble parentesis y se antepone la palabra SET o conjunto se crea una tupla
conjunto_de_letras = set(("yo","ty","hi"))
print(type(conjunto_de_letras))

#en este cao si no se coloca la palabra set y dentro de los corchetes se coloca los valores entre parentesis se tomara como una lista
conjunto_mixto = set[(1,2,3,"yo","tu")]
print(type(conjunto_mixto))

