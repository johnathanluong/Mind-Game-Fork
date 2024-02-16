INCLUDE ../globals.ink
{SUPER ==1: ->TutorialMean}
{Pissed ==0: ->TutorialNice}
{Pissed !=0: ->TutorialNormal}

===TutorialNice===
I hope Gary falls down a well #speaker: Tutorial Guy #portrait: #portrait: Tutorial_Guy_neutral
~tem=0
~People=1
->END
===TutorialMean===
GOD DAMN IT! #speaker: Tutorial Guy #portrait: #portrait: Tutorial_Guy_neutral
~tem=0
~People=1
->END

===TutorialNormal===
You... #speaker: Tutorial Guy #portrait: #portrait: Tutorial_Guy_neutral
GAAARRRYYYYYYYYYYYYYYYYY!
~tem=0
~People=1
->END