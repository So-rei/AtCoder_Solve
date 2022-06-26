

#2022-06-26
#arc143-b
#時間切れ。
#試しに作っては見たけど遅すぎ。配列のcopyが重い...と思う
#AC2/TLE13/RE1

import copy

N = int(input())
MOD = 998244353 

#def CalcMain(s,ary, i,j, rowmin, rowindex, colmax):
def CalcMain(ary, i,j, rowmin, rowindex, colmax):
   #最終マスsum
    if (i == N-1 and j == N-1): 
        colmax[N-1] = max(colmax[N-1], ary[0])       
        if rowmin[i] > ary[0]:
            rowmin[i] = ary[0]
            rowindex[i] = i
            
        for x in range(N):
            #x行目で、行条件を満たしていない唯一のセルが
            #列条件も満たしていない場合、却下する
            if (colmax[rowindex[x]] == rowmin[x]):
                #print(rowmin[0],rowmin[1],colmax[0],colmax[1])
                #print("NG" + s + str(ary[0]))
                return 0

        #print("OK" + s + str(ary[0]))
        return 1
    else:
        r = 0

        ni = i
        nj = j
        if (j != N-1):
            nj += 1
        else:
            ni += 1 #列終わり、改行
            nj = 0
            #s += "\n"

        for v in ary:
            #この段階ではまだ判断できない。(次のrowや次の列のcolで満たすかもしれない)
            tmp_ary = copy.deepcopy(ary)
            tmp_ary.remove(v)
                                
            tmp_colmax = copy.deepcopy(colmax)           
            tmp_colmax[j] = max(colmax[j], v)

            if rowmin[i] > v:
                tmp_rowmin = copy.deepcopy(rowmin)
                tmp_rowindex = copy.deepcopy(rowindex)
                tmp_rowmin[i] = min(rowmin[i], v)
                tmp_rowindex[i] = j

                #r += CalcMain(s + "," + str(v),tmp_ary, ni, nj, tmp_rowmin, tmp_rowindex, tmp_colmax)
                r += CalcMain(tmp_ary, ni, nj, tmp_rowmin, tmp_rowindex, tmp_colmax)
            else:
                #r += CalcMain(s + "," + str(v),tmp_ary, ni, nj, rowmin, rowindex, tmp_colmax)
                r += CalcMain(tmp_ary, ni, nj, rowmin, rowindex, tmp_colmax)

        return r
    
#まだ使用していないマス値
mapping = [0] * (N * N)
i = 0
while (i < N * N):
    mapping[i] = i + 1
    i += 1
#i行目の行minを記憶する
row = [250001] * N
#i行目の行minとなったのが何列目かを記憶する
rindex = [0] * N
#j列目の列maxを記憶する
col = [0] * N

#最終計算結果
#ret = CalcMain("",mapping,0,0,row,rindex,col) % MOD
ret = CalcMain(mapping,0,0,row,rindex,col) % MOD

print(ret)