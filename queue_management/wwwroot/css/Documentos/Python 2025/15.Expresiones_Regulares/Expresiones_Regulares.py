import re

#para expresiones regulares se utiliza el modulo re "regular expresion" y se usa para realizar busquedas de patrones dentro de un rango establecido
#ejemplo
#en el que queremos buscar un número de teléfono dentro de un texto. Para ello vamos a utilizar la función search()

texto = 'Estaré disponible en el +34755142009 el lunes por la tarde o en la mañana al +57304325714'
#nombre dela variable regex "reg" de regular y "ex" de expresion

regex = r'\+?\d{1}\d{10}'
         
""" 
separa los prefijos o por numeros
'\+?(\d{2})(\d{9})
Este lo muestra completo
r'\+?\d{1}\d{10}' 

"""

# solo busca una sola coincidencia
buscar = re.search(regex, texto)

# para buscar mas de una coincidencia o numeros fijos en este caso se utilida findall y lo devuelve en un lista o array
buscar2 = re.findall(regex, texto)


print(buscar)
print(buscar2)
print(buscar2[1])