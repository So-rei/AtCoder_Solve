#abc243_d

(N,X) = [str(s) for s in input().rstrip().split(' ')]
S = str(input())

xx = int(X)

cnt = len(str(bin(int(xx))))
cmin = cnt
for c in S:
    if (c == 'U'):
        cnt -= 1
        if (cnt < cmin):
            cmin = cnt
            index = i
    else
        cnt += 1
print(str(xx))
exit()