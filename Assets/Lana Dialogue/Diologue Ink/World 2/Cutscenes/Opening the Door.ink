INCLUDE ../../globals.ink
~tem=0
So, this door...#speaker:L #audio: LAudio #layout: Cutscene #portrait: L_Cutscene2
Look at me
...#speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
Yep same hair... just a couple things missing. It should work.#speaker:L #audio: LAudio #layout: Cutscene #portrait: L_Cutscene2
Go up to the door
+[Go up to the door]
->door
...#speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
~tem=1
'ding'#speaker:??? #audio: default #portrait: default #layout: Default
Perfect!#speaker:L #audio: LAudio #layout: Cutscene #portrait: L_Cutscene2
Let's go!
~Sequence = 7
->END
+[Don't go up to the door]
...#speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
.-.#speaker:L #audio: LAudio #layout: Cutscene #portrait: L_Cutscene2
->stubborn1
===door===
...#speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
~tem=1
'ding'#speaker:??? #audio: default #portrait: default #layout: Default
Perfect!#speaker:L #audio: LAudio #layout: Cutscene #portrait: L_Cutscene2
Let's go!
~Sequence = 7
->END

===stubborn1===
+[Relent and go to door]
->door
+[Stand your ground]
.-.#speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
<u>I don't have time for this</u>#speaker:L #audio: LAudio #layout: Cutscene #portrait: L_Cutscene2
->stubborn2

===stubborn2===
.-.#speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
<u>L begins walking up to you</u>#speaker:??? #audio: default #portrait: default #layout: Default
+[don't budge]
!!!!#speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
._.#speaker:L #audio: LAudio #layout: Cutscene #portrait: L_Cutscene2
->stubborn3
+[go to the door]
->door

===stubborn3===
<u>L gets out a gun. What do you do? </u>#speaker:??? #audio: default #portrait: default #layout: Default
+[GO TO THE DOOR]
->door
+[Glare]
]:< #speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
I need this Hero.#speaker:L #audio: LAudio #layout: Cutscene #portrait: L_Cutscene2
Move it
->stubborn4

===stubborn4===
<u>L cocks her glock. What do you do? </u>#speaker:??? #audio: default #portrait: default #layout: Default
+[THE DOOOOOOR!!!]
->door
+[walk towards her]
!!!!! #speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
Fine #speaker:L #audio: LAudio #layout: Cutscene #portrait: L_Cutscene2
->stubborn5

===stubborn5===
<u>L puts her gun away </u>#speaker:??? #audio: default #portrait: default #layout: Default
<u>L shoves you towards the door </u>#speaker:??? #audio: default #portrait: default #layout: Default
<u>You are now at the door </u>#speaker:??? #audio: default #portrait: default #layout: Default
.-.-.-.-.-.-.-.-.-.-. #speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
->door




