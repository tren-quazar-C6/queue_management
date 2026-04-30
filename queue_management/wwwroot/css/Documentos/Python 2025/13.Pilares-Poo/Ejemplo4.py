class Marino():
    def Hablar(self):
        print('Hola...')

class Pulpo(Marino):
     def Hablar(self):
        print('Soy un pulpo glu glu glu')

class Foca(Marino):
    def Hablar(self, mensaje):
        self.mensaje = mensaje
        print(f'El mensaje es:', self.mensaje)


marino = Marino()
marino.Hablar()

pulpi = Pulpo()
pulpi.Hablar()


foca = Foca()
men = input("ingrese un mensaje: ")
foca.Hablar(men)
