
class Estudiante():
    def __init__(self, Nombre , Nota):
        self.nombre = Nombre
        self.nota = Nota
    
    def Definitiva(self):
        if self.nota >3 :
            print(f" Tu Nombre es: {self.nombre} y la nota final es: {self.nota}. HAZ APROBADO FELICIDADES")
        else:
            print(f" Tu Nombre es: {self.nombre} y la nota final es: {self.nota}. HAZ Reprobado")

nombre = input(" Ingresa el nombre del estudiante: ")
nota = float(input(" Ingresa la nota del estudiante: "))

estudiante1 = Estudiante(nombre , nota)
estudiante1.Definitiva()