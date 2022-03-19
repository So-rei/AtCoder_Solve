#f(x)の値を自動メモしてくれるツール
#以下例

from functools import lru_cache

MOD = 998244353
 
@lru_cache(maxsize=None)
def f(X):
    if X <= 4:
        return X
    X1 = X // 2
    X2 = (X + 1) // 2
    return f(X1) * f(X2) % MOD
 
X = int(input())
print(f(X))

###############################################

# 割り算を / ではなく // ですることは大事(intで計算してくれる)

x = [0,1,2,3]
x.pop() #pop位置指定しない場合は最後を削除
print(x) #[0,1,2]
x.pop(1)
print(x) #[0,2]