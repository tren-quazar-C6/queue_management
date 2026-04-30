
class FabricaTelefonos():
    #inicializa el programa con este constructor
    def __init__(self, marca, color):
        self.marca = marca
        self.color = color
        print(f"Se ha Creado {self.marca}")
    #modifica el mensaje de memoria
    def __str__(self):
       return " ----- el objeto es {} ----".format(self.marca)

    #destruye o borra la informacion que hay en marca despues de finalizar el programa

    def __del__(self):
      print(f"Se ha borrado {self.marca}")
      
    

    
telefono = FabricaTelefonos("nokia", "negro")

print(telefono)