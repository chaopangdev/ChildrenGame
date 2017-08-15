## ChildrenGame

This is a .Net MVC implementation of the Children Game (Josephus Problem). 
The applicaition needs to call remote API to load game setup parameters, 
and then upload the result to remote API.

## .Net Framework

.Net 4.5.2

## Installation

git clone https://github.com/chaopangdev/ChildrenGame.git.
Load the solution into Visual Studio and build the solution.

## Test

Run the test cases in ChildrenGame.Tests project.

## Run

Run ChildrenGame project.

## Algorithm Complexity

The runtime order complexity is O(nk), in which 'n' is the children count and 'k' is the elimination count.

## Resiliency

* All application internal exceptions are captured and redirected to a customized error page. 
* Remote API failure is handled and prompt an error message on index page.
* Unexpected setup parameter doesn't trigger the game process, but display an error message on index page.

## Assumptions and Trade-offs

* The main service class, GameService, has two dependency services interfaces, IGameHelperService and IGameAlgorithmService. This has increased the complexity of the GameService, while brings expandability to the application. By doing this way, better algorithm may be introduced easily to replace the current one, and setup parameters loading method is replacable as well. 

* .Net MVC may be not the most efficient way to implement this program, but considering it is more widely used and I am familiar with it, I chose it complete the task.