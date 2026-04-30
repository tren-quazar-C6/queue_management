class Fabrica( ):
    def __init__(self , llantas, color , precio):
        self._llantas = llantas
        self._color= color
        self._precio = precio

    @property
    def llantas(self):
        return self._llantas

    @llantas.setter
    def llantas(self, llantas):
        self._llantas = llantas


    @property
    def color(self):
        return self._color
    
    @color.setter
    def color (self, color):
        self._color = color

    @property
    def precio(self):
        return self._precio

    @precio.setter
    def precio(self, precio):
        self._precio = precio



class Moto(Fabrica):
    def Cantidad(self):
        print(f'la cantidad de llantas son: {self.llantas}')
    
    def colorido(self):
        print(f'El color de la moto es: {self.color}')

    def precios(self):
        print(f'el precio de la moto es: {self.precio}')

class Carro(Fabrica):
  def Cantidad(self):
        print(f'la cantidad de llantas son: {self.llantas}')
    
  def colorido(self):
        print(f'El color de la carro es: {self.color}')

  def precios(self):
        print(f'el precio de la carro es: {self.precio}')


moto = Moto(2,'blanco',32000)
print(moto.llantas)
print(moto.color)
print(moto.precio)



print('//////----////')

carro = Carro(4 , 'Negroooo', 5000000)
carro.Cantidad()
carro.colorido()
carro.precios() 