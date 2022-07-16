
#ARC144-b
#途中！わからん！

(N,a,b) = [int(s) for s in input().rstrip().split(' ')]
An = [int(s) for s in input().rstrip().split(' ')]

An.sort(reverse=True)
if (a==b):
    #a=bなら損失を考えずいくらでも平坦にならすことができるのだから、答えは{合計の平均値＋たかだか余り1回分}になるはず
    modmax = 0
    for ai in Range(An):
       modmax = max(modmax,ai % a)

    print(sum(An) // N + modmax) 

else:
    #a!=bだと...
    sa = 0
    for i in Range(1,N):
        sa += A[i-1] - A[i]
        
