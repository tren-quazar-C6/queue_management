#Creo una clase fabrica que indica lo que se va a realizar
class FabricanteTelefonos():
    
    marca = "samsung" # se le da un valor inicial o por defecto  "metodo"
    
    def ElaborarHuawei(self): # se crea una funcion interna o metodo en la cual sera una nueva caracteristica a lo q se creara a nivel general osea otra marca de celulares
       self.marca = "Huawei"

    def ElaborarXiaomi(self): # se crea una funcion interna o metodo en la cual sera una nueva caracteristica a lo q se creara a nivel general osea otra marca de celulares
      self.marca="Xiaomi"


telefono = FabricanteTelefonos()  #se crea un objeto que indica el producto del cual tomará los valores de la clase

print(telefono.marca) # se imprime la marca por defecto, en este caso "samsung"
"------------------"
telefono.ElaborarHuawei() # se inicializa nuevamente el objeto y se ejecuta el metodo nuevo que seria HUAWEI para solicitar a la clase un nuevo "movil" de otra marca

print(telefono.marca) # se imprime el nuevo valor del objeto con su respectiva marca el cual es "HUAWEI"
"------------------"

telefono.ElaborarXiaomi() # el objeto solicita a la clase una nueva referencia o marca de movil,accede al metodo para obtener el valor de "XIAOMI"

print(telefono.marca) # imprime el nuevo valor del objeto


