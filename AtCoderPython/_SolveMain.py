
#abc247_d

Q = int(input())
qu = []
for i in range(Q):
    qu.append([int(s) for s in input().rstrip().split(' ')])

#solve
bolls = [] #x,c
j = 0
for i in range(Q):
    if (qu[i][0] == 1): #�ǉ�
        bolls.append([qu[i][1],qu[i][2]])
    else:
        c = qu[i][1]
        sumx = 0

        #bolls��������Ă���
        while (c > 0):
            if (bolls[j][1] <= c):
                sumx += bolls[j][0] * bolls[j][1]
                c -= bolls[j][1]
                j += 1 #�Ȃ��Ȃ����z��������Ȃ�
            else:
                sumx += bolls[j][0] * c
                bolls[j][1] -= c
                break

        print(sumx)

exit()