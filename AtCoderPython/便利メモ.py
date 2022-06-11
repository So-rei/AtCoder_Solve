# longやdoubleが混ざる可能性がある時、割り算を / ではなく // ですることは大事(intで計算してくれる)

#よくあるinput集---------------------------------------------------------------------------------------
#1行1数値基本
N = int(input())
#1行複数数値
(a,b,c) = [int(s) for s in input().rstrip().split(' ')]
#1行複数数値はこちらでも可
N, M, K, S, T, X = map(int, input().split())
#複数行1数値
D = [0] * N
for i in range(N):
    D[i] = int(input())
#または
D = []
for i in range(N):
    D.append(int(input()))
#複数行複数値--通常
E = [] * N
for i in range(N):
    E.append([int(s) for s in input().rstrip().split(' ')])
#複数行複数値--名称をつけたい場合
m = [0] * N
s = [0] * N
for i in range(N):
    (m[i],s[i]) = [int(s) for s in input().rstrip().split(' ')]
#複数行複数値--サイズを明示的に決めたい場合
tc = [[0 for i in range(2)] for j in range(N)] 
for i in range(N):
    tm = [s for s in input().rstrip().split(' ')]
    (tc[i][0],tc[i][1]) = (tm[0],tm[1])

#よくあるoutput集--------------------------------------------------------------------------------------
ary = ["a","b","c"]
#複数行に出したい #a,b,cの3行出力される
for i in ary:
    print(i)
#1行にスペース入れて出したい
print(" ".join(ary)) #a b c
#インデックスを返す(0始まり)
print(ary.index("b")) #1
print(len(ary)) #配列の長さ 3

iary = [3,2,1]
print(max(iary) + min(iary)) #最大3,最小1

#for文について-----------------------------------------------------------------------------------------
#for文はc#でいうところのforeachにちかい。IEnumerableであれば利用できる
for str in ["a","b","c"]:
    print(str)

#カウンタ取得はrange()
#1..10と出力される
for i in range(10):
    print(i)

#配列関係----------------------------------------------------------------------------------------------
x = []              #基本宣言
y = [] * 100        #サイズ指定宣言
z = [0] * 5         #初期値を指定した宣言(数字
z = ["default"] * 5 #初期値を指定した宣言(文字

#forとかでイテレーターを取ってくるだけで雑に配列になる
c = [t for t in range(3)]

#適当に配列宣言して好き勝手に追加とかできる
ST = []
for i in range(N):
    ST.append(input().rstrip().split(' '))

#複数まとめた簡易タプルがある
#左から順のイテレーターと判断してくれるので下記みたいな挿入ができる
#※配列の数<=名前の数であること（多すぎる場合は残りの名前に何も入らないだけで動く）
(a,b,c) = [int(s) for s in input().rstrip().split(' ')]

#簡易タプルと配列を組み合わせるとこんなこともできる

#名前付きタプルをきちんと実装するならこう
import collections #宣言必須
Point = collections.namedtuple('Point', ['x', 'y'])# named tupleの定義
p1 = Point(10, 20)# インスタンスは省略もできる
p2 = Point(x=30, y=40) #名前付きインスタンスセット
    
#dictionaryは{}で宣言、setdefault(key,value)で登録
SNandCnt = {}
for i in range(N):
    SNandCnt.setdefault(s[i],0)

#二次元配列の宣言 {[0,0,0]... x N個}
gsb = [[0 for i in range(3)] for j in range(N)]
for i in range(N):
    gsb[i] = [int(s) for s in input().rstrip().split(' ')]

#配列をくっつけて1つの文字列にする
print('-'.join(['a','b','c'])) #'-'は区切り文字

len([0,1,2]) #3

#配列のディープコピー
import copy
h = [1,2,3]
h2 = copy.deepcopy(h)
h2[0] = 100
print(h[0]) #h[0] = 1のまま

#削除
x = [0,1,2,3]
x.pop() #pop位置指定しない場合は最後を削除
print(x) #[0,1,2]
x.pop(1)
print(x) #[0,2]

#日付時刻処理-------------------------------------------------------------------------------------------------------
import datetime
dt1 = datetime.datetime(2021,11,1,8,59)
dt1 += datetime.timedelta(minutes=10)
i = dt1.minute #取る時はminuteなので注意

#その他--------------------------------------------------------------------------------------------------------------
str(2).zfill(3) #0パティング →"002"と出力
exit() #処理を終わる時
i = 2 ** 5#階乗 →2の5乗

#関数
def hoge(i,j,k):
    return i+j+k

#ラムダ式でソートルールを記述 t = gsb.OrderBy(t=> -t[0]).ThenBy(t => -t[1]).ThenBy(t => -t[2])と等価
cLi = sorted(gsb, key = lambda x:(-x[0],-x[1],-x[2]))
#c#じゃ絶対１行にするの無理そうな処理を１行にできる(ソート+イテレーターで取ってループ出力)
[print(str(c[0]) + " " + str(c[1]) + " " + str(c[2])) for c in cLi]



#関数の計算結果のキャッシュを覚えてくれるlru_cache(dp問題などに転用できる)------------------------------------------------
#実装例-
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
#----------------------------------------------------------------------------------------------------------------------

#二分探索
#数値配列A(ソート済)から、数値Nの位置(Ak<N<=Ak+1となるk)を探索する
def searchdiv(cA,cN):
    cst = 0
    cfi = len(cA)- 1
    while(cst<=cfi):
        tmp = (cst+cfi) // 2
        if (cA[tmp] < cN):
            cst = tmp + 1
        else:
            cfi = tmp - 1

    return cst-1

