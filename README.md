# Wordle - Itamar Shaked
## Introduction
In 2022, my computer science class was tasked with creating a full website project.
The project instructions were to code and deploy a website using HTML, CSS and C# that is connected to an SQL database and allows the user to create a profile.
From there on out, we were given complete freedom.
This project was graded 100%.

## How to Run
Clone this repository, and open the solution file in Visual Studio.
Set the **login.aspx** file as the start page.

## A brief explanation on Wordle
For my project, I decided to create my own version of the, at the time, popular game: Wordle.
Wordle is a word game were a word is chosen at random everyday, and the player is tasked with finding what that word is.
The player has 6 attempts at doing so, and every attempt reveals a clue at the final word:
1. Letters colored **gray** do **not** exist in the word.
2. Letters colored **yellow** exist in the word, but are **at the wrong location**.
3. Letters colored **green** exist in the word, and are at the correct location.

## Approach
I started by finishing the basic requirements early on (i.e. allowing the user to create their own profile), and played with the wordle algorithm from there on.
To do this, I created the following two tables:
1. **Users**
   | username | password | day | month | year | rowOne | rowTwo | rowThree | rowFour | row | completed |
   | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
   | | | Day of last played | Month of last played | Year of last played | Row #1 input | ... | ... | ... | ... | Last row completed | Completed? |
2. **Words**
   | word | day | month | year | first | second | third | fourth | fifth | failed | total |
   | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
   | | Day generated | Month generated | Year generated | # of people who completed first try | ... | ... | ... | ... | ... | # of people who attempted |

### Database Algorithm:
1. When a user logs in, check if it's a new day (i.e. the date doesn't match the last word's date in the words table). If it isn't, skip to step #4.
2. Generate a new word and write it to the words table with initializer values. 
3. Clear the user's rowOne-Four, row and completed values.
4. Check if the user already started attempting today's wordle (i.e. last played date in users table matches generated date in words table). If it isn't, skip to step #6.
5. Load all of the values from rowOne-row into the game.
6. When the user makes an attempt, run the word-checking algorithm and write the result to the users table, and update row. If the user guessed correctly, skip to step # .
7. If row exceeds the number of attempts (6), write 1 to completed in the users table and increment failed and total by 1 in words table. Otherwise, return to step #6.
8. Write 1 to completed in the users table and increment the winning row and total by 1 in words table.

### Game Algorithm:
1. The player chooses a word.
2. Start at the first letter, let the current letter be **cl**.
3. If **cl** is the same as the chosen word's letter at the same index, color the letter green. Go to step #8.
4. Start at the letter at the index of **cl** + 1, let the current letter in the chosen word be **cwcl** (chosen word letter).
5. If **cl** is the same as **cwcl**, color **cl** yellow. Go to step #8.
6. Increment **cwcl** by one.
7. If **cwcl** is out of bounds, color **cl** gray. Go to step #8. Otherwise, go to step #5.
8. If **cl** isn't out of bounds, return to step #2. Otherwise, return to step #1.
