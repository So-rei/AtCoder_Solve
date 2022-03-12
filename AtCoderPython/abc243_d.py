#abc243_d
#//にするのが大事！
#けどこれだとTLE

(N,X) = [str(s) for s in input().rstrip().split(' ')]
S = [c for c in str(input())]

xx = int(X)
for c in S:
    if (c == 'U'):
        xx = (xx - xx % 2) // 2
    if (c == 'L'):
        xx = xx * 2
    if (c == 'R'):
        xx = xx * 2 + 1
 
print(str(xx))
exit()

#場合分けを増やして減らせるかなーと試したけどこれでもTLE

(N,X) = [str(s) for s in input().rstrip().split(' ')]
S = [c for c in str(input())]

xx = int(X)
n = int(N)
for i in range(0,n,2):
    if (i + 1 == n):
        if (S[i] == 'U'):
            xx = int((int(xx) - int(xx % 2)) // 2)
        if (S[i] == 'L'):
            xx = int(int(xx) * 2)
        if (S[i] == 'R'):
            xx = int(int(xx) * 2 + 1)

        break

    if (S[i] == 'U' and S[i+1] == 'U'):
        xx = (xx - xx % 4) // 4
    if (S[i] == 'U' and S[i+1] == 'R'):
        xx = (xx - xx % 2) + 1
    if (S[i] == 'U' and S[i+1] == 'L'):
        xx = (xx - xx % 2)
        
    if (S[i] == 'R' and S[i+1] == 'U'):
        xx = xx
    if (S[i] == 'R' and S[i+1] == 'R'):
        xx = ((xx * 2 + 1) * 2 + 1)
    if (S[i] == 'R' and S[i+1] == 'L'):
        xx = ((xx * 2 + 1) * 2)

    if (S[i] == 'L' and S[i+1] == 'U'):
        xx = xx
    if (S[i] == 'L' and S[i+1] == 'R'):
        xx = ((xx * 2) * 2 + 1)
    if (S[i] == 'L' and S[i+1] == 'L'):
        xx = (xx * 4)

print(str(xx))
exit()


#最も葉の近い所に来た所を求めて、
#そこから計算を始めるとか？？

(N,X) = [str(s) for s in input().rstrip().split(' ')]
S = [c for c in str(input())]

xx = int(X)
n = int(N)

index = 0
cnt = len(str(bin(xx)))
cmin = cnt
for i in range(n):
    if (S[i] == 'U'):
        cnt -= 1
        if (cnt <= cmin):
            cmin = cnt
            index = i
    else:
        cnt += 1

#最も葉の近い所に来た所の葉を求める
if (index > 0):
    sabun = len(str(bin(xx))) - cmin
    xx = (xx - xx % (2 ** sabun)) // sabun

#その葉から計算を始める
for i in range(index,n):
    if (S[i] == 'U'):
        xx = int((int(xx) - int(xx % 2)) // 2)
    if (S[i] == 'L'):
        xx = int(int(xx) * 2)
    if (S[i] == 'R'):
        xx = int(int(xx) * 2 + 1)

print(str(xx))
exit()






#解説…
#2bit数値文字列としてappend("0")のほうが早い

