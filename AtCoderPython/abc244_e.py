
#abc244_e
#AC14 TLE15 RE9

#input------------------
(N,M,K,S,T,X) = [int(s) for s in input().rstrip().split(' ')]

U = [0] * M
V = [0] * M
for i in range(M):
    (U[i],V[i]) = [int(s) for s in input().rstrip().split(' ')]

MOD = 998244353

#output-----------------
uvx = [[] for _ in range(N+1)]
for i in range(M):
    uvx[U[i]].append(V[i])
    uvx[V[i]].append(U[i])

def GetNext(s,cnt,xcnt,chr):

    #最後
    if (cnt == K-1):
        if (xcnt % 2 == 0):
            for i in range(M):
                if (T in uvx[s]):
                    return 1
        return 0
    
    #最後以外
    r = 0
    for c in uvx[s]:
        xx = 0
        if (c==X):
           xx = 1
        r += GetNext(c,cnt+1,xcnt+xx,str(chr) + str(c)) % MOD
    return r

ret = GetNext(S,0,0,str(S)) % MOD

print(ret)

