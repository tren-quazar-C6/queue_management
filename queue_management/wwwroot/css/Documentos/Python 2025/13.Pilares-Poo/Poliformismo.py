# El polimorfismo es modificar el resultado de la funcion main. en este caso se generan respuestas por defecto para tener un mensaje diferente segun la clase que se active

#la clase animal solicita un mensaje
class Animales():
    def __init__(self, mensaje):
        self.mensaje = mensaje

    def Hablar(self):
        print(self.mensaje)


class Perro(Animales):

    def Hablar(self):
        print("Yo no Hablooo, hago wauuu")

class Pez(Animales):
    
    def Hablar(self):
        print("yo no hablo; yo hago glu glu")

#se le pasa un mensaje ya que la funcion la solicita en el self
animal = Animales("EStos son animales generales")
animal.Hablar()

#a pesar de que la funcion main (Animales) solicite un mensaje este de la clase directa perro ya tiene un valor por defecto asi que omite
# el mansaje pasado por usuario e imprime un resultado modificado para dicha clase en este caso  "Yo no Hablooo, hago wauuu"
perro = Perro("waoooo")
perro.Hablar()

#sucede lo mismo para el pez  "yo no hablo; yo hago glu glu"
pescado = Pez("SOY UNA PIDAÑA")
pescado.Hablar()