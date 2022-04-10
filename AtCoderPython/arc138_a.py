#2022-04-09
#arc138_a
#TLEと一部WA TLE21WA2AC13

#input------------------
(N,K) = [int(s) for s in input().rstrip().split(' ')]
A =  [int(s) for s in input().rstrip().split(' ')]

#calc--------------------

#不可能
samin = min(A[0:K])
if (max(A[K:]) <= samin):
    print(-1)
    exit()

#可能
score = N

#TLE15/WA2/AC19
#単純に以下だと毎回逆順で配列にアクセスするので遅い
#for i in range(K,N):
#    if (i - K > score): break
#    if (A[i] > samin):
#        for k in range(K-1,-1,-1): #ここ0までになっていたのがWAの原因
#            if (A[k] < A[i]): #A[k]<A[i]となる(i,k)の中で、(i-k)が最も小さくなる組み合わせを探す
#                score = min(score,i-k)
#                break

#------------------------------------------------------------------
#K番目までの内容を比較速度のため値・indexでソートして...
#同様にループ...
#でもこれでも遅い AC21/TLE15
AI = []
tmpmin = A[K-1]
for i in range(K-1,-1,-1):
    if (A[i] > tmpmin):
        continue #スコアがでかいのは不要
    AI.append([i,A[i]])

AIsorted = sorted(AI, key = lambda x:(-x[0],x[1]))
for i in range(K,N):
    if (i - K > score): break
    if (A[i] > samin):
        for ai in AIsorted:
            if (ai[1] < A[i]):
                score = min(score, i - ai[0])
                break


print(score)


