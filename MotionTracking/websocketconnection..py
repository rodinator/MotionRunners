import socket
import random
from time import sleep


def sendRandomColors():
    s = socket.socket()
    number = "01"

    # connect to the server on local computer
    s.connect(('192.168.1.197', 1755))
    s.send((

                   str(number).encode("UTF-8")))
    s.close()


for i in range(100):
    sendRandomColors()
    sleep(1)