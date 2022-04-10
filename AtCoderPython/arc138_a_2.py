#2022-04-09
#arc138_a
#答えを見て作成....


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

#Kまでのから見たほうが早い？？
mi = float('inf')
for i in reversed(range(K)):
    if mi>A[i]:
        mi = A[i]
    else:
        A[i]=-1

end = K
for i in range(K):  
  if not A[i] == -1:
    for j in range(end,N): #endから始めることができるので、これで計算量が一気に減る　これをしないとAC24/TLE12
      if A[i]<A[j]:
        score = min(score,j-i)
        end = j 
        break

print(score)
exit()
