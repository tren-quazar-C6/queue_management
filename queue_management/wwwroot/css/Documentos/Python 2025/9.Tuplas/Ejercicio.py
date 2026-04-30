
meses = ('enero','febrero','marzo','abril','mayo','junio','julio','agosto','septiembre','octubre','noviembre','diciembre')

numero_Ingresado = int(input("ingresa un numero del 0 al 11: "))
numero_Ingresado -= 1
mes = meses[numero_Ingresado]

if mes:
    print(f'El numero ingresado es: {numero_Ingresado} y ese MES igual a: {mes}')
else:
    print("el mes no existe")