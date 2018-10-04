import unittest
import Procedural as pt

class BasicTests(unittest.TestCase):
     
    @unittest.skip("Relying on skipping")
    def test_Fail(self):
        self.fail("Should not happen")


if __name__ == '__main__':
    unittest.main()
    