INCLUDE ../../globals.ink


{ rock =="": ->choice1 | ->choiceAfter }


===choice1===
Oh don't worry about here, this is to <i>his</i> base...#speaker: L #portrait: L_neutral  #audio: LAudio
We can't get through the gate soo...
Lets keep going. If anything, I'd be sad to see my new recruit get hurt!
->END
===choiceAfter===
Now that I can see, you look exactly like him#speaker: L #portrait: L_neutral  #audio: LAudio
I'll...unlock the doorr...
!
....
..
+[L?]
? #speaker: Hero #portrait: MC_neutral2 #audio: default
S-shut it! I...#speaker: L #portrait: L_neutral  #audio: LAudio
~rock="Pushed"
It's unlocked. Lets go....
... #speaker: Hero #portrait: MC_neutral2 #audio: default
->END
+[Stay quiet]
.....#speaker: L #portrait: L_neutral  #audio: LAudio
...#speaker: L #portrait: L_neutral  #audio: LAudio
<b>SAdfedsfef!</b>#speaker: L #portrait: L_neutral  #audio: LAudio
~rock="Pushed"
...#speaker: L #portrait: L_neutral  #audio: LAudio
The door is unlocked.
Move it!!
->END