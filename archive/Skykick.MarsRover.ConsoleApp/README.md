A rover's position is represented by a combination of an x and y co-ordinates and a letter
representing one of the four cardinal compass points. The plateau is divided up into a grid to
simplify navigation. An example position might be 0, 0, N, which means the rover is in the bottom
left corner and facing North.

As I was reading through the "specs" I started to visualize how the actions would look in the physical world. From there I drew out a sketch of the discrete "objects" that made out the different steps.

Rover: representation of the actual object, with an identifier and a recording of it's location  
GridMap: representation of the "plateau", or area in which the rovers would be located
Position: object to hold the coordinate and orientation values  
"Translator" objects to manage conversion of input parameters to usable concepts for movement of rovers  
Some of these values I added to a constants file, as I tend to find it helps me organize the data better to have them in one place, instead of separate classes (just incase of reuse)

I also considered the need for a "Controller" object to manage the communication from user to rover, as well as an "Orchestrator" to process instructions and perhaps coordinate between the GridMap and Rovers, etc.

In the end I decided against the "Orchestrator", and included that functionality into the GridMap object directly, to minimize the data that I would need to pass to successive objects.


It was easy to consider the idea of storing grid data in a 2d array since we had x/y parameters directly available. However, since I felt it was important to be able to distinguish one rover from another (even if not specifically needed right now) I didnt want to have to go through the entire array to find a specific instance of a rover. It is with similar consideration that I didnt think that holding the data in a collection would be the best, since it would make lookups of coordinated positions harder as well.  

This is why I decided to use both concepts. A collection to store a  list of rovers in the grid, and a 2d array to store the location of specific rovers - meaning I could just check one coordinate in the array to see if there was anything there. 

Some of the ideas above were not explicitly stated in the spec - here is the full list of assumptions and questions that I went through.

Questions:
- What happens if there is an input error  
Assumption: provide messaging around instruction format validation, and other errors
- What happens when Rover goes out of bounds OR
- Rover tries to travel to an occupied position OR
- Rover would have to travel THROUGH an occupied position  
Assumption: Rover can not travel outside of grid, nor to an occupieed coordinate.  Added validation and out put messaging to reflect this. Also made the assumption that the rover should travel as far as possible until blocked, then return an appropriate message, as well as the final output 