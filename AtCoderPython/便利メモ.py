#f(x)の値を児童でメモしてくれるツール
#以下例

from functools import lru_cache

MOD = 998244353
 
@lru_cache
def f(X):
    if X <= 4:
        return X
    X1 = X // 2
    X2 = (X + 1) // 2
    return f(X1) * f(X2) % MOD
 
X = int(input())
print(f(X))

###############################################