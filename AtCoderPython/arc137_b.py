#arc137_b

#input------------------
N = int(input())
A = [int(s) for s in input().rstrip().split(' ')]

#output-----------------
li = []

from functools import lru_cache
@lru_cache(maxsize=None)
def f(X,Y):
    if (Y == -1): return sum(A[X:])
    return sum(A[X:X+Y])

for i in range(N):
    for j in range(0,N-i+1):
       ret = f(0,i) + f(i+j,-1) + (j - f(i,j))
       if ((ret in li) == False):
           li.append(ret)

print(len(li))
exit()