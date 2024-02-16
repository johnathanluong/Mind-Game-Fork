INCLUDE ../globals.ink

{memories>=8: ->TrueEnding}
{2<memories<=7:->FalseEnding}
{memories<=2: ->WorstEnding}

===FalseEnding===
You did well... #speaker: JJ  #portrait: Jester_neutral #layout: Default #audio: JesterAudio
<i>But not enough</i>
~Fin=2
->END
===TrueEnding===
Congratulations... #speaker: JJ  #portrait: Jester_neutral #layout: Default #audio: JesterAudio
~Fin=1
->END

===WorstEnding===
Hahaha... #speaker: JJ  #portrait: Jester_neutral #layout: Default #audio: JesterAudio
So this is your choice...
Fine...
<b>You chose this</b>
~Fin=0
->END