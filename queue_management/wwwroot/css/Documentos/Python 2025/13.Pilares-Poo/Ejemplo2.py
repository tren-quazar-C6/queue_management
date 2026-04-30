class Calculadora():
    def __init__(self, numero1, numero2):
        self.numero1 = numero1
        self.numero2 = numero2
    
    def Suma(self):
        print(f"La suma es: {self.numero1} + {self.numero2} = { self.numero1 + self.numero2}")
    
    def Resta(self):
        print(f"La resta es: {self.numero1} - {self.numero2} = { self.numero1 - self.numero2}")
    
    def Multiplicacion(self):
        print(f"La multiplicacion es: {self.numero1} x {self.numero2} = { self.numero1 * self.numero2}")
    
    def Division(self):
        print(f"La division es: {self.numero1} / {self.numero2} = { self.numero1 / self.numero2}")

num1 = int(input("Ingrese el primer numero a calcular: "))
num2 = int(input("Ingrese el segundo numero a calcular: "))

calc = Calculadora( num1 , num2)   
calc.Suma()
calc.Resta()
calc.Multiplicacion()
calc.Division()
