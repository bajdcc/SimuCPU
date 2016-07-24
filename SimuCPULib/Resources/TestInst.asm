nop
mov ah,02h
mov dl,'0'
.LOOP
int 21h
inc dl
#jmp loop
halt