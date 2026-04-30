import turtle
import random

s = turtle.Screen()
s.title("Primer proyecto")
s.bgcolor("gray")

jugador1 = turtle.Turtle()
jugador2 = turtle.Turtle()


jugador1.shape("turtle")
jugador1.color("green", "green")
jugador1.shapesize(2,2,2)


jugador2.shape("turtle")
jugador2.color("blue", "blue")
jugador2.shapesize(2,2,2)

jugador1.penup()
jugador1.goto(200, 200)
jugador1.pendown()
jugador1.circle(50)

jugador1.penup()
jugador1.goto(-250,225)

jugador2.penup()
jugador2.goto(200, -200)
jugador2.pendown()
jugador2.circle(50)

jugador2.penup()
jugador2.goto(-250,-175)


turtle.done()
