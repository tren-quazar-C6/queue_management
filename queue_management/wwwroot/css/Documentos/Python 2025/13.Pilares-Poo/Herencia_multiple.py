class A():
    def primera (self):
        return "esta es la Clase A"
    
class B():
    def segunda(self):
        return "esta es la Clase B"

class C( A, B):
    pass


c = C()

print(c.primera())
print(c.segunda())