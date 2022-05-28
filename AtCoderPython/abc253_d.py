
#2022-05-28
#ABC253-d
#AC

#Ans = 総和 - Aの倍数 - Bの倍数 + (A,Bの公倍数)

import math

(N,A,B) = [int(s) for s in input().rstrip().split(' ')]

X = (1+N) * N // 2

rA = N // A
rB = N // B

lcm = A * B // math.gcd(A, B) #math.lcm(A,B) 
rAB = N // lcm

if (rA < X):
    X = X - A * ((1+rA) * rA // 2)
if (rB < X):
    X = X - B * ((1+rB) * rB // 2)
if (lcm < X):
    X = X + lcm * ((1+rAB) * rAB // 2)

print(X)