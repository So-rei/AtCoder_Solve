
#ABC255-c

#AC
#途中細かい誤記でRE,WAを3回も出してしまった…D!=0大事。

(X,A,D,N) = [int(s) for s in input().rstrip().split(' ')]

Smin = min(A, A + D * (N - 1))
Smax = max(A, A + D * (N - 1))

if (Smin <= X and X <= Smax and D != 0):
    #完全一致
    if (X % D == A % D):
        print(0)
        exit()

    else:
        ret = min(abs(abs(X % D) - abs(A % D)), abs(D) - abs(abs(X % D) - abs(A % D)))
        print(ret)
        exit()

else:
    print(min(abs(Smin - X),abs(Smax - X)))
    exit()