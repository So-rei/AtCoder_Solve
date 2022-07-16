#ARC144-a
#AC 17分!

N= int(input())
     
#xがN桁の111....111 だったら、f(x)=N、 f(2x)=2N　になる　これが明らかに最大 M=2N
print(N * 2)
     
#xを探す...
#Nが十分大きい時、最小は明らかに444...4(Nによっては最初の桁が3,2,1)
if (N < 4):
    print(N)
else:
    s = ""
    if (N % 4 > 0):
        s += str(N % 4)
        N -= N % 4
     
    s += ''.join([str(4) for _ in range(N // 4)])
    print(s)
