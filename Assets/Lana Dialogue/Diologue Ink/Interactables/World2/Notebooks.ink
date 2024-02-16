INCLUDE ../../globals.ink

{Sequence<6: ->NotL}
{Sequence>=6: ->L}

===NotL===
It is a pile of notebooks
There is a book open on the ground
Do you wish to read it?
+[yes]
You opened the book
...#speaker: Hero #portrait: MC_neutral2 #audio: default
Log date: 7 14 XX#speaker: ???  #portrait: default #layout: Default #audio: LAudio
Normal day hanging out with friends!
Its so awesome, I hope it never ends!
Me and **** , along with the crew
I can't be happier!
->N2
+[No]
->END
===L===
Don't touch that!#speaker: L #portrait: L_neutral  #audio: LAudio
!#speaker: Hero #portrait: MC_neutral2 #audio: default
Those are my notebooks #speaker: L #portrait: L_neutral  #audio: LAudio
I don't want you to burn those as well
...#speaker: Hero #portrait: MC_neutral2 #audio: default
->END

===N2===
Continue Reading? #speaker: ???  #portrait: default #layout: Default #audio: default
+[Yes]
Log Date: 11 2 XX
My friend... something is going on #speaker: ???  #portrait: default #layout: Default #audio: LAudio
He stopped hanging out with the group
He is currently in a Lab? He refuses to talk to me...
I hope he comes back to his senses...
->N3
+[No]
->END

===N3===
Continue reading? #speaker: ???  #portrait: default #layout: Default #audio: default
+[Yes]
Log Date: 2 28 XX
These.. creatures have been appearing #speaker: ???  #portrait: default #layout: Default #audio: LAudio
So much so that me and my friends had to go underground...
Well..remaining friends
Why do they look like him?
->N4
+[No]
->END

===N4===
Continue reading? #speaker: ???  #portrait: default #layout: Default #audio: default
+[Yes]
Log Date: 7 20 XX #speaker: ???  #portrait: default #layout: Default #audio: LAudio
Most of my friends are dead
The very few remaining are giving up
I need to act strong for them
If I lose them all...I...
->N5
+[No]
->END

===N5===
Continue reading? #speaker: ???  #portrait: default #layout: Default #audio: default
+[Yes]
Log Date: 10 2 XX #speaker: ???  #portrait: default #layout: Default #audio: LAudio
I need to kill him
He has to die
I need to save my friends
I need the antidote
They need me, im their only hope...
I need them
Please...I need to stop him
No more pages remaining#speaker: ???  #portrait: default #layout: Default #audio: default
->END
+[No]
->END
