
N = int(input())
T = [0] * N
K = [0] * N
A = [[0 for i in range(200000)] for j in range(N)]
for i in range(N):
    ary = [int(s) for s in input().rstrip().split(' ')]
    T[i] = ary[0]
    K[i] = ary[1]
    A[i] = ary[2:]

def childs(x,iUsed):
    cnt = T[x-1]
    for i in range(K[x-1]):
        if not (A[x-1][i] in iUsed):
            iUsed.append(A[x-1][i])
            cnt += childs(A[x-1][i],iUsed)

    return cnt

ret = childs(N, [0])
print(ret)
