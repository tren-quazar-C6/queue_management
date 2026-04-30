#Se desea tener un algoritmo que permita determinar y mostrar el promedio que ha obtenido un alumno en un determinado curso, conociendo las notas de: 
#tres prácticas, el examen parcial y el examen final.

#Considere:
#Donde: P1, P2, P3 : Practicas
#PP: promedio de práctica
#PROM: promedio
#EP: examen parcial
#EF: examen final

P1 = int(input("ingrese primer practica"))
P2 = int(input("ingrese segunda practica"))
P3 = int(input("ingrese tercera practica"))

EP = int(input("ingrese examen parcial :" ))
EF = int(input("ingrese el examen final :" ))


PP = ( P1 + P2 +P3 ) / 3 
PROM = ( PP + 2*EP + 3*EF ) / 6

print("promedio final es: ", PROM )
print( " el promedio de practica es ", PP )

