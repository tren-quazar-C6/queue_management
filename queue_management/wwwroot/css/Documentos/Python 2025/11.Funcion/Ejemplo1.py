Lista =[]
num=0

def PedirNum():
    for i in range(5):
        num= int(input("ingresar numero: "))
        Lista.append(num)

def Ordenar():  
    Lista.sort()
    Listapar = []
    ListaImpar =[]
    for i in Lista:    
        if i % 2 == 0:
            Listapar.append(i)
        else:
             ListaImpar.append(i)
    print(Listapar)
    print(ListaImpar)

PedirNum()
Ordenar()