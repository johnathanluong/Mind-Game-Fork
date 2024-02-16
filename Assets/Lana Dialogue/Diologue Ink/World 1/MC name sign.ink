INCLUDE ../globals.ink


{ MC =="": ->choice1 | ->already_chosen }
=== choice1 ===
Hello I am a sign made by the ultimate man
I have a question...
What is your name?
    + [Bob]
    ~MC="Bob"
    Your name is now {MC}
    -> END
    + [Karen]
    ~MC="Karen"
    Your name is now {MC}
    -> END
    + [MC]
    ~MC="MC"
    Your name is now {MC}
    -> END

=== already_chosen ===
I am still the ultimate sign!
You chose your name to be {MC} , Do you wish to change it?
    +[Yes]
    Very well
    ->choice1
    +[No]
    Then It shall stay the same!
    -> END
