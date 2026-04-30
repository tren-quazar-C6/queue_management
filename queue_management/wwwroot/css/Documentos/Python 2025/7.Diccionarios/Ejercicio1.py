'''
En el siguiente diccionario se encuentran capitales de los paises en el mundo, debes realizar un programa que pida un pais al usuario, 
y muestre la capital de ese pais, en dado caso el pais no este en el diccionario, se debe mostrar un mensaje diciendo que ese pais no se encuentra
'''
Paises = {
    "Guatemala": "Ciudad de Guatemala", 
    "El Salvador": "San Salvador", 
    "Honduras": "Honduras",
    "Nicaragua": "Managua", 
    "Costa Rica": "San Jose", 
    "Panama": "Panama",
    "Argentina": "Buenos Aires",
    "Colombia": "Bogota", 
    "Venezuela": 
    "Caracas", 
    "España": "Madrid"
}
# Solicitar al usuario que ingrese un país
pais = input("Por favor, ingresa el nombre de un país: ")

# Buscar la capital en el diccionario
#capital = Paises.get(pais.title()) 

#print(User.capitalize())
# Buscar la capital en el diccionario

# Usamos .title() para normalizar la entrada
'''
if capital:
    print(f"El pais es: { pais.title()} es {capital}")
else:
    print(f"Lo siento, no tengo información sobre la capital de {pais.title()}.")
'''

if Paises.get(pais.title()):
     print(f"El pais es: { pais.title()} es {Paises.get(pais.title())}")
else:
     print("no existe..")