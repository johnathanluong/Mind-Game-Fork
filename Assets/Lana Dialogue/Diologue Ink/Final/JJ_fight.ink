INCLUDE ../globals.ink
{memories>=8: ->TrueEnding}
{2<memories<=7:->FalseEnding}
{memories<=2: ->WorstEnding}

===FalseEnding===
YES, Yes finish me! DO IT! #speaker: JJ  #portrait: Jester_neutral #layout: Default #audio: JesterAudio
Then you can go, Hero
//~Fin=2
->END
===TrueEnding===
I'm not satisifed yet... no.... no no you can't #speaker: JJ  #portrait: Jester_neutral #layout: Default #audio: JesterAudio
<b>I won't let you!</b>
//~Fin=1
->END

===WorstEnding===
oh goodness...please defeat me... #speaker: JJ  #portrait: Jester_neutral #layout: Default #audio: JesterAudio
I am rather excited...
Hero...
//~Fin=0
->END