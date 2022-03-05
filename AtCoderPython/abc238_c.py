#2022-02-05 WR
#惜しい感じの数字になるが、%MODの処理がおかしい...何が抜けてる？
#/で精度がおかしくなったのが原因　//にしたらAC！

N = int(input())
MOD = 998244353


ret = 0
for i in range(1,len(str(N))+1):
    if (i < len(str(N))):
        ri = 10 ** i - 10 ** (i - 1)
    else:#最上桁
        ri = N - (10 ** (i - 1)) + 1

    if (ri > MOD):
        #Σn{1→ri} = Σn{1....MOD}+Σn{MOD+1...MOD*2}...Σn{...ri} =Σ{1...MOD} * _ + Σn{...ri} となる。
        #また、MODは奇数であるため、Σ{1...MOD} % MOD = 0　これより計算を省略できる
        #{1...}だけ計算する
        ri = ri % MOD
    ret += (ri * (ri + 1) // 2) #% MOD

ret = ret % MOD

print(str(int(ret)))