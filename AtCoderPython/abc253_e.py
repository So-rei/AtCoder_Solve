
#2022-05-28
#ABC253-e
#時間切れてから自分でやってみたけどTLE9,WA1
#(list型を改善すればいける?)
#K=0の場合もなにか事故を起こすっぽい?

#input--------------------------------------------------------
MOD = 998244353

(N,M,K) = [int(s) for s in input().rstrip().split(' ')]

#Ai=Xのとき、A1...Ai-1が何通りあるかでDPで解く
#dp[i][X] = dp[i-1][1] + ... + dp[i-1][X-K] + dp[i-1][X+K] + ... dp[i-1][M]

dp = [[0 for i in range(M+1)] for j in range(N)] 
#0番目は明らかに1～Mのどの値も1通り
for i in range(1,M+1):
    dp[0][i] = 1

i = 1
while (i < N):
    X = 1
    while (X <= M):
        #i-1番目の条件を満たす範囲を合計
        R_ed = 1 + max(0,X - K)
        R_st = min(M + 1,X + K)
        dp[i][X] = (sum(dp[i-1][:R_ed]) + sum(dp[i-1][R_st:]) ) % MOD

        X = X + 1
    i = i + 1

#output-------------------------------------------------------
ret = 0
m = 0
while (m <= M):
    ret = (ret + dp[N-1][m]) % MOD
    m = m + 1

print(ret)