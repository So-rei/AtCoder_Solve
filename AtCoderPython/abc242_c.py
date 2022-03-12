#2022-03-06
#AC 
#1ずつ減らすとキャッシュが足りないので、2^nで減らそうとして、こんがらがってしまって2時間ぐらいかかった、、、勿論時間切れ。

from functools import lru_cache

N = int(input())
MOD = 998244353

#ex.2桁の時、11,12,21,22,23,...の25種類
 
@lru_cache(maxsize=None)
def Rx(digit,ia, iz):
	#計算量減らし用
	if (ia > 5):
		ia = 10 - ia
		iz = 10 - iz

	if (digit == 0):
		return 1
	elif (digit == 1):
		return 1
	elif (digit == 2):
		if (ia - iz > -2 and ia - iz < 2):
			return 1
		else:
			return 0
	elif (digit == 3):
		if (ia - iz == -2 or ia - iz == 2):
			return 1
		elif (ia - iz == -1 or ia - iz == 1):
			return 2
		elif (ia == iz):
			if (ia != 1 and ia != 9):
				return 3
			else:
				return 2
		else:
			return 0
	elif (digit < 9):
		if (ia - iz < -digit or ia - iz > digit):
			return 0

	#通常の計算
	#ex. Rx(10, ..) = Rx(8, ..) + Rx(3, ..)

	keta_next = len(bin(digit-1)[2:])
	
	r = 0
	for t in range(1,10):
		r = r + (Rx(2 ** (keta_next-1),ia,t) * Rx(digit + 1 - 2 ** (keta_next-1),t,iz)) % MOD

	return r % MOD

cnt = 0
for i in range(1,10):
	for j in range(1,10):
		cnt = (cnt + Rx(N,i,j)) % MOD

print(cnt)
		


