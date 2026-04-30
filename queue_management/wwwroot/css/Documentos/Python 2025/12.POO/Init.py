
class FabricaTelefonos():
    def __init__(self, marca , color, tamano):
       self.marca = marca
       self.color = color
       self.tamano = tamano

marca = input("ingresa la marca deseada: ")
color = input("ingresa el color: ")
tamaño = input(" ingresa el tamaño: ")

telefono = FabricaTelefonos(marca,color, tamaño)

print(telefono.marca)
print(telefono.color)
print(telefono.tamano)