
#2022-04-16
#ABC248-C
#AC

(N,M,K) = [int(s) for s in input().rstrip().split(' ')]
MOD = 998244353 

from functools import lru_cache
@lru_cache(maxsize=None)
def calc(n,rest): #残り回数(N→0),残りsum(K-SumA(0-(n-1))
    if (rest == 0): return 0

    if (n == 1): #最終回
        return min(rest,M)
    else:
        tmp_cnt = 0
        ptn = min(rest,M)
        i = 1
        while (i <= ptn):
            tmp_cnt += calc(n-1, rest - i) % MOD
            i += 1

        return tmp_cnt

sumall = calc(N,K) % MOD
print(sumall)