class A():
    def __init__(self):
        self._cuenta = 0
        self._contador = 20
    
    # Esto hace referencia del metodo GET el cual controla el intercambio de inf entre un dato privado y hacerlo publico mediante un metodo creado

    @property # permite el llamado de un metodo sin los parentesis el ejemplo estara entre las lineas 19 y 20
    def cuenta(self):
        return self._cuenta
    
    @cuenta.setter #permite el llamado del metodo fuera de la clase y modificar su valor, el ejemplo estara en la linea 40
    def cuenta(self, cuenta):
        self._cuenta = cuenta

    """ ----------------------------------------------------------------------------------------------------------------------------- """
    @property # permite el llamado de un metodo sin los parentesis el ejemplo estara entre las lineas 24
    def contador(self):
        return self._contador
    
    @contador.setter
    def contador(self, contador):
     self._contador = contador


a = A()

print(a.cuenta)
a.cuenta = 15
print(a.cuenta)

print(a.contador)
a.contador = 50
print(a.contador)