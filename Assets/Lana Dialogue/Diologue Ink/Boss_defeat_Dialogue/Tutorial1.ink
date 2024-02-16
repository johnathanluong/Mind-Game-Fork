INCLUDE ../globals.ink

{SUPER ==1: ->TutorialMean}
{Pissed ==0: ->TutorialNice}
{Pissed !=0: ->TutorialNormal}


===TutorialNice===
Thank you Hero for listening to me! #speaker: Tutorial Guy #portrait: #portrait: Tutorial_Guy_neutral
But Gary... you ru- #speaker: Tutorial Guy #portrait: #portrait: Tutorial_Guy_neutral
PUNCH HIM TO DEATH NOW! #speaker: ??? #portrait: Gary_neutral  #audio: GaryAudio
+[Punch to death?]
!#speaker: Hero #portrait: MC_neutral #audio: default
Wait no-#speaker: Tutorial Guy #portrait: #portrait: Tutorial_Guy_neutral
~Complete=1
->END
===TutorialMean===
Neither of you listen to what I say! #speaker: Tutorial Guy #portrait: #portrait: Tutorial_Guy_neutral
I'll...
<b>I'll be back</b>
~Complete=1
->END

===TutorialNormal===
You... #speaker: Tutorial Guy #portrait: #portrait: Tutorial_Guy_neutral
GAAARRRYYYYYYYYYYYYYYYYY
~Complete=1
->END