#2022-04-09
#arc138_b
#TLE...

#input------------------
N = int(input())
A =  [str(s) for s in input().rstrip().split(' ')]
AX = int(''.join(A),2) #文字列結合して2進数にする

#calc--------------------
#操作B:末尾0削除 n回
def RevB(X):
    while (X % 2 == 0):
        X //= 2
    return int(X)
#操作A
def RevA(X,head):
    #ex) 0b0010 → 7-2=5 → 0b101
    keta = (len(bin(X))-2) + head-1
    nx = (2 ** keta - 1) - X
    nhead = keta - (len(bin(nx)) - 2) #先頭に0がいくら続いているか
    return (nx, nhead)

#先頭０の数の初期値
zerohead = 0
for i in range(N):
    if (A[i] == "0"):
        zerohead += 1
    else:
        break

while (AX > 0):
    if (zerohead == 0):
        print("No")
        exit()

    AX = RevB(AX)#操作Bをできる限りB
    (AX,zerohead) = RevA(AX,zerohead)#操作Aを1回

print("Yes")