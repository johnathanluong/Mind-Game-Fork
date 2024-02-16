INCLUDE ../../globals.ink
~rock=""
~tem=0
?#speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
!
!!!!!!!!!
~tem=1
 <b>SLAM</b>
 ...

 ?
Billy! Thank goodness you returned! I was worried!! #speaker:??? #audio: LAudio #layout: Default #portrait: default
+ [I'm not Billy?]
!?#speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
->NotBilly

+[Name's Hero, cut to the chase!]
>:[ #speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
O-Oh Im sorry... I ummm... #speaker:??? #audio: LAudio #layout: L_Cutscene1 #portrait: L_Cutscene1
<i>ahem</i>#speaker:??? #audio: LAudio #layout: L_Cutscene1 #portrait: L_Cutscene1
I'm L, I'll guide you to the base
If you're the kind of guy who likes it quick, then I bet you don't like getting lost
So follow the <b>red</b> torches to get there fastest
I'll help you along the way. I got your back, Hero!
~Sequence=1
->END

+[???????]
?????#speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
->confused

===NotBilly===
Not Billy? I swear even without my glasses you are the spitting image!#speaker:??? #audio: LAudio #layout: L_Cutscene1 #portrait: L_Cutscene1
He has black hair-
...
You have red hair...I think...
.-.#speaker:MC #portrait: MC_neutral_cutscene #audio: default 
+[Say you fell down a well]
.-.-.-.#speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
...#speaker:??? #audio: LAudio #layout: L_Cutscene1 #portrait: L_Cutscene1
So you fell down the <i>specific</i> well my partner was supposed to...
<i>He</i> must of gotten to him then...figures
Even so, I don't have time to explain
The name's L #speaker:L
Whats yours?
->M


===M===
...#speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
Hero? Sounds like you're the kind of person who wants to help people.#speaker: L #audio: LAudio #layout: L_Cutscene1 #portrait: L_Cutscene1
Well at the very least, I'll help you
I'll take you back to base
Zombies swarm these sewers, so stick with me and follow the <b>red</b> torches
I promise I'll protect you!
~Sequence=1
->END

===confused===
Ok, from what I can see... you must be confused.#speaker:??? #audio: LAudio #layout: L_Cutscene1 #portrait: L_Cutscene1
It's okay lost child, I shall protect you!
+[Say your name]
>:[#speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
OOH Hero! My apologies!#speaker:??? #audio: LAudio #layout: L_Cutscene1 #portrait: L_Cutscene1
Then Billy-
...
Its ok!
I'm L, and if you need <u>anything</u>, please don't feel afraid to ask#speaker:L
Just make sure to follow the <b>red</b> torches. I'll be right behind you.
~Sequence=1
->END