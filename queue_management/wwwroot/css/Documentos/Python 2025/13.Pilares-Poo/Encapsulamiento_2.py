
class A():
    def __init__(self):
        self._contador = 0
    
    def incrementar(self):
       self._contador +=1 

    def cuenta(self):
       return self._contador

a = A()

print(a.cuenta)
a._cuenta =20
print(a._cuenta)
#no se permite hacer así ya que o muestra un error o indica que son valores privador o protegidos
