def Greet(name):
    print( f"Hello, {name}!")

def GenerateNames():
    yield "Alex"
    yield "Bob"
    yield "Eve"

def GreetAll():
    for nm in GenerateNames():
        Greet(nm)

if __name__ == "__main__":
    GreetAll()   
    