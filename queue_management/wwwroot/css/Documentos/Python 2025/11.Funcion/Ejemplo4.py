
def factura():
    num = float(input("ingresar el valor de la factura sin iva: "))
    porcentaje = int(input("Ingrese el iva: "))
    res = 0
    if porcentaje > 0:        
        res =num +( num * porcentaje /100)
        return res
    else:
        res = num + (num * 21 / 100)
        return res

    
print(factura())
