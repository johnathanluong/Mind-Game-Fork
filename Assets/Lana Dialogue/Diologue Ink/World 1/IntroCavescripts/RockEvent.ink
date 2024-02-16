INCLUDE ../../globals.ink

->Main
===Main===
Since you're new here, I need to know your name. #speaker:??? #portrait: default #audio: TutorialAudio
->choiceName
{ rock =="": ->choice1 | ->already_chosen }


===choiceName===
... #speaker: ??? #portrait: MC_neutral #audio: default
Oh, what's that? (Select the option by pressing [ENTER]) #speaker:??? #portrait: default #audio: TutorialAudio
    + [Hero]
    ~Name="Hero"
    ... #speaker: Hero #portrait: MC_neutral #audio: default
    {Name}? Sounds like a video game protagonist... #speaker:??? #portrait: default #audio: TutorialAudio
    THAT'S PERFECT!!!
    Now that I know your name, I'll tell you mine. Just call me... Jeff.
    ->choice1
===choice1===
Now next in order is a SUPER SIMPLE TASK! Go ahead and push the rock.#speaker:Jeff #portrait: default #audio: TutorialAudio
    +[Push the Rock]
    ~rock="Pushed"
    ...#speaker: Hero #portrait: MC_neutral #audio: default
    Perfect first try!#speaker:Jeff #portrait: default #audio: TutorialAudio
    ->already_chosen
    +[Don't Push the Rock]
    ~rock="Not Pushed"
    ...#speaker: Hero #portrait: MC_neutral #audio: default
    Lets... try that again... #speaker:Jeff #portrait: default #audio: TutorialAudio
    ->choice1
    
===already_chosen===
{Pissed ==1: Alright then.}#speaker:Jeff #portrait: default #audio: TutorialAudio
{Pissed ==2: MY GOD. OK. So now you finally listen! It seriously took you 2 tries...} #speaker:Jeff #portrait: default #audio: TutorialAudio
Now go to the right and down. It's about time for the <b>ULTIMATE GAME!</b>#speaker:Jeff #portrait: default #audio: TutorialAudio
->END