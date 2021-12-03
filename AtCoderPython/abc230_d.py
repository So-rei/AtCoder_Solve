

#未完！

(N,D) = [int(s) for s in input().rstrip().split(' ')]
lr = [0] * N

for i in range(N):
    p = [int(s) for s in input().rstrip().split(' ')]
    lr[i] = [p[0],p[1]]

cnt = 0;
while (True):
    point = min(lr, key=lambda p:p[1])[1] + D;
    for x in filter(lambda y: y[0] < point, lr):
        lr.remove(x);

    cnt += 1;
    if (len(lr) == 1):
        print(cnt);