class Animales():
    def Habla(self):
        print("YO SOY UN ANIMALLLL")

    def Volar(self):
        print("PUEDO VOLaaaaaaaaaarrrrrrr")

    def Descripcion(self):
        print(f"Yo soy un {self.animal}")


class Perro( Animales):
    pass

class Abeja(Animales):
    pass

class Cocodrilo( Animales):
    def __init__(self , animal):
        self.animal = animal


""" animal = Animales()
animal.Habla()

perro = Perro()
perro.Habla()


abeja = Abeja()
abeja.Volar()  """

nombre = input("ingrese el animal que deseas en pantalla: ")

cocodrilo = Cocodrilo(nombre)
cocodrilo.Descripcion()