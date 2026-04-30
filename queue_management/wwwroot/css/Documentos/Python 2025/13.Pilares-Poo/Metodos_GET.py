
class A():
    def __init__(self):
        self._cuenta = 0
        self._contador = 0
    
    # Esto hace referencia del metodo GET el cual controla el intercambio de inf entre un dato privado y hacerlo publico mediante un metodo creado

    @property # permite el llamado de un metodo sin los parentesis el ejemplo estara entre las lineas 19 y 20
    def cuenta(self):
        return self._cuenta
    
    @property # permite el llamado de un metodo sin los parentesis el ejemplo estara entre las lineas 24
    def contador(self):
        return self._contador



a = A()

#rint(a._cuenta)
print(a.cuenta)

print(a.contador)

""" a._cuenta = 10
print(a._cuenta) """
