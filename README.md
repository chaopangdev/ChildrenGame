## ChildrenGame

This is a .Net MVC implementation of the Children Game (Josephus Problem). 
The applicaition needs to call remote API to load game setup parameters, 
and then upload the result to remote API.

## .Net Framework

.Net 4.5.2

## Installation

git clone https://github.com/chaopangdev/ChildrenGame.git

## Test

Run the test cases in ChildrenGame.Tests project.

## Run

Build ChildrenGame project and run it.

## Algorithm Complexity

The runtime order complexity is O(nk), in which 'n' is the children count and 'k' is the elimination count.

## Resiliency

* All application internal exceptions are captured and redirected to a customized error page. 
* Remote API failure is handled and prompt an error message on index page.
* Unexpected setup parameter doesn't trigger the game process, but display an error message on index page.


