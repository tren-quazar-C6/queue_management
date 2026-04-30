import re
# se utiliza para busquedas y muestra una unica coincidencia
#se usa con el ejemplo de expresiones regulares
texto = 'Estaré disponible en el +34755142009 el lunes por la tarde o en la mañana al +57304325714'

regex = r'\+?\d{1}\d{10}'

buscar = re.search(regex, texto)
print(buscar)

Texto2 = "los carro son mas seguros, el carro es lindo"
regex2 = r"carr[o|a]s?"
buscar2= re.search(regex2,Texto2)
print(buscar2)