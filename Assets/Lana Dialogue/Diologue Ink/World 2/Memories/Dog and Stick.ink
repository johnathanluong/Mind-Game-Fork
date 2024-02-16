INCLUDE ../../globals.ink

{DogAndStick==1: ->choose}
~memories++
{ rock =="": ->choice1 | ->choice2 }

===choice1===
<b>FREE GLASSES!<\b>#speaker: L #portrait: L_Cutscene1  #audio: LAudio #layout: Cutscene
I'll take those thank you very mu-
oh
they're broken
Wait...
? #speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
+[Ask about the door]
... #speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
Oh, don't mind that #speaker: L #portrait: L_Cutscene1  #audio: LAudio #layout: Cutscene
I don't want you to get hurt, so I wouldn't reccomend going in
Jimmy lost an arm to the thing in there, and he talked my ear off about it...
But if you would lend <i>me</i> an ear, these glasses...
 ->Memory
+[don't ask about the door]
->choice11

===choice2===
Broken Glasses... well luckily Laryl has my back so those are unessessary...#speaker: L #portrait: L_Cutscene2  #audio: LAudio #layout: Cutscene
...
Do you remember...the dog.
+[I don't remember]
...#speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
Figures...#speaker: L #portrait: L_Cutscene2  #audio: LAudio #layout: Cutscene
To keep it brief, you were antagonizing a dog and i stopped you 
You... really aggravated it though, so it attacked me, broke my glasses...
I will give you some credit though. You made sure to save me from your own mess
You gave these glasses back to me and just... walked off
There was this look you gave me...
...
Wait, why is there also a stick here?
<b\>!!!!!!!!</b> #speaker:Hero #portrait: MC_neutral_cutscene
<u>*Hero has found a edible stick! It has been added to your inventory*<u> #speaker: ??? #portrait: default #layout: Default
????? .#speaker: L #portrait: L_Cutscene2  #audio: LAudio #layout: Cutscene
~DogAndStick=1
->END

===choice11===
... #speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
Oh, Hero?#speaker: L #portrait: L_Cutscene1  #audio: LAudio #layout: Cutscene

+[<b>Stare at Door</b>]
!!! #speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
Uuummmmmm... please don't do what I think you want to do...#speaker: L #portrait: L_Cutscene1  #audio: LAudio #layout: Cutscene
uhh
Distraction!
Story Time!
->Memory

===Memory===
I used to have... a good friend #portrait: L_Cutscene1  #audio: LAudio #layout: Cutscene
But one day, he... changed
We were going to meet up like usual
but when i saw him, he wasn't acting like himself
My friend who was normally so kind... wasn't... for some reason
He was messing with a chained up dog. Antagonizing it and harrassing it to no end
He wasn't physcially harming it, but even so, it wasn't right
I asked him what the hell he was doing, but he just ignored me
I pushed him aside and forced him to stop, going to console the poor dog
...but he was too riled up already
It lunged at me and I fell back, droppping and breaking these very glasses
Luckily the chain kept it from getting to me
but it tried again... and the chain <i>snapped</i>
My friend jumped and grabbed the chain, and I booked it
I hid and not long after, he was there, bruised up but no dog in sight
Before I could say anything, he handed me the glasses, and left
that was the last day he talked to me like normal...
w-wait what the-
<b\>!</b> #speaker:Hero #portrait: MC_neutral_cutscene #audio: default #layout: Cutscene
<u>*Hero has found a edible stick! It has been added to your inventory*<u> #speaker: ??? #portrait: default #layout: Default
Why is that here? There wasn't even a stick in my story...#speaker: L #portrait: L_Cutscene1  #audio: LAudio #layout: Cutscene
..Why, are you looking at the stick like that...
oh god...
<u>Please don't</u>
~DogAndStick=1
->END

===choose===
which version would you like too see?
+[L nice]
->choice1
+[L angry]
->choice2