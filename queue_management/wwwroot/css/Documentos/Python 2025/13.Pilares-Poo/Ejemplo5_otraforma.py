
class Estudiante():
    def estudiante(self,nombre, edad):
        self.nombre = nombre
        self.edad = edad

class Universidad():
    def universidad(self,nombreU):
        self.nombreU = nombreU

class Carrera():
    def carrera(self,especialidad):
        self.especialidad = especialidad


class Imprimir(Estudiante, Universidad, Carrera ):
     # Universidad.__init__ se llama primero por el MRO
 
   def __init__(self,  nombre, edad,nombreU, especialidad):
        self.nombre = nombre
        self.edad = edad
        self.nombreU = nombreU
        self.especialidad = especialidad
        
   def imprimir(self):
        print(f'''
        Estudiante: {self.nombre}
        Edad: {self.edad}
        Universidad: {self.nombreU}
        Especialidad: {self.especialidad}
        
        
        ''')


persona = Imprimir("yonathan",29,"Americana","desarollo")
persona.imprimir()