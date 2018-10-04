from numpy import array as arr
class Particle:
    def __init__(self, pos, vel, mass):
        self._position = pos.copy()
        self._velocity = vel.copy()
        self._mass = mass

    @property
    def position(self):
        return self._position
    @position.setter
    def position(self, val):
        self._position = val.copy()

    @property
    def velocity(self):
        return self._velocity
    @velocity.setter
    def velocity(self, val):
        self._velocity = val.copy()

    @property
    def mass(self):
        return self._mass

    def advanceTime(self, dt):
        self._position += self._velocity * dt

    def __repr__(self):
        return f"{{r = {self._position}, " + \
            f"v = {self._velocity}, " + \
            f"m = {self._mass}}}"

if __name__ == "__main__":
    
    prtcls = [
        Particle(arr([ 1.0, 0.0, 0.0]), arr([-0.5, 0.0, 0.0]), 1.0),
        Particle(arr([ 0.0, 1.0, 0.0]), arr([ 0.0,-0.5, 0.0]), 1.0),
        Particle(arr([-1.0, 0.0, 0.0]), arr([ 0.5, 0.0, 0.0]), 1.0),
        Particle(arr([ 0.0,-1.0, 0.0]), arr([ 0.0, 0.5, 0.0]), 1.0)]

    for p in prtcls:
        print(p)

    for p in prtcls:
        p.advanceTime(0.1)
    print("\r\n")
    for p in prtcls:
        print(p)

    

