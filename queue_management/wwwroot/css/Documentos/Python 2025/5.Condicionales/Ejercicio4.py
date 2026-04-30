"""
Crear un programa que permita al usuario elegir un candidato por el cual votar. 
Las posibilidades son: candidato A por el partido rojo, candidato B por el partido verde, candidato C por el partido azul. 
Según el candidato elegido (A, B ó C) se le debe imprimir el mensaje “Usted ha votado por el partido [color que corresponda al candidato elegido]”. 
Si el usuario ingresa una opción que no corresponde a ninguno de los candidatos disponibles, indicar “Opción errónea”.
Pista: Si la letra ingresada por el usuario es en minúscula, el programa debe convertirla en mayúscula
"""
print("Candidato A es del partido Rojo, Candidato B es del partido Verde, Canditado C es por el partido azul")

Candidato = input(" Vote por su partido favorito teniendo en cuenta la información anterior:  ")

if Candidato.upper() in "ABC":
    if Candidato.upper() == "A":
        print("Haz votado por el partido Rojo....")
    elif Candidato.upper() == "B":
        print("Haz votado por el partido Verde....")
    else:
        print("Haz votado por el partido Azul....")


