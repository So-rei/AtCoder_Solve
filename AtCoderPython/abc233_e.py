#abc233_e

Xs = str(input())

#TLE12 AC7
#バカデカイ数の足し算をN回やってるから遅い?
#R = int(Xs)
#for i in range(len(Xs)):
#    X = X // 10 #計算中にfloatにならないために//記号
#    R += X


#TLE13 AC6
#バカデカイ数は扱わなくなったが、sum()をN回、繰り上がり計算をちまちまやってるから遅い?
S = [0] * (len(Xs) + 1)

#繰り上げ用関数
def Kuriage(k,sumR):
    S[k] = (sumR % 10)
    if (sumR > 9):
        Kuriage(k-1,int(S[k-1]) + 1)



for i in reversed(range(len(Xs))):
    r = sum(int(t) for t in Xs[:i+1])
    for d in reversed(range(len(str(r)))):
        sumR = S[i - d + 1] + int(str(r)[-d-1])
        Kuriage(i-d+1, sumR)

R = ""
if (S[0] != 0):#一番上の桁の0除去
    R += str(S[0])

for s in S[1:]:
    R += str(s)
print(R)