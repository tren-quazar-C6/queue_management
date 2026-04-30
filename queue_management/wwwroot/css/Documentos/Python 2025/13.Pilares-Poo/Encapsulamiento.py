
class A():
    def __init__(self):
        self.contador = 0
        
    
    def Incrementar(self):
        self.contador += 1
        print("----")

    def total(self):
        return self.contador

""" a = A()

num = int(input("ingresame un numero: "))

for i in range(num ):
    a.Incrementar()
    print(a.total()) """

# NOTA = Al colorlar los guiones bajos indica que esa variable dentro de los costructores son privados o encapsulados y no se puede acceder desde afuera de la clase
# de tal  manera que no acceder como el ejemplo de arriba, 

class B():
    def __init__(self):
        self.__contador = 0
        
    
    def Incrementar(self):
        self.__contador += 1
        print("----")

    def total(self):
        return self.__contador

b = B()
b.Incrementar()
print(b.total())
# Al colocar la cariable privada de las clases esta se encapsula y emite un error de atributos, mostrando error en consola
print(b.__contador) 