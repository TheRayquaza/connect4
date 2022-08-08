
SRC = $(find ./src/ *.cs)
OUT = game.exe

all :
	mcs ${SRC} -out:${OUT} 

clean :
	rm build/game.exe
