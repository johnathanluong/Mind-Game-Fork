INCLUDE globals.ink

{ notebook == 0: ->Intro | ->Normal }

===Intro===
~notebook=1
This is the only thing of note in this room.#speaker:JJ #portrait: Jester_neutral #layout: Default #audio: JesterAudio
It fills when you gain memories in the worlds...
I don't even know why this is here so...
It's <i>probably</i> not important.
->Normal

===Normal===
<i>{Name}</i>'s notebook #speaker: ???  #portrait: default #layout: Default #audio: default
Entries Available: {memories} / 8
Wish to look through?
+[yes]
->Entries
+[no]
->END

===Entries===
Log 1: {ArtandChaos==0:??? |ArtandChaos}
+ [View Log]
{ArtandChaos==0:No info available |->ArtandChaosI}
->Entries
+ [Next Page]
->Entries1
+ [Exit]
->END

===Entries1===
Log 2: {FireAlarm==0:??? |FireAlarm}
+ [View Log]
{FireAlarm==0:No info available |->FireAlarmI}
->Entries1
+ [Next Page]
->Entries2
+ [Exit]
->END

===Entries2===
Log 3: {MachoMan==0:??? |MachoMan}
+ [View Log]
{MachoMan==0:No info available |->MachoManI}
->Entries2
+ [Next Page]
->Entries3
+ [Exit]
->END

===Entries3===
Log 4: {CoreMemory1==0:??? |CoreMemory1}
+ [View Log]
{CoreMemory1==0:No info available|->CoreMemory1I}
+ [Next Page]
->Entries4
+ [Exit]
->END
===Entries4===
Log 5: {DogAndStick==0:??? |DogAndStick}
+ [View Log]
{DogAndStick==0:?No info available|->DogAndStickI}
->Entries4
+ [Next Page]
->Entries5
+ [Exit]
->END

===Entries5===
Log 6: {ScrapsAndFailure==0:??? |ScrapsAndFailure}
+ [View Log]
{ScrapsAndFailure==0:No info available |->ScrapsAndFailureI}
->Entries5
+ [Next Page]
->Entries6
+ [Exit]
->END

===Entries6===
Log 7: {Smile==0:??? |Smile}
+ [View Log]
{Smile==0:No info available|->SmileI}
->Entries6
+ [Next Page]
->Entries7
+ [Exit]
->END

===Entries7===
Log 8: {CoreMemory2==0:??? |CoreMemory2}
+ [View Log]
{CoreMemory2==0:No info available|->CoreMemory2I}
->Entries7
+ [Back to First Log]
->Entries
+ [Exit]
->END



