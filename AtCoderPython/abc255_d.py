

#ABC255-d

#input--
(N,Q) = [int(s) for s in input().rstrip().split(' ')]
A = [int(s) for s in input().rstrip().split(' ')]
X = [0] * Q
for i in range(Q):
    X[i] = int(input())

#calc--
A.sort()
aSum = sum(A)

#TLE15
for i in range(Q):
    r = aSum - X[i] * N
    j = 0
    while (j < N and A[j] < X[i]):
        r += (X[i] - A[j]) * 2
        j += 1
    print(r)

exit()


#メモ見て短縮バージョン
#全然早くなってない。TLE16
memo = []
memo.append((0,aSum))
#memo.append((max(A),max(A) * N - aSum))

for i in range(Q):
    nearR = X[i]
    t = 0
    for tt in range(len(memo)):
        if (X[i] > memo[tt][0] and nearR > (X[i] - memo[tt][0])):
            nearR = (X[i] - memo[tt][0])
            t = tt          
    nmemo = memo[t]

    r = nmemo[1] - nearR * N
    for j in range(N):
        #反転済みのものはそのまま伸ばす
        if (A[j] < nmemo[0]):
            r += nearR * 2
        #今回反転するものは反転するぶんを数える
        elif (A[j] < X[i]):
            r += (X[i] - A[j]) * 2
        else:
            break

    print(r)
    memo.append((X[i],r))

exit()


#解説を見るに、二分探索が早いようだった...