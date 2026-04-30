import turtle

s = turtle.Screen() # inicializa la pantalla
t = turtle.Turtle() #funcion que permite modificar las coordenadas del puntero


s.bgcolor("beige") # modifica el color de la view
s.title("Proyecto 1") # coloca el titulo a la ventana

t.shapesize(2,2,2) # modifica el tamaño del puntero en el orde de alto, largo y borde o relleno

#cambia el fondo del puntero, sin embargo depende de como se tenga el borde o relleno ya que si esta muy alto este se satura o solapa con el color el ejemplo tomado es (2,2,40)
t.fillcolor("orange")
t.speed(2)
t.bk(300)
t.rt(10)

t.pencolor("red") # cambia el color de la linea
t.color("black","green")
t.speed(2)
t.fd(200)

t.pensize(10)
t.rt(50)
t.fd(60)



turtle.done() # evita el cierre del la pantalla