===ArtandChaosI===
Dear notebook (God I don't know why I'm writing this)
but Olivia told me I needed it (smartass)
Well, I'm failing classes (parents are mad like usual)
So the school required I get a tutor (Olivia)
But its fine, another excuse to not go home I guess
Though this log is about a recent trip to a museum
Instead of tutoring, I said we should go to the museum, to <i>learn</i> or whatever
BUT MUSEUMS ARE SO BORING!
Who even likes looking at naked statues, or weird colored squares??
That art just looks like a child threw up on paper.
I could do that!
Whatever the case, I brought a lighter and everything was right in the world.
As far as I know, I haven't been caught.
->Entries
===FireAlarmI===
Dear notebook (Why am I still writing in this?)
Player 2 decided 'hm im such a child ima be late to school'.
My standards are low for that kid, but I didn't think it could go that low.
If anything, one more tardy and he would have been suspended.
...
Well, couldn't let that happen.
Pulled the alarm and sent everyone running like headless chickens.
Finally some fun chaos in such a life sucking place!
Gary got in undetected and he bought me like 10 games after that.
Even though he's a pain in my ass, at least he is entertaining (also his family rich af)
->Entries1
===MachoManI===
Dear notebook (Ok this isn't that bad)
Gary talks my ear off about 'how good I am at games'
Im not good, Gary just sucks.
Like literally he can't get through a single screen of MachoMan!
You just JUMP AND SHOOT!. The enemy patterns are just like every other game.
Even the yellow blob was a piece of cake but NOOOO he makes me do it!
He . Is. An. Idiot.
...But he's my idiot.
...Idiots can help distract you from 2 bigger idiots...
...why won't they just leave eachother...and me alone.
... god this is to personal I need to stop.
->Entries2
===CoreMemory1I===
Dear notebook (Guess this is a reflection piece)
Today's Gary's birthday and all I did was play games with him soo 
nothing special
I'll write about our first meeting I guess
He was always curled up in a corner
failing miserably at whatever game he was playing
He had no friends.
Well I guess I didn't either, but at the time I didn't need any
HELL I STILL DON'T!
He is just entertainment...
totally...
BUT, his sobbing was really annoying.
SO annoying I took his game.
I defeated the boss and was about to keep the console for myself.
It was the deluxe game with extra content and the console was limited edition!
I needed it! I don't even have a game console, WHY DOES THE IDIOT GET IT?!?!
Well Gary with his dumb braincell took it the wrong way and thanked me?
He was like 'omg you beat the <i>TUTORIAl</i> for me oesgrdfrseasgfd'
He was so annoying and so loud I just gave it back and walked away.
However, it caused him to cling to me and now he won't leave me alone.
...Hanging out with him is fun sometimes I guess
At least his place is STOCKED with C4 energy drinks.
->Entries3
===DogAndStickI===
Dear notebook
...Why am I writing this? 
Maybe cause I'm not sure who I could tell
I've been a lot... angrier lately
Little things keep setting me off, people...animals
Gary likes dogs... way to much... so can't tell him
It didn't even really do anything wrong, it just... reminded me of someone
It also smelled... really really bad (like someone else I know)
I just kept messing with it, it was chained up so it couldn't get me
...Now I know that dog didn't deserve it...and she did too
so of course since shes oh so good, she tried to stop me...
but the dog turned on her instead
...
I still have scars from that stupid encounter
I won't let anyone get hurt from my mistakes, even if she makes me...mad
...
WHatever, cats are better anyways (IF only Gary didn't hate cats)
(Or top hats)
->Entries4
===ScrapsAndFailureI===
Dear <s>diary</s> <i>journal</i>
So, today I was in one of my moods I guess
I walked the path where I knew I'd see her again
She was holding this nice purple notebook
One her parents or something probably bought for her
...
She does nothing yet I try so hard
She doesn't deserve things...
Notes = good grades
No Notes = failure
If she gets a fun life at least I can take something away from her
Makes things more fair
I...
...
I'll write more later 
->Entries5
===SmileI===
Dear diary (whatever I guess that's what this is)
I saw her again today
She was at that tree, the one in the park where we first met
...
I... wasn't always so bad to her
When I was young, let's just say, we both had the world
We were friends or whatever
Imagine that, that idiot and me used to be <i>friends</i>
She always used to compliment me on my smile
said it made her smile too or something
I don't really feel like doing that much anymore
...
Whatever
I...
I need to think about some things...
What am I even writing?
->Entries6
===CoreMemory2I===
Plan:
Today, I'm going to mess with Lily
Something is wrong, very wrong
I can't think straight!
I have been thinking about her more.
I've been avoiding Gary.
I've been avoiding Olivia
I...
I just need to knock her down a peg again
YEAH! Im just not feeling like myself
She deserves to be pushed about.
I hate her!
I hate her
...
Why do I hate her...
...
{Fin!=1:->Entries7}

Fixed Log:
I know why I felt that way
I... was jealous
I wanted her to feel as bad as I did
I was being terrible
I'm just so <i>angry</i> 
But she did nothing wrong
I...
I'm going to therapy
Probably my last log for a while
For whoever is reading this, thanks for dealing with my rambling.
I need to change.
I can change.
Thanks for giving me the chance to change
->Entries7