class universidades():
    def __init__(self,nombreU):
        self.nombreU = nombreU

class Carrera():
    def carrera(self,especialidad):
        self.especialidad = especialidad

class Estudiante():
    def estudiante(self,nombre, edad):
        self.nombre = nombre
        self.edad = edad

class Imprimir(universidades, Carrera , Estudiante):
    
    
    def imprimir(self):
       print(f' El nombre del estudiante es: {self.nombre}\b La edad es: {self.edad}\b La especialidad es: {self.especialidad}\b La Universidad es: {self.nombreU}')


persona = Imprimir("Inedinco",29,"tecnologia", "yonathan")


persona.imprimir()


""" //////////////////////////// """
""" 
class Universidad():
    def __init__(self,nombre):
        self.nombre_universidad = nombre
    
class Carrera():
    def __init__(self, especializacion):
        self.especializacion = especializacion

class Estudiante():
    def __init__(self, nombre , edad):
        self.nombre = nombre
        self.edad = edad


class Imprimir(Universidad, Carrera, Estudiante):
     
     def __init__(self, nombre_universidad, nombre, edad, especializacion):

        Universidad.__init__(self, nombre_universidad)
        Carrera.__init__(self, especializacion)
        Estudiante.__init__(self, nombre, edad)

    
     def imprimir(self):
        print(f'''
              
        Estudiante: {self.nombre}
        Edad: {self.edad}
        Especialidad: {self.especializacion}
        Universidad: {self.nombre_universidad}
        
        ''')


persona = Imprimir("Inedinco", "Yonathan", 29, "Tecnología")
persona.imprimir() """