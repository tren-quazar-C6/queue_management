import turtle

s= turtle.Screen() # inicializa la pantalla
t = turtle.Turtle() #funcion que permite modificar las coordenadas del puntero

t.circle(40)
t.circle(45)
t.circle(50)

t.dot(55) # da un giro y luego lo pinta en negro, por lo general lo muestra desde la posicion (0,0)

t.speed(5) # se usa del 1 al 10
t.hideturtle() # se usa para desaparecer el puntero
t.circle(75)
t.showturtle() # se usa para aparecer el puntero
t.circle(90)

t.setx(80)# indica que vaya cierta distancia en el eje X
t.sety(-30)

turtle.done() # evita el cierre del la pantalla

