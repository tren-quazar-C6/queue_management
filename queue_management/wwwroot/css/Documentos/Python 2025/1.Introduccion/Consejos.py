"""1. no puede tener nombre las variables de letras reservadas """
import keyword 
### Me muestra mediante el print las palabras claves
keyword.kwlist
print(keyword.kwlist)

#Muestra el valor de la variable numero
Num=120
#print(Num)

#CONVErtir numeros en python de int a float

Int1 = 40
Int2 = 12.3

print(type(Int1))   
# se antepone a la variable al tipo de dato q se desea convertir
print(float(Int1))
# en el ejemplo o por consola, este imprime 40.0
