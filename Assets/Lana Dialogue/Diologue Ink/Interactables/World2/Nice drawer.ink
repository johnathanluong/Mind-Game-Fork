INCLUDE ../../globals.ink
{Sequence>=6: ->NotDefault}
{Sequence<6: ->default}
===default===
Its a <i>nice</i> drawer
will you open it?
+[yes]
There in nothing inside
At least it looks nice.
->END
+[no]
->END

===NotDefault===
Its a <i>nice</i> drawer
will you open it?
+[yes]
!#speaker: Hero #portrait: MC_neutral2 #audio: default
There in nothing inside
At least it looks nice.
->END
+[no]
->END
+[Make L open it]
...#speaker: Hero #portrait: MC_neutral2 #audio: default
...fine. But don't expect me to steal anything! #speaker: L #portrait: L_neutral  #audio: LAudio
hmmm
Nothing, just dissapointment.
Like you 
^-^
.-.-.-.-.-.-.-. #speaker: Hero #portrait: MC_neutral2 #audio: default
->END